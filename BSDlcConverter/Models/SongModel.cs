using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DlcConverter.Models
{
    public class SongModel
    {
        public string internalName { get; set; }
        public string nameOverride { get; set; }
        public string version { get; set; } = "2.0.0";
        public string songName { get; set; }
        public string songSubName { get; set; } = "";
        public string songAuthorName { get; set; } = "";
        public string levelAuthorName { get; set; } = "";
        public double beatsPerMinute { get; set; }
        public double previewStartTime { get; set; } = 0.0;
        public int previewDuration { get; set; } = 30;
        public string environmentName { get; set; } = "FitBeatEnvironment";
        public string songPack { get; set; }
        public string songPath { get; set; }
        public string coverPath { get; set; }
        public List<string> beatMapFiles { get; set; } = new List<string>();
        public string levelDataPath { get; set; }
    }
}
