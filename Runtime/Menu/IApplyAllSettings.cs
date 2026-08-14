namespace Uni.Settings
{
    /// <summary>
    /// Have your SettingsUIMenu inherit from this to have some logic to apply your settings.
    /// Good for if you have settings that need to be applied in a certain order
    /// </summary>
    public interface IApplyAllSettings
    {
        void ApplyAllSettings();
    }
}