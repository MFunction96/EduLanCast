
using Device = SharpDX.Direct3D11.Device;
using Buffer = SharpDX.Direct3D11.Buffer;
using SharpDX.DXGI;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX;
using SharpDX.Mathematics.Interop;
using System.Collections.Generic;
using System;

namespace EduLanCastCore.Controllers.Drawcontrol
{
    public class Drawing
    {
        protected Device _device;
        protected SwapChain _swapchain;
        protected ShaderSignature InputSingnature { get; set; }
        protected VertexShader VertexShader;
        protected PixelShader PixelShader;
        protected List<Pointdata> Pointlist { get; set; }
        protected DeviceContext Devicecontext { get; private set; }
        public int Num { get; private set; }
        public DataStream Vertices { get; private set; }
        public InputLayout Layout { get; private set; }
        public Buffer Vertexbuffer { get; private set; }
        public RenderTargetView Rendertarget { get; private set; }
        public RawColor4 color4;
        public String shaderfile { get; set; }

        public void Cleancanvas()
        {
            Devicecontext.ClearRenderTargetView(Rendertarget, color4);
        }
        public virtual void Render(List<Strokedata> strokelist, Strokedata stroke)
        {

        }


    }
}
