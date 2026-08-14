using System;
using UnityEngine;
using UnityEngine.UI;

namespace Uni.Settings
{
    /// <summary>
    /// Inherit from IOnButtonSettingClick
    /// </summary>
    [Serializable]
    public class ButtonSetting : SettingUIConfig
    {
        public override string PrefabKey => "Button";

        public Button Button { get; protected set; }
        public virtual string ButtonText => buttonText;
        [SerializeField] private string buttonText;
        [SerializeReference, SubclassSelector] public IOnButtonSettingClick OnButtonSettingClick;

        public override void LoadUI(GameObject instantiated, UIPrefabDataSheet prefabData)
        {
            Button = instantiated.GetComponentInChildren<Button>();
            Button.onClick.AddListener(OnButtonSettingClick.OnClick);
        }
    }

}