using EduLanCastCore.Controllers.Drawcontrol.DrawingFunc;
using System;
using System.Collections.Generic;
using EduLanCastCore.Models.Drawmodel;

namespace EduLanCastCore.Controllers.Drawcontrol
{
    public class Pointtrace
    {
        //private int _interval;
        public static int Flag;
        private static readonly Object Flaglock = new Object();
        public static int Pointcount;

        public static List<Strokedata> Allline { get; set; } = new List<Strokedata>();
        public static Strokedata Stroke { get; set; } = new Strokedata(new List<Pointdata>());

        //Pointtrace(int interval)
        //{
        //    //_interval = interval;
        //}

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
                    Allline.Add(new Strokedata(Stroke));
                    Stroke.Clear();
                }
            }
        }

        public static void AddPoint(float x, float y)
        {
            if (Flag == 1)
            {
                Pointcount = Stroke.Plist.Count;
                if (!Tooltype.IsonlyClickTool())
                {
                    if (Pointcount > 0)
                    {
                        if (!Stroke.Plist[Pointcount - 1].X.Equals(x) || !Stroke.Plist[Pointcount - 1].Y.Equals(y))
                        {
                            Stroke.Plist.Add(new Pointdata(x, y));
                        }
                    }
                    else
                    {
                        Stroke.SetTl(Tooltype.Type, Tooltype.Line);
                        Stroke.Plist.Add(new Pointdata(x, y));
                    }
                }
            }
        }
        public static Strokedata GetPointlist()
        {
            return Stroke;
        }
        public static List<Strokedata> GetAllPoint()
        {
            return Allline;
        }
    }
}
