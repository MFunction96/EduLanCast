using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;

namespace EduLanCastCore.Controllers.Drawcontrol.DrawingFunc
{
    class Chalk : Drawing, IDisposable
    {
        public Chalk(Initdetail init, RawColor4 color4)
        {
            this.color4 = color4;
            shaderfile = "chalk.fx";
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
