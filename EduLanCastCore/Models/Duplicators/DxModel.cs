using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Device = SharpDX.Direct3D11.Device;

namespace EduLanCastCore.Models.Duplicators
{
    public class DxModel : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Adapter1> Adapters1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Output> Outputs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Adapter1 SelectedAdapter { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public Output SelectedOutput { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public Device Device { get; set; }

        public Rectangle ScreenDpi => SelectedOutput == null
            ? new Rectangle(0, 0, 0, 0)
            : new Rectangle(0, 0,
                SelectedOutput.Description.DesktopBounds.Right - SelectedOutput.Description.DesktopBounds.Left,
                SelectedOutput.Description.DesktopBounds.Bottom - SelectedOutput.Description.DesktopBounds.Top);

        /// <summary>
        /// 
        /// </summary>
        public OutputDuplicateFrameInformation FrameInfo;
        /// <summary>
        /// 
        /// </summary>
        public Texture2D TextureDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OutputDuplication DuplicatedOutput { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Texture2D ScreenTexture { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        public void SelectAdapter(string adapter)
        {
            SelectedAdapter = Adapters1.FirstOrDefault(tmp => tmp.Description.Description == adapter);
            if (SelectedAdapter is null) throw new NullReferenceException();
            Outputs = SelectedAdapter.Outputs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        public void SelectOutput(string output)
        {
            SelectedOutput = Outputs.FirstOrDefault(tmp => tmp.Description.DeviceName == output);
            if (SelectedAdapter is null) throw new NullReferenceException();
        }


        public void Dispose()
        {
            SelectedAdapter?.Dispose();
            SelectedOutput?.Dispose();
            Device?.Dispose();
            TextureDesc?.Dispose();
            DuplicatedOutput?.Dispose();
            ScreenTexture?.Dispose();
        }
    }
}
