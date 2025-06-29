namespace ModSyncLib
{
    public class ModPack
    {
        public string Name { get; set;  }
        public string CreatorName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string RemoteUri { get; set; }
        public string? FileHash { get; set; }
    }

    public class ReferencedModPack: ModPack
    {
        public string ModPackUrl { get; set; }

        public static ReferencedModPack FromModPack(string modPackUrl, ModPack modPack)
        {
            return new ReferencedModPack
            {
                ModPackUrl = modPackUrl,
                Name = modPack.Name,
                CreatorName = modPack.CreatorName,
                CreationDate = modPack.CreationDate,
                LastUpdated = modPack.LastUpdated,
                RemoteUri = modPack.RemoteUri,
                FileHash = modPack.FileHash,
            };
        }
    }
}
