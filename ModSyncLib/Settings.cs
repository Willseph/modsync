namespace ModSyncLib
{
    public class Settings
    {
        public Dictionary<string, GameConfig> Games { get; set; } = new Dictionary<string, GameConfig>();
        public Dictionary<string, List<ReferencedModPack>> ModPacks { get; set; } = new Dictionary<string, List<ReferencedModPack>>();

        public static Settings Default { get => new(); }
    }
}
