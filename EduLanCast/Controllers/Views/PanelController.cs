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
    /// 面板控制器。
    /// </summary>
    public class PanelController
    {
        /// <summary>
        /// 桌面复制核心对象。
        /// </summary>
        private DesktopDuplication Duplication { get; }
        /// <summary>
        /// 屏幕刷新率。
        /// </summary>
        public int Fps { get; set; }
        /// <summary>
        /// 适配器列表。
        /// </summary>
        public IList<string> Adapters { get; }
        /// <summary>
        /// 输出列表。
        /// </summary>
        public IList<string> Outputs { get; }
        /// <summary>
        /// 
        /// </summary>
        public Bitmap Screen;
        /// <summary>
        /// 面板控制器构造函数。
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
        /// 刷新适配器列表。
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
        /// 刷新输出列表。
        /// </summary>
        /// <returns>
        /// 异步任务运行状态。
        /// </returns>
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
        /// 初始化屏幕复制。
        /// </summary>
        /// <param name="adapter">
        /// 所选适配器。
        /// </param>
        /// <param name="output">
        /// 所选输出。
        /// </param>
        public void InitDuplicate(string adapter, string output)
        {
            var ad = Duplication.Adapters1.First(tmp => tmp.Description1.Description == adapter);
            var ou = Duplication.Outputs.First(tmp => tmp.Description.DeviceName == output);
            Duplication.InitDuplication(ad, ou, ref Screen, 1000 / Fps);
        }
        /// <summary>
        /// 异步获取输出列表。
        /// </summary>
        /// <param name="adapter">
        /// 选择的适配器。
        /// </param>
        /// <returns>
        /// 异步任务运行状态。
        /// </returns>
        public async Task QueryOutputsAsync(string adapter)
        {
            var q = Duplication.Adapters1.First(tmp => tmp.Description1.Description == adapter);
            Duplication.QueryOutputs(q);
            await RefrushOutputs();
        }
        /// <summary>
        /// 选择输出并获取该输出分辨率。
        /// </summary>
        /// <param name="output">
        /// 所选输出字符串。
        /// </param>
        /// <returns>
        /// 该输出分辨率。
        /// </returns>
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
