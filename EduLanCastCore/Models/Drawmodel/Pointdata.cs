using System;

namespace EduLanCastCore.Controllers.Drawcontrol
{
    /// <summary>
    /// 存储屏幕像素点位置的结构体
    /// </summary>
    [Serializable]
    public class Pointdata
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Pointdata(float xx, float yy, float zz = 0.5f)
        {
            X = xx;
            Y = yy;
            Z = zz;
        }
        public Pointdata(Pointdata p)
        {
            X = p.X;
        }
    }
}