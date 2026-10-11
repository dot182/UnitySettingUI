using System;
using UnityEngine;
using System.Collections.Generic;

namespace Uni.Settings
{
    [Serializable]
    public class DropdownConfig : SettingConfig<string>, IDropdownOptions
    {
        public List<string> Options
        {
            get => options;
            set
            {
                options = value;
                DefaultValue = options[0];
            }
        }
        [SerializeField] private List<string> options;
        public List<string> GetOptions() => Options;
    }
}