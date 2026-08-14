using System;
using UnityEngine;
using System.Collections.Generic;

namespace Uni.Settings
{
    [Serializable]
    public class SettingsGroup
    {
        public string Header;
        [SerializeReference, SubclassSelector] public List<SettingUIConfig> Settings;
    }
}