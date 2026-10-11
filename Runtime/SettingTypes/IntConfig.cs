using System;
using UnityEngine;

namespace Uni.Settings
{
    [Serializable]
    public class IntConfig : SettingConfig<int>
    {
        public int Max
        {
            get => max;
            set
            {
                max = value;
                this.Value = Math.Max(Value, max);
            }
        }
        public int Min
        {
            get => min;
            set
            {
                min = value;
                this.Value = Math.Min(Value, min);
            }
        }

        [SerializeField] private int max;
        [SerializeField] private int min;
    }
}