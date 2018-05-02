using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using EduLanCastOld.Data;
using EduLanCastOld.Services;
using SharpDX.Direct3D;
using Device = SharpDX.Direct3D11.Device;

namespace EduLanCastOld.Controllers.Capturer
{
    public class DesktopDuplication : ITerminate,IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public Factory1 Factory { get; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Adapter1> Adapters1 { get; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Output> Outputs { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Rectangle ScreenDpi { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        protected Adapter1 SelectedAdapter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected Output SelectedOutput { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected Device Device { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected Texture2DDescription TextureDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected Bitmap Bitmap;
        /// <summary>
        /// 
        /// </summary>
        protected int Interval { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected OutputDuplication DuplicatedOutput { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected Texture2D ScreenTexture { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DesktopDuplication()
        {
            Factory = new Factory1();
            Adapters1 = Factory.Adapters1;
            StaticData.ThreadMgr.ManageObject["CaptureScreen"] = new Thread(Duplicate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter1"></param>
        public void QueryOutputs(Adapter1 adapter1)
        {
            Outputs = adapter1.Outputs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public Rectangle SelectOutput(Output output)
        {
            var width = output.Description.DesktopBounds.Right - output.Description.DesktopBounds.Left;
            var height = output.Description.DesktopBounds.Bottom - output.Description.DesktopBounds.Top;
            return ScreenDpi = new Rectangle(0, 0, width, height);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter1"></param>
        /// <param name="output"></param>
        /// <param name="bitmap"></param>
        /// <param name="interval"></param>
        public void InitDuplication(Adapter1 adapter1, Output output, ref Bitmap bitmap, int interval)
        {
            SelectedAdapter = adapter1;
            SelectedOutput = output;
            if (!(Enum.GetValues(typeof(DriverType)) is int[] drivertypes)) throw new NullReferenceException();
            var featurelevels = new[]
            {
                FeatureLevel.Level_11_0,
                FeatureLevel.Level_10_1,
                FeatureLevel.Level_10_0,
                FeatureLevel.Level_9_1
            };
            foreach (var dt in drivertypes)
            {
                try
                {
                    Device = new Device((DriverType) dt, DeviceCreationFlags.None, featurelevels);

                }
                catch (Exception)
                {
                    // ignored
                }
            }
            
            TextureDesc = new Texture2DDescription
            {
                CpuAccessFlags = CpuAccessFlags.Read,
                BindFlags = BindFlags.None,
                Format = Format.B8G8R8A8_UNorm,
                Width = ScreenDpi.Width,
                Height = ScreenDpi.Height,
                OptionFlags = ResourceOptionFlags.None,
                MipLevels = 1,
                ArraySize = 1,
                SampleDescription = { Count = 1, Quality = 0 },
                Usage = ResourceUsage.Staging
            };
            ScreenTexture = new Texture2D(Device, TextureDesc);
            DuplicatedOutput = SelectedOutput.QueryInterface<Output1>().DuplicateOutput(Device);
            Bitmap = bitmap;
            Interval = interval;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Duplicate()
        {
            try
            {
                DuplicatedOutput.AcquireNextFrame(10000, out _, out var screenresource);
                var screenTexture2D = screenresource.QueryInterface<Texture2D>();
                Device.ImmediateContext.CopyResource(screenTexture2D, ScreenTexture);
                var mapSource = Device.ImmediateContext.MapSubresource(ScreenTexture, 0, MapMode.Read, SharpDX.Direct3D11.MapFlags.None);
                Bitmap = new Bitmap(ScreenDpi.Width, ScreenDpi.Height, PixelFormat.Format32bppArgb);
                var boundsRect = new Rectangle(0, 0, ScreenDpi.Width, ScreenDpi.Height);
                var mapDest = Bitmap.LockBits(boundsRect, ImageLockMode.WriteOnly, Bitmap.PixelFormat);
                var sourcePtr = mapSource.DataPointer;
                var destPtr = mapDest.Scan0;
                for (var y = 0; y < ScreenDpi.Height; y++)
                {
                    // Copy a single line 
                    Utilities.CopyMemory(destPtr, sourcePtr, ScreenDpi.Width << 2);

                    // Advance pointers
                    sourcePtr = IntPtr.Add(sourcePtr, mapSource.RowPitch);
                    destPtr = IntPtr.Add(destPtr, mapDest.Stride);
                    // Release source and dest locks
                    Bitmap.UnlockBits(mapDest);
                    StaticData.Buffer.Enqueue(Bitmap);
                    Bitmap.Dispose();
                    Device.ImmediateContext.UnmapSubresource(ScreenTexture, 0);
                }
                screenresource.Dispose();
                DuplicatedOutput.ReleaseFrame();
            }
            catch (SharpDXException e)
            {
                if (e.ResultCode.Code != SharpDX.DXGI.ResultCode.WaitTimeout.Result.Code)
                {
                    throw;
                }
            }
            Thread.Sleep(Interval);
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public Task Terminate()
        {
            return Task.Run(() => { StaticData.ThreadMgr.ManageObject["CaptureScreen"].Interrupt(); });
        }
        /// <summary>
        /// 析构函数。
        /// </summary>
        ~DesktopDuplication()
        {
            Dispose(false);
        }
        /// <summary>
        /// 根据析构类型，选取析构方式。
        /// </summary>
        /// <param name="disposing">
        /// true表示主动析构。
        /// false表示被动析构。
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (!disposing) return;
            Bitmap?.Dispose();
            Factory?.Dispose();
            SelectedAdapter?.Dispose();
            SelectedOutput?.Dispose();
            Device?.Dispose();
            DuplicatedOutput?.Dispose();
            ScreenTexture?.Dispose();
        }
        /// <summary>
        /// 释放非托管资源。
        /// </summary>
        protected void ReleaseUnmanagedResources()
        {
            
        }
        /// <inheritdoc />
        /// <summary>
        /// 析构对象。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
