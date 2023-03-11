using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DlcConverter.Models
{
    public class InfoModel
    {
        public string _version { get; set; } = "2.0.0";
        public string _songName { get; set; }
        public string _songSubName { get; set; }
        public string _songAuthorName { get; set; }
        public string _levelAuthorName { get; set; } = "rippedDLC";
        public float _beatsPerMinute { get; set; }
        public float _songTimeOffset { get; set; } = 0.0f;
        public float _shuffle { get; set; } = 0.0f;
        public float _shufflePeriod { get; set; } = 0.0f;
        public float _previewStartTime { get; set; } = 0.0f;
        public float _previewDuration { get; set; } = 30.0f;
        public string _songFilename { get; set; } = "song.ogg";
        public string _coverImageFilename { get; set; } = "cover.jpg";
        public string _environmentName { get; set; }
        public string _allDirectionsEnvironmentName { get; set; } = "GlassDesertEnvironment";
        public _Difficultybeatmapsets[] _difficultyBeatmapSets { get; set; }
    }

    public class _Difficultybeatmapsets
    {
        public string _beatmapCharacteristicSerializedName { set { _beatmapCharacteristicName = value; } }
        public string _beatmapCharacteristicName { get; set; }
        public _Difficultybeatmaps[] _difficultyBeatmaps { get; set; }
    }

    public class _Difficultybeatmaps
    {
        public string _difficulty { get; set; }
        public int _difficultyRank { get; set; }
        public string _beatmapFilename { get; set; }
        public float _noteJumpMovementSpeed { get; set; }
        public float _noteJumpStartBeatOffset { get; set; }
    }

}
