using EduLanCastCore.Data;
using EduLanCastCore.Interfaces;
using EduLanCastCore.Models.Duplicators;
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

namespace EduLanCastCore.Controllers.Duplicators
{
    /// <inheritdoc cref="ITerminate"/>
    /// <inheritdoc cref="IDisposable" />
    /// <summary>
    /// 桌面复制类。
    /// </summary>
    public class Duplication : IDisposable, ITerminate
    {
        /// <summary>
        /// 桌面复制执行委托。
        /// </summary>
        public Action<BlockingCollection<BitMapCollection>> DuplAction { get; set; }
        /// <summary>
        /// 线程同步容器。
        /// </summary>
        private BlockingCollection<BitMapCollection> DuplBuffer { get; }
        /// <summary>
        /// DirectX模块。
        /// </summary>
        public DxModel DxModel { get; }
        /// <summary>
        /// 桌面分辨率。
        /// </summary>
        public Rectangle ScreenDpi { get; private set; }
        /// <summary>
        /// 线程执行间隔。
        /// </summary>
        protected int Interval { get; set; }
        /// <summary>
        /// 复制线程运行状态。
        /// </summary>
        protected bool Duplicating;
        /// <summary>
        /// 桌面复制类构造函数。
        /// </summary>
        public Duplication()
        {
            DuplBuffer = new BlockingCollection<BitMapCollection>();
            DxModel = new DxModel();
            StaticData.ThreadMgr.ManageObject["Duplicator"] = new Thread(Capture);
        }
        /// <summary>
        /// 选择适配器。
        /// </summary>
        /// <param name="adapter">
        /// 适配器名称。
        /// </param>
        public void SelectAdapter(string adapter)
        {
            DxModel.SelectAdapter(adapter);
        }
        /// <summary>
        /// 选择输出设备。
        /// </summary>
        /// <param name="output">
        /// 输出设备名称。
        /// </param>
        public void SelectOutput(string output)
        {
            DxModel.SelectOutput(output);
            ScreenDpi = new Rectangle(0, 0,
                DxModel.SelectedOutput.Description.DesktopBounds.Right - DxModel.SelectedOutput.Description.DesktopBounds.Left,
                DxModel.SelectedOutput.Description.DesktopBounds.Bottom - DxModel.SelectedOutput.Description.DesktopBounds.Top);
        }
        /// <summary>
        /// 启动桌面复制。
        /// </summary>
        /// <param name="dpi">
        /// 复制帧数。
        /// </param>
        public void Start(int dpi)
        {
            DxModel.Device = new Device(DxModel.SelectedAdapter);
            var output1 = DxModel.SelectedOutput.QueryInterface<Output1>();
            var textureDesc = new Texture2DDescription
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
            try
            {
                DxModel.DuplicatedOutput = output1.DuplicateOutput(DxModel.Device);
            }
            catch (SharpDXException e)
                when (e.Descriptor == SharpDX.DXGI.ResultCode.NotCurrentlyAvailable)
            {
                throw new Exception("There is already the maximum number of applications using the Desktop Duplication API running, please close one of the applications and try again.", e);
            }
            catch (SharpDXException e)
                when (e.Descriptor == SharpDX.DXGI.ResultCode.Unsupported)
            {
                throw new NotSupportedException("Desktop Duplication is not supported on this system.\nIf you have multiple graphic cards, try running Captura on integrated graphics.", e);
            }

            Duplicating = true;
            DxModel.ScreenTexture = new Texture2D(DxModel.Device, textureDesc);
            Interval = 1000 / dpi;
            StaticData.ThreadMgr.ManageObject["Duplicator"].Start();
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
            var frame = new Bitmap(ScreenDpi.Width, ScreenDpi.Height, PixelFormat.Format32bppRgb);

            // Copy pixels from screen capture Texture to GDI bitmap
            var mapDest = frame.LockBits(new Rectangle(0, 0, ScreenDpi.Width, ScreenDpi.Height), ImageLockMode.WriteOnly, frame.PixelFormat);

            Parallel.For(0, ScreenDpi.Height, y =>
            {
                Utilities.CopyMemory(mapDest.Scan0 + y * mapDest.Stride,
                    sourcePtr + y * sourceRowPitch,
                    ScreenDpi.Width * 4);
            });

            // Release source and dest locks
            frame.UnlockBits(mapDest);

            if (!DxModel.FrameInfo.PointerPosition.Visible) return frame;
            using (var g = Graphics.FromImage(frame))
                MouseCursor.Draw(g, p => new Point(p.X - ScreenDpi.X, p.Y - ScreenDpi.Y));

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
                    throw new Exception("Failed to release frame.", e);
                }
            }
        }

        /// <summary>
        /// 屏幕捕捉核心线程。
        /// </summary>
        protected void Capture()
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
                        var resourceRegion = new ResourceRegion(ScreenDpi.Left, ScreenDpi.Top, 0, ScreenDpi.Right, ScreenDpi.Bottom, 1);

                        DxModel.Device.ImmediateContext.CopySubresourceRegion(tempTexture, 0, resourceRegion, DxModel.TextureDesc, 0);
                    }
                }

                ReleaseFrame();

                var mapSource = DxModel.Device.ImmediateContext.MapSubresource(DxModel.TextureDesc, 0, MapMode.Read, MapFlags.None);

                try
                {
                    DuplBuffer.TryAdd(new BitMapCollection
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

        /// <inheritdoc />
        /// <summary>
        /// 终止桌面复制。
        /// </summary>
        /// <returns>
        /// 异步任务运行状态。
        /// </returns>
        public Task Terminate()
        {
            return Task.Run(() =>
            {
                Duplicating = false;
            });
        }
        /// <summary>
        /// 析构函数。
        /// </summary>
        ~Duplication()
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
            if (disposing)
            {
                DuplBuffer?.Dispose();
            }
        }
        /// <summary>
        /// 释放非托管资源。
        /// </summary>
        protected void ReleaseUnmanagedResources()
        {
            DxModel.DuplicatedOutput?.Dispose();
            DxModel.TextureDesc?.Dispose();
            DxModel.Device?.Dispose();
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
