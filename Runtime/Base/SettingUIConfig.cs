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

        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                if (Instantiated)
                    Instantiated.SetActive(enabled);
            }
        }
        private bool enabled = true;
        private UIPrefabDataSheet prefabDataSheet;

        public virtual GameObject Load(UIPrefabDataSheet prefabDataSheet)
        {
            this.prefabDataSheet = prefabDataSheet;

            UIPrefabData data = prefabDataSheet.MainPrefabDatas[PrefabKey];
            Instantiated = GameObject.Instantiate(data.Prefab);

            Label = Instantiated
                .transform.Find(data.LabelTransformName)
                ?.GetComponent<TextMeshProUGUI>();
            Label.text = SettingName;

            LoadUI(Instantiated, prefabDataSheet);
            Instantiated.SetActive(Enabled);
            return Instantiated;
        }

        public virtual void Refresh()
        {
            Label.text = SettingName;
            LoadUI(Instantiated, prefabDataSheet);
        }

        public abstract void LoadUI(GameObject instantiated, UIPrefabDataSheet prefabData);
    }

    /// <summary>
    /// Controls setting UI
    /// </summary>
    [Serializable]
    public abstract class SettingUIConfig<T> : SettingUIConfig, ISaveable
    {
        public T Value => Config.Value;

        [field: SerializeField, SerializeReference, SubclassSelector]
        public SettingConfig<T> Config { get; private set; }

        public void Init(ISaveMethod saveMethod)
        {
            Config.OnValueChanged += _ => UpdateShownValue();
            Config.Init(saveMethod);
        }

        public override GameObject Load(UIPrefabDataSheet prefabDataSheet)
        {
            GameObject gameObject = base.Load(prefabDataSheet);
            return gameObject;
        }

        public void AddListener(Action<T> listener) => Config.OnValueChanged += listener;

        /// <summary>
        /// Refreshes value and ui
        /// </summary>
        public override void Refresh()
        {
            Config.RefreshValue();
            if (Instantiated)
            {
                base.Refresh();
            }
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
