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
    /// </summary>
    public class Duplication : IDisposable, ITerminate
    {
        /// <summary>
        /// 
        /// </summary>
        public Action<BlockingCollection<BitMapCollection>> DuplAction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private BlockingCollection<BitMapCollection> DuplBuffer { get; }
        /// <summary>
        /// 
        /// </summary>
        public DxModel DxModel { get; }
        /// <summary>
        /// 
        /// </summary>
        public Rectangle ScreenDpi { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected bool Duplicating;
        /// <summary>
        /// 
        /// </summary>
        public Duplication()
        {
            DuplBuffer = new BlockingCollection<BitMapCollection>();
            DxModel = new DxModel();
            StaticData.ThreadMgr.ManageObject["Duplicator"] = new Thread(Capture);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        public void SelectAdapter(string adapter)
        {
            DxModel.SelectAdapter(adapter);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        public void SelectOutput(string output)
        {
            DxModel.SelectOutput(output);
            ScreenDpi = new Rectangle(0, 0,
                DxModel.SelectedOutput.Description.DesktopBounds.Right - DxModel.SelectedOutput.Description.DesktopBounds.Left,
                DxModel.SelectedOutput.Description.DesktopBounds.Bottom - DxModel.SelectedOutput.Description.DesktopBounds.Top);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="output"></param>
        public void Start(string adapter, string output)
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
            StaticData.ThreadMgr.ManageObject["Duplicator"].Start();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePtr"></param>
        /// <param name="sourceRowPitch"></param>
        /// <returns></returns>
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
        /// 
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
        /// 
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
        /// </summary>
        /// <returns></returns>
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
