namespace ModSyncLib
{
    public enum CheckResult
    {
        Synced,
        NeedsSync,
    }

    public interface ISyncResult
    {
        Exception Error { get; }
    }

    public interface IGameModManager
    {
        Task<CheckResult> CheckInstalled(ModPack ModPack);
        Task<ISyncResult> Sync(ModPack ModPack);
    }
}
