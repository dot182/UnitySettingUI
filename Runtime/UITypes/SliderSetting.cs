using UnityEngine;
using UnityEngine.UI;
using System;

namespace Uni.Settings
{
    [Serializable]
    public class SliderSetting : SettingUIConfig<float>
    {
        public override string PrefabKey => "Slider";
        public Slider Slider { get; protected set; }

        public override void UpdateShownValue(float value)
            => Slider.SetValueWithoutNotify(value);
        public override void LoadUI(GameObject instantiated, UIPrefabDataSheet prefabData)
        {
            Slider = instantiated.GetComponentInChildren<Slider>();

            Slider.wholeNumbers = false;

            if (Config is FloatConfig data)
            {
                Slider.maxValue = data.Max;
                Slider.minValue = data.Min;
            }

            Slider.onValueChanged.AddListener(OnSliderChange);
        }
        private void OnSliderChange(float value)
            => Config.Value = value;
    }
}