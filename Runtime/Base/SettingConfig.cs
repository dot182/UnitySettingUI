using System;
using UnityEngine;

namespace Uni.Settings
{
    /// <summary>
    /// Holds setting data.
    /// </summary>
    [Serializable]
    public abstract class SettingConfig<T>
    {
        public event Action<T> OnValueChanged;
        public T Value
        {
            get => value;
            set
            {
                Debug.Log($"Setting value {SaveKey} from {this.value} to {value}");
                this.value = value;
                saveMethod.Set(SaveKey, value);
                OnValueChanged?.Invoke(value);
            }
        }
        private T value;

        public string SaveKey;
        public T DefaultValue;
        protected ISaveMethod saveMethod;

        public void Init(ISaveMethod saveMethod)
        {
            this.saveMethod = saveMethod;
            RefreshValue();
        }
        /// <summary>
        /// Updates the Value field.
        /// </summary>
        public void RefreshValue()
            => Value = saveMethod.Get(SaveKey, DefaultValue);
        public void ResetToDefault()
            => Value = DefaultValue;
    }
}