using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Uni.Settings
{
    /// <summary>
    /// Inherit from IDropdownOptions
    /// </summary>
    [Serializable]
    public class DropdownSetting : SettingUIConfig<string>
    {
        public override string PrefabKey => "Dropdown";
        public TMP_Dropdown Dropdown { get; protected set; }

        public override void UpdateShownValue(string value)
        {
            int index = Dropdown.options.FindIndex(x =>
                x.text.Equals(value, StringComparison.OrdinalIgnoreCase)
            );
            if (index != -1)
                Dropdown.SetValueWithoutNotify(index);
        }

        public override void LoadUI(GameObject instantiated, UIPrefabDataSheet prefabData)
        {
            Dropdown = instantiated.GetComponentInChildren<TMP_Dropdown>();
            RefreshOptionsUI();
            Dropdown.onValueChanged.AddListener(OnDropdownChange);
        }

        private void OnDropdownChange(int index) => Config.Value = Dropdown.options[index].text;

        public void RefreshOptionsUI()
        {
            Dropdown.ClearOptions();
            if (Config is IDropdownOptions dropdownOptions)
            {
                var options = dropdownOptions.GetOptions();
                if (options != null)
                {
                    Dropdown.AddOptions(options);
                    UpdateShownValue();
                }
            }
        }

        /// <summary>
        /// Shortcut
        /// </summary>
        public void AddOptions(List<string> options)
        {
            if (Config is DropdownConfig dropdownConfig)
            {
                dropdownConfig.Options = options;
                if (Dropdown)
                    RefreshOptionsUI();
            }
        }
    }
}
