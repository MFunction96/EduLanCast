using EduLanCastCore.Controllers.Drawcontrol.DrawingFunc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLanCastCore.Controllers.Drawcontrol
{
    class Pointtrace
    {
        private int Interval;
        public static int Flag;
        private static readonly Object Flaglock = new Object();
        public static int Pointcount = 0;
        private static Strokedata _stroke = new Strokedata(new List<Pointdata>());
        private static List<Strokedata> Allline = new List<Strokedata>();

        Pointtrace(int interval)
        {
            Interval = interval;
        }

        public static void Pressdown()
        {
            lock (Flaglock)
            {
                Flag = 1;
            }
        }
        public static void Pressup()
        {
            lock (Flaglock)
            {
                Flag = 0;
                if (!Tooltype.IsonlyClickTool())
                {
                    Allline.Add(new Strokedata(_stroke));
                    _stroke.Clear();
                }
            }
        }

        public static void AddPoint(float x, float y)
        {
            if (Flag == 1)
            {
                Pointcount = _stroke.Plist.Count;
                if (!Tooltype.IsonlyClickTool())
                {
                    if (Pointcount > 0)
                    {
                        if (!_stroke.Plist[Pointcount - 1].x.Equals(x) || !_stroke.Plist[Pointcount - 1].y.Equals(y))
                        {
                            _stroke.Plist.Add(new Pointdata(x, y));
                        }
                    }
                    else
                    {
                        _stroke.SetTl(Tooltype.Type, Tooltype.Line);
                        _stroke.Plist.Add(new Pointdata(x, y));
                    }
                }
            }
        }
        public static Strokedata GetPointlist()
        {
            return _stroke;
        }
        public static List<Strokedata> GetAllPoint()
        {
            return Allline;
        }
    }
}
