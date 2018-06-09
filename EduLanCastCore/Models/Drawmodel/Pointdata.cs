using System;

namespace EduLanCastCore.Controllers.Drawcontrol
{
    /// <summary>
    /// 存储屏幕像素点位置的结构体
    /// </summary>
    [Serializable]
    public class Pointdata
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public Pointdata(float xx, float yy, float zz = 0.5f)
        {
            x = xx;
            y = yy;
            z = zz;
        }
        public Pointdata(Pointdata p)
        {
            x = p.x;
        }
    }
}