using System;
using System.Collections.Generic;

namespace EduLanCastCore.Controllers.Drawcontrol
{
    class MathTool
    {
        public static List<Pointdata> GetCircle(Pointdata p, Strokedata s, int precise)
        {
            List<Pointdata> pointlist = new List<Pointdata>();
            for (float i = 0; i < precise * 2; i++)
            {
                pointlist.Add(new Pointdata(GetRelateX((float)(GetRealX(p) + s.Line * (1f) * Math.Cos(Math.PI * i / precise))), GetRelateY((float)(GetRealY(p) + s.Line * (1f) * Math.Sin(Math.PI * i / precise)))));
            }
            return pointlist;
        }

        public static float GetRealY(Pointdata p)
        {
            return Canvainfo.width * p.y / 2;
        }

        public static float GetRealX(Pointdata p)
        {
            return Canvainfo.width * p.x / 2;
        }

        public static float GetRelateY(float y)
        {
            return 2 * y / Canvainfo.width;
        }

        public static float GetRelateX(float x)
        {
            return 2 * x / Canvainfo.height;
        }

        public static bool ClockWise(Pointdata a, Pointdata b, Pointdata c)
        {
            //empty
            return true;
        }
        private static Pointdata GetPoint(Pointdata last, Pointdata p, int line,String abcd) {
            float rotate1Cos = (GetRealX(p) - GetRealX(last)) / (float)Math.Sqrt(Math.Pow(GetRealX(p) - GetRealX(last), 2) + Math.Pow(GetRealY(p) - GetRealY(last), 2));
            float rotate1Sin = (GetRealY(p) - GetRealY(last)) / (float)Math.Sqrt(Math.Pow(GetRealX(p) - GetRealX(last), 2) + Math.Pow(GetRealY(p) - GetRealY(last), 2));
            float rotate2Cos = rotate1Cos;
            float rotate2Sin = -rotate1Sin;
            float newcenX = GetRealX(last) * rotate1Cos - GetRealY(last) * rotate1Sin;
            float newcenY = GetRealX(last) * rotate1Sin + GetRealY(last) * rotate1Cos;
            float a1X = newcenX * rotate2Cos - (newcenY + line * 1f) * rotate2Sin;
            float a2X = newcenX * rotate2Cos - (newcenY - line * 1f) * rotate2Sin;
            float ax, ay;
            if (abcd == "a" || abcd == "c")
            {
                ax = a1X < a2X ? a1X : a2X;
                ay = newcenX * rotate2Sin + (newcenY - line * (1f)) * rotate2Cos;
                return new Pointdata(GetRelateX(ax), GetRelateY(ay));
            }
            else if (abcd == "b" || abcd == "d")
            {
                ax = a1X > a2X ? a1X : a2X;
                ay = newcenX * rotate2Sin + (newcenY - line * (1f)) * rotate2Cos;
                return new Pointdata(GetRelateX(ax), GetRelateY(ay));
            }
            else {
                throw new EntryPointNotFoundException();
            }
        }
        public static Pointdata GetpointA(Pointdata last, Pointdata p, int line)
        {
            return GetPoint(last, p, line, "a");
            
        }

        public static Pointdata GetPointB(Pointdata last, Pointdata p, int line)
        {
            return GetPoint(last, p, line, "b");
        }

        public static Pointdata GetpointC(Pointdata last, Pointdata p, int line)
        {
            return GetPoint(last, p, line, "c");
        }

        public static Pointdata GetpointD(Pointdata last, Pointdata p, int line)
        {
            return GetPoint(last,p,line,"d");
        }
    }
}