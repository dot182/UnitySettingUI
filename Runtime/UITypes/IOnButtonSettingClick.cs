using System;

namespace Uni.Settings
{
    public interface IOnButtonSettingClick
    {
        void OnClick();
    }
    public class OnButtonSettingClick : IOnButtonSettingClick
    {
        private readonly Action onClick;
        public OnButtonSettingClick(Action action)
            => onClick = action;
        public void OnClick()
        {
            onClick?.Invoke();
        }
    }

}