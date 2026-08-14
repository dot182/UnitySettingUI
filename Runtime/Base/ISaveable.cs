namespace Uni.Settings
{
    public interface ISaveable
    {
        void Init(ISaveMethod saveMethod);
        void Save();
    }
}