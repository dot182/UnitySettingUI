using System;
using UnityEngine;

namespace Uni.Settings
{
    [Serializable]
    public class FloatConfig : SettingConfig<float>
    {
        public float Max
        {
            get => max;
            set
            {
                max = value;
                this.Value = Mathf.Max(Value, max);
            }
        }
        public float Min
        {
            get => min;
            set
            {
                min = value;
                this.Value = Mathf.Min(Value, min);
            }
        }

        [SerializeField] private float max;
        [SerializeField] private float min;
    }
}