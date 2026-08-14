using System;
using UnityEngine;

namespace Uni.Settings
{
    /// <summary>
    /// Holds setting data.
    /// If you inherit from this, you can inherit from IOnSettingApply to get a callback on value changed
    /// ie. to apply changes immediately
    /// </summary>
    [Serializable]
    public abstract class SettingConfig<T>
    {
        public event Action<T> OnValueChanged;
        public SettingUIConfig<T> UI { get; private set; }
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                if (this is IOnSettingApply<T> onApply)
                    onApply?.OnApply(value);
                OnValueChanged?.Invoke(value);
            }
        }
        private T value;

        public virtual string SaveKey => saveKey;
        [SerializeField] private string saveKey;

        public virtual T DefaultValue => defaultValue;
        [SerializeField] private T defaultValue;

        protected ISaveMethod saveMethod;

        public void Init(SettingUIConfig<T> parent, ISaveMethod saveMethod)
        {
            UI = parent;
            this.saveMethod = saveMethod;
            RefreshValue();
        }
        /// <summary>
        /// Updates the Value field.
        /// </summary>
        public virtual void RefreshValue()
            => Value = saveMethod.Get(SaveKey, defaultValue);
        /// <summary>
        /// Saves the current Value field.
        /// </summary>
        public virtual void SaveCurrentValue()
            => saveMethod.Set(SaveKey, Value);
    }
}