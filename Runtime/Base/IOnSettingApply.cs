namespace Uni.Settings
{
    /// <summary>
    /// Inherit from SettingConfig and this if you would like to have some logic that applies setting immediately.
    /// </summary>
    public interface IOnSettingApply<T>
    {
        void OnApply(T value);
    }
}