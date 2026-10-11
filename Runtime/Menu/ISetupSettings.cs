namespace Uni.Settings
{
    /// <summary>
    /// Have your SettingsUIMenu inherit from this to set up additional listeners etc
    /// </summary>
    public interface ISetupSettings
    {
        void Setup();
    }
}