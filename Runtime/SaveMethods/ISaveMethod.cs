namespace Uni.Settings
{
    public interface ISaveMethod
    {
        void Set<T>(string key,T value);
        T Get<T>(string key, T defaultValue = default);
        void SaveToDisk();
    }
}