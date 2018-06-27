using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using System.Collections.Generic;
using EduLanCastCore.Models.Drawmodel;

namespace EduLanCastCore.Controllers.Drawcontrol.DrawingFunc
{
    class Chalk : Blackboard
    {
        public Chalk(Initdetail init, RawColor4 color4)
        {
            Rcolor4 = color4;
            Shaderfile = "chalk.fx";
            Init(init);
        }
        override
            public void Render(List<Strokedata> strokelist, Strokedata stroke)
        {
            Shaderfile = Tooltype.Getfx(stroke.Type);
            Compilevertex();
            Compilepixel();
            if (stroke.Plist.Count > 0)
            {
                Createvertex(stroke);
            }
            Swapchain.Present(0, PresentFlags.None);
        }
    }
}
