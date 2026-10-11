using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Uni.Settings
{
    /// <summary>
    /// You can also inherit <see cref="IApplyAllSettings"/> to have a callback to apply everything,
    /// and <see cref="ISetupSettings"/> to have a setup callback for additional listeners
    /// </summary>
    [CreateAssetMenu(menuName = "SettingUI/Setting Menu")]
    public class SettingsUIMenu : ScriptableObject
    {
        public Action OnTryCloseMenu;
        public string SettingsMenuName;

        [SerializeReference, SubclassSelector]
        public ISaveMethod SaveMethod;
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

        public SettingUIConfig Get(string key)
        {
            return SettingGroups
                .SelectMany(x => x.Settings)
                .FirstOrDefault(x =>
                    string.Equals(x.SettingName, key, StringComparison.OrdinalIgnoreCase)
                );
        }

        public T Get<T>(string key)
            where T : SettingUIConfig
        {
            return Get(key) as T;
        }

        public void InitAllSettings()
        {
            if (this is ISetupSettings setupSettings)
                setupSettings.Setup();
            foreach (SettingsGroup group in SettingGroups)
            {
                foreach (SettingUIConfig config in group.Settings)
                {
                    if (config is ISaveable saveable)
                    {
                        try
                        {
                            saveable.Init(SaveMethod);
                        }
                        catch (Exception e)
                        {
                            Debug.LogException(e);
                        }
                    }
                }
            }
            if (this is IApplyAllSettings applyAllSettings)
                applyAllSettings.ApplyAllSettings();
        }

        public void SaveAllSettings()
        {
            SaveMethod.SaveToDisk();
        }
    }
}
