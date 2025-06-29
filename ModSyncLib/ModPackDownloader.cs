using System;
using System.ComponentModel;
using System.Net.Http.Handlers;

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

        public async Task<FileInfo> DownloadModPack(ModPack modPack, BackgroundWorker? worker = null)
        {
            if(!downloadDir.Exists)
            {
                Directory.CreateDirectory(downloadDir.FullName);
                downloadDir = new DirectoryInfo(downloadDir.FullName);
            }

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

            worker?.ReportProgress(0, new ModPackDownloaderProgress
            {
                ProgressIndeterminate = false,
                Status = "Downloading mod pack archive...",
            });

            using (var handler = new HttpClientHandler() { AllowAutoRedirect = true })
            using (var ph = new ProgressMessageHandler(handler))
            {
                ph.HttpReceiveProgress += (_, args) =>
                {
                    if (args.TotalBytes == null) return;
                    int percentage = (int)Math.Floor(100.0 * (double)args.BytesTransferred / (double)args.TotalBytes);
                    worker?.ReportProgress(percentage, new ModPackDownloaderProgress
                    {
                        ProgressIndeterminate = false,
                        Status = "Downloading mod pack archive...",
                    });
                };
                using (var client = new HttpClient(ph))
                using (var clientStream = await client.GetStreamAsync(modPack.RemoteUri))
                using (var fileStream = new FileStream(localFile.FullName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await clientStream.CopyToAsync(fileStream);
                }
            }

            localFile = LocalFileForModPack(modPack);
            if (!IsModPackDownloaded(modPack))
            {
                File.Delete(localFile.FullName);
                throw new Exception("Could not download mod pack archive. Hash may be incorrect.");
            }

            return localFile;
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

            if(modPack.FileHash == null)
            {
                return true;
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
