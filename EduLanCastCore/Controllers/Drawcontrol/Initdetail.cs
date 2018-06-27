using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using Device = SharpDX.Direct3D11.Device;
using Resource = SharpDX.Direct3D11.Resource;

namespace EduLanCastCore.Controllers.Drawcontrol
{
    /// <summary>
    /// 设置渲染屏幕对象与上下文对象
    /// </summary>
    public class Initdetail:IDisposable
    {
        public Device Device;
        public SwapChain Swapchain;
        public DeviceContext Devicecontext { get; private set; }
        public RenderTargetView RenderTarget { get; private set; }
        public IntPtr OutputhandlePtr { get; private set; }
        private float Width { get; }
        private float Height { get; }
        /// <summary>
        /// public构造函数实例化一个对象
        /// </summary>
        /// <param name="handle">屏幕对象指针</param>
        /// <param name="height">屏幕高度</param>
        /// <param name="width">屏幕宽度</param>
        public Initdetail(IntPtr handle, float height, float width)
        {
            Height = height;
            Width = width;
            SetPtr(handle);
            Createdevice();
            Setoutput();
        }
        /// <summary>
        /// 设置Devicecontext的渲染对象
        /// </summary>
        private void Setoutput()
        {
            RenderTarget?.Dispose();
            using (var resource = Resource.FromSwapChain<Texture2D>(Swapchain, 0))
            {
                RenderTarget = new RenderTargetView(Device, resource);
            }

            Devicecontext = Device.ImmediateContext;
            Devicecontext.Rasterizer.SetViewport(0.0f, 0.0f, Width, Height);
        }
        /// <summary>
        /// 获得显卡Device
        /// </summary>
        private void Createdevice()
        {
            Device?.Dispose();
            var description = new SwapChainDescription
            {
                BufferCount = 2,
                Usage = Usage.RenderTargetOutput,
                OutputHandle = OutputhandlePtr,
                IsWindowed = true,
                ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                SampleDescription = new SampleDescription(1, 0),
                Flags = SwapChainFlags.AllowModeSwitch,
                SwapEffect = SwapEffect.Discard
            };

            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.Debug, description, out Device, out Swapchain);
        }
        /// <summary>
        /// 设置Ptr
        /// </summary>
        /// <param name="handle">屏幕对象指针</param>
        private void SetPtr(IntPtr handle)
        {
            OutputhandlePtr = handle;
        }
        /// <summary>
        /// 释放所有构建的对象：device,swapchain,devicecontext,rendertarget
        /// </summary>
        public void Dispose()
        {
            Device.Dispose();
            Swapchain.Dispose();
            Devicecontext.Dispose();
            RenderTarget.Dispose();
        }
    }
}
