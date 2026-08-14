using System;
using TMPro;
using UnityEngine;

namespace Uni.Settings
{
    /// <summary>
    /// Controls setting UI
    /// </summary>
    [Serializable]
    public abstract class SettingUIConfig
    {
        public string SettingName;
        public abstract string PrefabKey { get; }

        // runtime
        public GameObject Instantiated { get; protected set; }
        public TextMeshProUGUI Label { get; protected set; }

        private UIPrefabDataSheet prefabDataSheet;

        public virtual GameObject Load(UIPrefabDataSheet prefabDataSheet)
        {
            this.prefabDataSheet = prefabDataSheet;

            UIPrefabData data = prefabDataSheet.MainPrefabDatas[PrefabKey];
            Instantiated = GameObject.Instantiate(data.Prefab);

            Label = Instantiated.transform.Find(data.LabelTransformName)?.GetComponent<TextMeshProUGUI>();
            Label.text = SettingName;

            LoadUI(Instantiated, prefabDataSheet);
            return Instantiated;
        }
        public virtual void RefreshUI()
        {
            Label.text = SettingName;
            LoadUI(Instantiated, prefabDataSheet);
        }
        public abstract void LoadUI(GameObject instantiated, UIPrefabDataSheet prefabData);
    }
    /// <summary>
    /// Controls setting UI
    /// If you are deriving from this: We will call UpdateShownValue() for you after load ui, so don't worry. 
    /// </summary>
    [Serializable]
    public abstract class SettingUIConfig<T> : SettingUIConfig, ISaveable
    {
        [field: SerializeField, SerializeReference, SubclassSelector]
        public SettingConfig<T> Config { get; private set; }
        
        public void Init(ISaveMethod saveMethod)
        {
            Config.OnValueChanged += UpdateShownValue;
            Config.Init(this, saveMethod);
        }
        public override GameObject Load(UIPrefabDataSheet prefabDataSheet)
        {
            GameObject gameObject = base.Load(prefabDataSheet);
            UpdateShownValue();
            return gameObject;
        }
        /// <summary>
        /// Refreshes value and ui
        /// </summary>
        public override void RefreshUI()
        {
            Config.RefreshValue();
            base.RefreshUI();
        }
        public void Save()
        {
            Config.SaveCurrentValue();
        }
        public void UpdateShownValue()
        {
            if (Instantiated)
            {
                UpdateShownValue(Config.Value);
            }
        }
        public abstract void UpdateShownValue(T value);
    }
}