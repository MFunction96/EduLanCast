using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLanCastCore.Controllers.Drawcontrol.DrawingFunc
{
    class Eraser : Drawing, IDisposable
    {
        public Eraser(Initdetail init, RawColor4 color4)
        {
            this.color4 = color4;
            shaderfile = "eraser.fx";
            Init(init);
        }
        override
            public void Render(List<Strokedata> strokelist, Strokedata stroke)
        {
            shaderfile = Tooltype.Getfx(stroke.Type);
            Compilevertex();
            Compilepixel();
            if (stroke.Plist.Count > 0)
            {
                Createvertex(stroke);
            }
            _swapchain.Present(0, PresentFlags.None);
        }
        public void Dispose()
        {
            Clean();
        }
    }
}
