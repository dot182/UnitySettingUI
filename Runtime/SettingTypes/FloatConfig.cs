using System;
using UnityEngine;

namespace Uni.Settings
{
    [Serializable]
    public class FloatConfig : SettingConfig<float>
    {
        public virtual float Max => max;
        public virtual float Min => min;

        [SerializeField] private float max;
        [SerializeField] private float min;
    }
}