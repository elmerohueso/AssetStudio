using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DlcConverter.Models
{

    public class SongPackDefinitions
    {
        public Song[] songs { get; set; }
    }

    public class Song
    {
        public string internalName { get; set; }
        public string songName { get; set; }
        public string songAuthorName { get; set; }
        public string beatsPerMinute { get; set; }
        public string songPack { get; set; }
        public string songSubName { get; set; }
        public string nameOverride { get; set; }
    }

}
