using UnityEngine;
using UnityEngine.UI;
using System;

namespace Uni.Settings
{
    [Serializable]
    public class SliderIntSetting : SettingUIConfig<int>
    {
        public override string PrefabKey => "Slider";
        public Slider Slider { get; protected set; }

        public override void UpdateShownValue(int value)
            => Slider.SetValueWithoutNotify(value);
        public override void LoadUI(GameObject instantiated, UIPrefabDataSheet prefabData)
        {
            Slider = instantiated.GetComponentInChildren<Slider>();

            Slider.wholeNumbers = true;

            if (Config is IntConfig data)
            {
                Slider.maxValue = data.Max;
                Slider.minValue = data.Min;
            }

            Slider.onValueChanged.AddListener(OnSliderChange);
        }
        private void OnSliderChange(float value)
            => Config.Value = (int)value;
    }
}