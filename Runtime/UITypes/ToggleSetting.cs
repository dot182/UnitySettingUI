using System;
using UnityEngine;
using UnityEngine.UI;

namespace Uni.Settings
{
    [Serializable]
    public class ToggleSetting : SettingUIConfig<bool>
    {
        public override string PrefabKey => "Toggle";
        public Toggle Toggle { get; protected set; }
        public override void LoadUI(GameObject instantiated, UIPrefabDataSheet prefabData)
        {
            Toggle = instantiated.GetComponentInChildren<Toggle>();
            Toggle.onValueChanged.AddListener(OnToggleChange);
        }
        public override void UpdateShownValue(bool value)
            => Toggle.SetIsOnWithoutNotify(value);
        private void OnToggleChange(bool value)
            => Config.Value = value;
    }
}