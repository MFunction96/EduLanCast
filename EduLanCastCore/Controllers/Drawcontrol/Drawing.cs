
using Device = SharpDX.Direct3D11.Device;
using Buffer = SharpDX.Direct3D11.Buffer;
using SharpDX.DXGI;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX;
using SharpDX.Mathematics.Interop;
using System.Collections.Generic;
using System;
using SharpDX.Direct3D;

namespace EduLanCastCore.Controllers.Drawcontrol
{
    public class Drawing
    {
        protected Device _device;
        protected SwapChain _swapchain;
        protected ShaderSignature InputSingnature { get; set; }
        protected VertexShader Vertexshader;
        protected PixelShader Pixelshader;
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
            _swapchain.Present(0, PresentFlags.None);
        }
        public virtual void Render(List<Strokedata> strokelist, Strokedata stroke)
        {
            //virtual
        }
        protected void Createvertex(Strokedata stroke)
        {
            Pointdata a = null, b = null, c = null, d = null;
            lock (stroke.Plist)
            {
                int count = 0;
                int precise = 5;
                List<Pointdata> list = CloneTool.Clone(stroke.Plist);
                Pointdata last = list[0];
                for (int i = 0; i < list.Count; i++)
                {
                    Pointdata p = list[i];
                    if (i >= stroke.Index)
                    {
                        List<Pointdata> pointCircle = MathTool.GetCircle(p, stroke, precise);
                        for (int j = 0; j < precise * 2; j++)
                        {
                            TriangleContext(p, pointCircle[(j + 1) % (precise * 2)], pointCircle[j]);
                        }
                        if (count > 0)
                        {
                            if (last.y.Equals(p.y))
                            {
                                a = new Pointdata(last.x, MathTool.GetRelateY(MathTool.GetRealX(last) + stroke.Line * 1f));
                                b = new Pointdata(last.x, MathTool.GetRelateY(MathTool.GetRealX(last) - stroke.Line * 1f));
                                c = new Pointdata(p.x, a.y);
                                d = new Pointdata(p.x, b.y);
                            }
                            else
                            {
                                a = MathTool.GetpointA(last, p, stroke.Line);
                                b = MathTool.GetPointB(last, p, stroke.Line);
                                c = MathTool.GetpointC(last, p, stroke.Line);
                                d = MathTool.GetpointD(last, p, stroke.Line);
                            }
                            if (a != null)
                            {
                                TriangleContext(a, c, b);
                                TriangleContext(a, b, c);
                                TriangleContext(c, d, b);
                                TriangleContext(c, b, d);
                            }
                        }
                        count++;
                        last = p;
                        stroke.Index = i;
                    }
                }
            }
        }

        private void TriangleContext(Pointdata a, Pointdata b, Pointdata c)
        {
            if (Vertices != null)
            {
                Vertices.Dispose();
                Layout.Dispose();
                Vertexbuffer.Dispose();
            }
            Vertices = new DataStream(12 * 3, true, true);
            Vertices.Write(new RawVector3(a.x, a.y, a.z));
            Vertices.Write(new RawVector3(b.x, b.y, b.z));
            Vertices.Write(new RawVector3(c.x, c.y, c.z));
            Vertices.Position = 0;

            var elements = new[] { new InputElement("Position", 0, Format.R32G32B32A32_Float, 0) };
            Layout = new InputLayout(_device, InputSingnature, elements);
            Vertexbuffer = new Buffer(_device, Vertices, 12 * 3, ResourceUsage.Default, BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);

            Devicecontext.InputAssembler.InputLayout = Layout;
            Devicecontext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            Devicecontext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(Vertexbuffer, 12, 0));

            Devicecontext.VertexShader.Set(Vertexshader);
            Devicecontext.PixelShader.Set(Pixelshader);

            Devicecontext.Draw(3, 0);
        }

        protected void Compilepixel()
        {
            if (Pixelshader != null)
            {
                Pixelshader.Dispose();
            }

            using (var bytecode = ShaderBytecode.CompileFromFile(shaderfile, "PShader", "ps_4_0", ShaderFlags.None, EffectFlags.None))
            {
                Pixelshader = new PixelShader(_device, bytecode);
            }
        }
        protected void Compilevertex()
        {
            if (Vertexshader != null)
            {
                Vertexshader.Dispose();
            }

            using (var bytecode = ShaderBytecode.CompileFromFile(shaderfile, "VShader", "vs_5_0", ShaderFlags.None, EffectFlags.None))
            {
                InputSingnature = ShaderSignature.GetInputOutputSignature(bytecode);
                Vertexshader = new VertexShader(_device, bytecode);
            }
        }
        public void Init(Initdetail init)
        {
            _device = init._device;
            _swapchain = init._swapchain;
            Devicecontext = init.Devicecontext;
            Rendertarget = init.RenderTarget;
            Compilevertex();
            Compilepixel();
        }
        protected void Clean()
        {
            Vertices.Close();
            Vertexbuffer.Dispose
                ();
            Layout.Dispose();
            InputSingnature.Dispose();
            Vertexshader.Dispose();
            Pixelshader.Dispose();
            Rendertarget.Dispose();
            _swapchain.Dispose();
            _device.Dispose();
        }
    }
}
