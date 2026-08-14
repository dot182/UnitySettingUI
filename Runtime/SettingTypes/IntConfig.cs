using System;
using UnityEngine;

namespace Uni.Settings
{
    [Serializable]
    public class IntConfig : SettingConfig<int>
    {
        public virtual int Max => max;
        public virtual int Min => min;

        [SerializeField] private int max;
        [SerializeField] private int min;
    }
}