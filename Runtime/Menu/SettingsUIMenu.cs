using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

namespace Uni.Settings
{
    /// <summary>
    /// You can also inherit from IApplyAllSettings to have a callback to apply everything,
    /// instead of needing to have individual logic for each setting
    /// </summary>
    public class SettingsUIMenu : ScriptableObject
    {
        public Action OnTryCloseMenu;
        public string SettingsMenuName;
        [SerializeReference, SubclassSelector] public ISaveMethod SaveMethod;
        public List<SettingsGroup> SettingGroups;

        /// <summary>
        /// Instantiates UI under the parent using the provided data sheet 
        /// and returns the instantiated objects.
        /// </summary>
        public IEnumerable<GameObject> LoadUI(UIPrefabDataSheet data, Transform parent)
        {
            foreach (SettingsGroup group in SettingGroups)
            {
                if (!string.IsNullOrWhiteSpace(group.Header))
                {
                    GameObject header = Instantiate(data.GroupTextHeaderPrefab);
                    header.GetComponentInChildren<TextMeshProUGUI>().text = group.Header;
                    header.transform.SetParent(parent, false);
                    yield return header;
                }

                foreach (SettingUIConfig config in group.Settings)
                {
                    GameObject loaded = config.Load(data);
                    loaded.transform.SetParent(parent, false);
                    yield return loaded;
                }
            }
        }
        public void InitAllSettings()
        {
            foreach (SettingsGroup group in SettingGroups)
            {
                foreach (SettingUIConfig config in group.Settings)
                {
                    if (config is ISaveable saveable)
                    {
                        saveable.Init(SaveMethod);
                    }
                }
            }
            if (this is IApplyAllSettings applyAllSettings)
                applyAllSettings.ApplyAllSettings();
        }
        public void SaveAllSettings()
        {
            foreach (SettingsGroup group in SettingGroups)
            {
                foreach (ISaveable saveable in group.Settings.OfType<ISaveable>())
                {
                    saveable.Save();
                }
            }
            SaveMethod.SaveToDisk();
        }
    }
}