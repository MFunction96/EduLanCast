using System.Collections.Generic;

namespace EduLanCastCore.Controllers.Drawcontrol
{
    public class Strokedata
    {
        public int Type { get; set; }
        public int Line { get; set; }
        public List<Pointdata> Plist { get; set; }
        public int Index { get; set; }

        public Strokedata(List<Pointdata> p, int type = 1, int line = 1, int index = 0)
        {
            Type = type;
            Line = line;
            Plist = new List<Pointdata>();
            Plist = p;
            Index = index;
        }

        public Strokedata(Strokedata strokedata)
        {
            Type = strokedata.Type;
            Line = strokedata.Line;
            Index = strokedata.Index;
            Plist = new List<Pointdata>(strokedata.Plist);
        }

        public void SetTl(int t, int l)
        {
            Type = t;
            Line = l;
        }
        public void Clear()
        {
            Plist.Clear();
            Type = 1;
            Line = 1;
            Index = 0;
        }
    }
}