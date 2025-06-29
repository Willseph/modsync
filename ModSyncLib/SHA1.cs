using System.Security.Cryptography;
using System.Text;

namespace ModSyncLib
{
    public static class SHA1Helper
    {
        public static string? FileSHA1(FileInfo fileInfo)
        {
            if(fileInfo?.Exists != true) return null;
            string? result = null;

            using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open))
            using (BufferedStream bs = new BufferedStream(fs))
            using (SHA1Managed sha1 = new SHA1Managed()) {
                byte[] hash = sha1.ComputeHash(bs);
                StringBuilder formatted = new StringBuilder(2 * hash.Length);
                foreach (byte b in hash)
                {
                    formatted.AppendFormat("{0:X2}", b);
                }
                result = formatted.ToString().ToLowerInvariant();
            }

            return result;
        }
    }
}
