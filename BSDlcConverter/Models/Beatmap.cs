using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSDlcConverter.Models
{
    public class Beatmap
    {
        public string version { get; set; }
        public Bpmevent[] bpmEvents { get; set; }
        public object[] rotationEvents { get; set; }
        public Colornote[] colorNotes { get; set; }
        public Bombnote[] bombNotes { get; set; }
        public Obstacle[] obstacles { get; set; }
        public Slider[] sliders { get; set; }
        public object[] burstSliders { get; set; }
        public object[] waypoints { get; set; }
        public Basicbeatmapevent[] basicBeatmapEvents { get; set; }
        public Colorboostbeatmapevent[] colorBoostBeatmapEvents { get; set; }
        public Lightcoloreventboxgroup[] lightColorEventBoxGroups { get; set; }
        public Lightrotationeventboxgroup[] lightRotationEventBoxGroups { get; set; }
        public Lighttranslationeventboxgroup[] lightTranslationEventBoxGroups { get; set; }
        public Basiceventtypeswithkeywords basicEventTypesWithKeywords { get; set; }
        public bool useNormalEventsAsCompatibleEvents { get; set; }
    }

    public class Basiceventtypeswithkeywords
    {
        public object[] d { get; set; }
    }

    public class Bpmevent
    {
        public float b { get; set; }
        public float m { get; set; }
    }

    public class Colornote
    {
        public float b { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int a { get; set; }
        public int c { get; set; }
        public int d { get; set; }
    }

    public class Bombnote
    {
        public float b { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Obstacle
    {
        public float b { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public float d { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Slider
    {
        public float b { get; set; }
        public int c { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int d { get; set; }
        public float tb { get; set; }
        public int tx { get; set; }
        public int ty { get; set; }
        public float mu { get; set; }
        public float tmu { get; set; }
        public int tc { get; set; }
        public int m { get; set; }
    }

    public class Basicbeatmapevent
    {
        public float b { get; set; }
        public int et { get; set; }
        public int i { get; set; }
        public float f { get; set; }
    }

    public class Colorboostbeatmapevent
    {
        public float b { get; set; }
        public bool o { get; set; }
    }

    public class Lightcoloreventboxgroup
    {
        public float b { get; set; }
        public int g { get; set; }
        public E[] e { get; set; }
    }

    public class E
    {
        public F f { get; set; }
        public float w { get; set; }
        public int d { get; set; }
        public float r { get; set; }
        public int t { get; set; }
        public int b { get; set; }
        public int i { get; set; }
        public E1[] e { get; set; }
    }

    public class F
    {
        public int f { get; set; }
        public int p { get; set; }
        public int t { get; set; }
        public int r { get; set; }
        public int c { get; set; }
        public int n { get; set; }
        public int s { get; set; }
        public float l { get; set; }
        public int d { get; set; }
    }

    public class E1
    {
        public float b { get; set; }
        public int i { get; set; }
        public int c { get; set; }
        public float s { get; set; }
        public int f { get; set; }
    }

    public class Lightrotationeventboxgroup
    {
        public float b { get; set; }
        public int g { get; set; }
        public E2[] e { get; set; }
    }

    public class E2
    {
        public F1 f { get; set; }
        public float w { get; set; }
        public int d { get; set; }
        public float s { get; set; }
        public int t { get; set; }
        public int a { get; set; }
        public int r { get; set; }
        public int b { get; set; }
        public int i { get; set; }
        public L[] l { get; set; }
    }

    public class F1
    {
        public int f { get; set; }
        public int p { get; set; }
        public int t { get; set; }
        public int r { get; set; }
        public int c { get; set; }
        public int n { get; set; }
        public int s { get; set; }
        public float l { get; set; }
        public int d { get; set; }
    }

    public class L
    {
        public float b { get; set; }
        public int p { get; set; }
        public int e { get; set; }
        public int l { get; set; }
        public float r { get; set; }
        public int o { get; set; }
    }

    public class Lighttranslationeventboxgroup
    {
        public float b { get; set; }
        public int g { get; set; }
        public E3[] e { get; set; }
    }

    public class E3
    {
        public F2 f { get; set; }
        public float w { get; set; }
        public int d { get; set; }
        public float s { get; set; }
        public int t { get; set; }
        public int a { get; set; }
        public int r { get; set; }
        public int b { get; set; }
        public int i { get; set; }
        public L1[] l { get; set; }
    }

    public class F2
    {
        public int f { get; set; }
        public int p { get; set; }
        public int t { get; set; }
        public int r { get; set; }
        public int c { get; set; }
        public int n { get; set; }
        public int s { get; set; }
        public float l { get; set; }
        public int d { get; set; }
    }

    public class L1
    {
        public float b { get; set; }
        public int p { get; set; }
        public int e { get; set; }
        public float t { get; set; }
    }

}

