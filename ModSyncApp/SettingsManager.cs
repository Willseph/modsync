using ModSyncLib;
using Newtonsoft.Json;

namespace ModSync
{
    public static class SettingsManager
    {
        static readonly string Filename = "settings.json";
        static FileInfo SettingsFile {
            get => new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), Filename));
        }

        public static Settings Settings
        {
            get {
                if (SettingsFile.Exists)
                {
                    try
                    {
                        var content = File.ReadAllText(SettingsFile.FullName);
                        var result = JsonConvert.DeserializeObject<Settings>(content);
                        return result ?? Settings.Default;
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex);
                    }
                }

                return Settings.Default;
            }

            set {
                var json = JsonConvert.SerializeObject(value);
                File.WriteAllText(SettingsFile.FullName, json);
            }
        }
    }
}
