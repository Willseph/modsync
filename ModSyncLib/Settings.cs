using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModSyncLib
{
    public class Settings
    {
        public Dictionary<string, GameConfig> Games { get; set; } = new Dictionary<string, GameConfig>();
        public Dictionary<string, ModPack> ModPacks { get; set; } = new Dictionary<string, ModPack>();

        public static Settings Default { get => new(); }
    }
}
