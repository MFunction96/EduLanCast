using EduLanCast.Controllers.Capturer;
using EduLanCast.Data;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EduLanCast.Controllers.Views
{
    /// <summary>
    /// 
    /// </summary>
    public class PanelController
    {
        private DesktopDuplication Duplication { get; }
        /// <summary>
        /// 
        /// </summary>
        public int Fps { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IList<string> Adapters { get; }
        /// <summary>
        /// 
        /// </summary>
        public IList<string> Outputs { get; }
        /// <summary>
        /// 
        /// </summary>
        public Bitmap Screen;
        /// <summary>
        /// 
        /// </summary>
        public PanelController()
        {
            Duplication = new DesktopDuplication();
            Adapters = new List<string>();
            Outputs = new List<string>();
            RefrushAdapters();
            RefrushOutputs();
            StaticData.ThreadMgr.ManageObject["Duplication"] = new Thread(Duplication.Duplicate);
        }
        /// <summary>
        /// 
        /// </summary>
        private void RefrushAdapters()
        {
            Adapters.Clear();
            foreach (var adapter1 in Duplication.Adapters1)
            {
                Adapters.Add(adapter1.Description1.Description);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Task RefrushOutputs()
        {
            return Task.Run(() =>
            {
                Outputs.Clear();
                if (Duplication.Outputs is null) return;
                foreach (var output1 in Duplication.Outputs)
                {
                    Outputs.Add(output1.Description.DeviceName);
                }
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="output"></param>
        public void InitDuplicate(string adapter, string output)
        {
            var ad = Duplication.Adapters1.First(tmp => tmp.Description1.Description == adapter);
            var ou = Duplication.Outputs.First(tmp => tmp.Description.DeviceName == output);
            Duplication.InitDuplication(ad, ou, ref Screen, 1000 / Fps);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <returns></returns>
        public async Task QueryOutputsAsync(string adapter)
        {
            var q = Duplication.Adapters1.First(tmp => tmp.Description1.Description == adapter);
            Duplication.QueryOutputs(q);
            await RefrushOutputs();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public Task<string> SelectOutput(string output)
        {
            return Task.Run(() =>
            {
                if (Duplication.Outputs is null)
                {
                    return "null";
                }
                var output1 = Duplication.Outputs.First(tmp => tmp.Description.DeviceName == output);
                var screen = Duplication.SelectOutput(output1);
                return $"{screen.Width}x{screen.Height}";
            });
        }
    }
}
