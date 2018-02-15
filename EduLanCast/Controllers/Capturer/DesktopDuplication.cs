using JeremyAnsel.DirectX.Dxgi;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace EduLanCast.Controllers.Capturer
{
    public class DesktopDuplication
    {
        public int Fps { get; set; }
        public DxgiFactory1 Factory1 { get; }
        public IEnumerable<DxgiAdapter1> Adapters1 { get; }

        public DesktopDuplication()
        {
            Factory1 = DxgiFactory1.Create();
            Adapters1 = Factory1.EnumAdapters();
        }

        public void Test()
        {
            foreach (var adapter1 in Adapters1)
            {
                Console.WriteLine(adapter1);
            }
        }

        public Bitmap Capture()
        {
            var bitmap = new Bitmap(0, 0);
            var factory = DxgiFactory1.Create();
            var adapters = factory.EnumAdapters();
            foreach (var adapter in adapters)
            {
                
            }
            return bitmap;
        }
        /*        private Bitmap _bitmap;

                public Bitmap Capture()
                {
                    // # of graphics card adapter
                    const int numAdapter = 0;

                    // # of output device (i.e. monitor)
                    const int numOutput = 0;

                    // Create DXGI Factory1
                    var factory = new Factory1();
                    var adapter = factory.GetAdapter1(numAdapter);

                    // Create device from Adapter
                    var device = new Device(adapter);

                    // Get DXGI.Output
                    var output = adapter.GetOutput(numOutput);
                    var output1 = output.QueryInterface<Output1>();

                    // Width/Height of desktop to capture
                    var width = ((Rectangle)output.Description.DesktopBounds).Width;
                    var height = ((Rectangle)output.Description.DesktopBounds).Height;

                    // Create Staging texture CPU-accessible
                    var textureDesc = new Texture2DDescription
                    {
                        CpuAccessFlags = CpuAccessFlags.Read,
                        BindFlags = BindFlags.None,
                        Format = Format.B8G8R8A8_UNorm,
                        Width = width,
                        Height = height,
                        OptionFlags = ResourceOptionFlags.None,
                        MipLevels = 1,
                        ArraySize = 1,
                        SampleDescription = { Count = 1, Quality = 0 },
                        Usage = ResourceUsage.Staging
                    };
                    var screenTexture = new Texture2D(device, textureDesc);

                    // Duplicate the output
                    var duplicatedOutput = output1.DuplicateOutput(device);

                    var captureDone = false;
                    for (var i = 0; !captureDone; i++)
                    {
                        try
                        {
                            // Try to get duplicated frame within given time
                            duplicatedOutput.AcquireNextFrame(10000, out _, out var screenResource);

                            if (i > 0)
                            {
                                // copy resource into memory that can be accessed by the CPU
                                using (var screenTexture2D = screenResource.QueryInterface<Texture2D>())
                                    device.ImmediateContext.CopyResource(screenTexture2D, screenTexture);

                                // Get the desktop capture texture
                                var mapSource = device.ImmediateContext.MapSubresource(screenTexture, 0, MapMode.Read, MapFlags.None);

                                // Create Drawing.Bitmap
                                _bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                                var boundsRect = new System.Drawing.Rectangle(0, 0, width, height);

                                // Copy pixels from screen capture Texture to GDI bitmap
                                var mapDest = _bitmap.LockBits(boundsRect, ImageLockMode.WriteOnly, _bitmap.PixelFormat);
                                var sourcePtr = mapSource.DataPointer;
                                var destPtr = mapDest.Scan0;
                                for (var y = 0; y < height; y++)
                                {
                                    // Copy a single line 
                                    Utilities.CopyMemory(destPtr, sourcePtr, width * 4);

                                    // Advance pointers
                                    sourcePtr = IntPtr.Add(sourcePtr, mapSource.RowPitch);
                                    destPtr = IntPtr.Add(destPtr, mapDest.Stride);
                                }

                                // Release source and dest locks
                                _bitmap.UnlockBits(mapDest);
                                device.ImmediateContext.UnmapSubresource(screenTexture, 0);

                                // Capture done
                                captureDone = true;
                            }

                            screenResource.Dispose();
                            duplicatedOutput.ReleaseFrame();

                        }
                        catch (SharpDXException e)
                        {
                            if (e.ResultCode.Code != SharpDX.DXGI.ResultCode.WaitTimeout.Result.Code)
                            {
                                throw;
                            }
                        }
                    }
                    return _bitmap;
                }*/
    }
}
