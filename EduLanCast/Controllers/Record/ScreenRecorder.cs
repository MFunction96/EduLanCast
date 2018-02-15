using System;
using System.Drawing;
using System.Windows.Forms;

namespace EduLanCast.Controllers.Record
{
    public class ScreenRecorder
    {
        public int Fps { get; set; }
        private readonly Bitmap _capture;

        public ScreenRecorder()
        {
            _capture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }

        public Bitmap Refrush()
        {
            var g = Graphics.FromImage(_capture);
            g.CopyFromScreen(0, 0, 0, 0, _capture.Size);
            GC.Collect();
            return _capture;
        }
    }
}
