using EduLanCastCore.Controllers.Utils;
using EduLanCastCore.Models.Configs;
using EduLanCastCore.Models.Duplicators;
using EduLanCastCore.Services;
using EduLanCastCore.Services.Enums;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;

namespace EduLanCastCore.Controllers.Threads
{
    public class DuplicateThread : ServiceThread
    {
        /// <summary>
        /// 桌面复制执行委托。
        /// </summary>
        public Action<ConcurrentQueue<BitMapCollection>> DuplAction { get; set; }
        /// <summary>
        /// 线程同步容器。
        /// </summary>
        public ConcurrentQueue<BitMapCollection> DuplBuffer { get; set; }

        protected bool Duplicating { get; set; }

        protected AppConfig Config;

        protected DxModel DxModel;

        protected int Interval => Config.Fps == 0 ? 1000 : 1000 / Config.Fps;

        public DuplicateThread(ref AppConfig config, ref DxModel dxModel)
        {
            Config = config;
            DxModel = dxModel;
            DuplBuffer = new ConcurrentQueue<BitMapCollection>();
        }

        public new void Start()
        {
            Initialization();
            Duplicating = true;
            NativeMethods.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | 
                                                  EXECUTION_STATE.ES_SYSTEM_REQUIRED | 
                                                  EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
            base.Start();
        }

        public override void Operation()
        {
            while (Duplicating)
            {
                SharpDX.DXGI.Resource desktopResource;

                try
                {
                    DxModel.DuplicatedOutput.AcquireNextFrame(1000, out DxModel.FrameInfo, out desktopResource);
                }

                catch (SharpDXException e) when (e.ResultCode.Failure)
                {
                    throw new Exception("Failed to acquire next frame.", e);
                }

                using (desktopResource)
                {
                    using (var tempTexture = desktopResource.QueryInterface<Texture2D>())
                    {
                        var resourceRegion = new ResourceRegion(DxModel.ScreenDpi.Left, DxModel.ScreenDpi.Top, 0, DxModel.ScreenDpi.Right, DxModel.ScreenDpi.Bottom, 1);

                        DxModel.Device.ImmediateContext.CopySubresourceRegion(tempTexture, 0, resourceRegion, DxModel.TextureDesc, 0);
                    }
                }

                ReleaseFrame();

                var mapSource = DxModel.Device.ImmediateContext.MapSubresource(DxModel.TextureDesc, 0, MapMode.Read, MapFlags.None);

                try
                {
                    DuplBuffer.Enqueue(new BitMapCollection
                    {
                        Picture = ProcessFrame(mapSource.DataPointer, mapSource.RowPitch),
                        TimeStamp = DateTime.Now
                    });
                }
                finally
                {
                    DxModel.Device.ImmediateContext.UnmapSubresource(DxModel.TextureDesc, 0);
                }
                DuplAction(DuplBuffer);
                Thread.Sleep(Interval);
            }
        }

        public new void Interrupt()
        {
            Duplicating = false;
            NativeMethods.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }

        protected void Initialization()
        {
            DxModel.Device = new Device(DxModel.SelectedAdapter);
            var output1 = DxModel.SelectedOutput.QueryInterface<Output1>();
            var textureDesc = new Texture2DDescription
            {
                CpuAccessFlags = CpuAccessFlags.Read,
                BindFlags = BindFlags.None,
                Format = Format.B8G8R8A8_UNorm,
                Width = DxModel.ScreenDpi.Width,
                Height = DxModel.ScreenDpi.Height,
                OptionFlags = ResourceOptionFlags.None,
                MipLevels = 1,
                ArraySize = 1,
                SampleDescription = { Count = 1, Quality = 0 },
                Usage = ResourceUsage.Staging
            };
            try
            {
                DxModel.DuplicatedOutput = output1.DuplicateOutput(DxModel.Device);
            }
            catch (SharpDXException e)
                when (e.Descriptor == SharpDX.DXGI.ResultCode.NotCurrentlyAvailable)
            {
                ErrorUtil.WriteError(e);
                throw new Exception("There is already the maximum number of applications using the Desktop Duplication API running, please close one of the applications and try again.", e);
            }
            catch (SharpDXException e)
                when (e.Descriptor == SharpDX.DXGI.ResultCode.Unsupported)
            {
                ErrorUtil.WriteError(e);
                throw new NotSupportedException("Desktop Duplication is not supported on this system.\nIf you have multiple graphic cards, try running Captura on integrated graphics.", e);
            }

            DxModel.ScreenTexture = new Texture2D(DxModel.Device, textureDesc);
        }

        /// <summary>
        /// 提取运算图形框架。
        /// </summary>
        /// <param name="sourcePtr">
        /// 首指针。
        /// </param>
        /// <param name="sourceRowPitch">
        /// 指针长度。
        /// </param>
        /// <returns>
        /// 位图像。
        /// </returns>
        protected Bitmap ProcessFrame(IntPtr sourcePtr, int sourceRowPitch)
        {
            var frame = new Bitmap(DxModel.ScreenDpi.Width, DxModel.ScreenDpi.Height, PixelFormat.Format32bppRgb);

            // Copy pixels from screen capture Texture to GDI bitmap
            var mapDest = frame.LockBits(new Rectangle(0, 0, DxModel.ScreenDpi.Width, DxModel.ScreenDpi.Height), ImageLockMode.WriteOnly, frame.PixelFormat);

            Parallel.For(0, DxModel.ScreenDpi.Height, y =>
            {
                Utilities.CopyMemory(mapDest.Scan0 + y * mapDest.Stride,
                    sourcePtr + y * sourceRowPitch,
                    DxModel.ScreenDpi.Width * 4);
            });

            // Release source and dest locks
            frame.UnlockBits(mapDest);

            if (!DxModel.FrameInfo.PointerPosition.Visible) return frame;
            using (var g = Graphics.FromImage(frame))
                MouseCursor.Draw(g, p => new Point(p.X - DxModel.ScreenDpi.X, p.Y - DxModel.ScreenDpi.Y));

            return frame;
        }
        /// <summary>
        /// 释放图形框架。
        /// </summary>
        protected void ReleaseFrame()
        {
            try
            {
                DxModel.DuplicatedOutput.ReleaseFrame();
            }
            catch (SharpDXException e)
            {
                if (e.ResultCode.Failure)
                {
                    ErrorUtil.WriteError(e);
                    throw new Exception("Failed to release frame.", e);
                }
            }
        }

        public new void Dispose()
        {
            try
            {
                Interrupt();
            }
            catch (Exception)
            {
                // ignore
            }
            
            DxModel?.Dispose();
            base.Dispose();
        }
    }
}
