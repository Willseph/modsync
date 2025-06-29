using System.ComponentModel;

namespace ModSyncLib
{
    public class ModPackDownloaderProgress
    {
        public string? Status { get; set; }
        public bool ProgressIndeterminate { get; set; }
    }

    public class ModPackDownloader
    {
        private DirectoryInfo downloadDir;

        public ModPackDownloader(DirectoryInfo downloadDir) {
            this.downloadDir = downloadDir;
        }

        public async Task<FileInfo> DownloadModPack(ModPack modPack, DirectoryInfo modFolder, BackgroundWorker? worker = null)
        {
            worker?.ReportProgress(0, new ModPackDownloaderProgress
            {
                ProgressIndeterminate = true,
                Status = "Checking for cached mod pack archive...",
            });
            var localFile = LocalFileForModPack(modPack);
            if(IsModPackDownloaded(modPack))
            {
                return localFile;
            }

            throw new NotImplementedException();
        }

        bool IsModPackDownloaded(ModPack modPack)
        {
            if (!downloadDir.Exists)
            {
                return false;
            }

            var modPackFile = LocalFileForModPack(modPack);
            if (!modPackFile.Exists)
            {
                return false;
            }

            var localFileHash = SHA1Helper.FileSHA1(modPackFile);
            if (localFileHash == null)
            {
                throw new Exception($"Could not generate SHA-1 hash of file at {modPackFile.FullName}");
            }

            return localFileHash.Trim().ToLowerInvariant() == modPack.FileHash.Trim().ToLowerInvariant();
        }

        FileInfo LocalFileForModPack(ModPack modPack)
        {
            var remotePathUri = new Uri(modPack.RemoteUri);
            var fileName = Path.GetFileName(remotePathUri.AbsolutePath);
            return new FileInfo(Path.Combine(downloadDir.FullName, fileName));
        }
    }
}
