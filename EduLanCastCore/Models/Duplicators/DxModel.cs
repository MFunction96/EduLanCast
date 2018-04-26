using System;
using SharpDX.DXGI;
using System.Collections.Generic;
using System.Linq;
using SharpDX.Direct3D11;
using Device = SharpDX.Direct3D11.Device;

namespace EduLanCastCore.Models.Duplicators
{
    public class DxModel
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
        public Adapter1 SelectedAdapter { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Output SelectedOutput { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Device Device { get; set; }

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
            SelectedAdapter = Adapters1.First(tmp => tmp.Description.Description == adapter);
            if (SelectedAdapter is null) throw new NullReferenceException();
            Outputs = SelectedAdapter.Outputs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        public void SelectOutput(string output)
        {
            SelectedOutput = Outputs.First(tmp => tmp.Description.DeviceName == output);
            if (SelectedAdapter is null) throw new NullReferenceException();
        }
    }
}
