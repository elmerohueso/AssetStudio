using System.Collections.Generic;

namespace BSDlcConverter.Models
{

    public class SongPackDefinitions
    {
        public Song[] songs { get; set; }
        public Dictionary<string, SongPack> songPacks { get; set; }
    }

    public class Song
    {
        public string internalName { get; set; }
        public string songName { get; set; }
        public string songSubName { get; set; }
        public string songAuthorName { get; set; }
        public string beatsPerMinute { get; set; }
        public string songPack { get; set; }
        public string nameOverride { get; set; }
    }
    public class SongPack
    {
        public string cover { get; set; }
        public string environment { get; set; }
    }
}
