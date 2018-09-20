using EduLanCastCore.Controllers.Threads;
using EduLanCastCore.Data;
using EduLanCastCore.Interfaces;
using EduLanCastCore.Models.Configs;
using EduLanCastCore.Models.Duplicators;
using System;
using System.Drawing;
using System.Threading;

namespace EduLanCastCore.Controllers.Duplicators
{
    /// <inheritdoc cref="IDisposable" />
    /// <summary>
    /// 桌面复制类。
    /// </summary>
    public class Duplication : IDisposable
    {
        /// <summary>
        /// DirectX模块。
        /// </summary>
        public DxModel DxModel;
        /// <summary>
        /// 桌面分辨率。
        /// </summary>
        public Rectangle ScreenDpi { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        protected IServiceThread Duplicate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected AppConfig Config;
        /// <summary>
        /// 复制线程运行状态。
        /// </summary>
        protected bool Duplicating;
        /// <summary>
        /// 桌面复制类构造函数。
        /// </summary>
        public Duplication(ref AppConfig config)
        {
            
            DxModel = new DxModel();
            Duplicate = new DuplicateThread(ref config, ref DxModel);
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
            
        }
        /// <summary>
        /// 启动桌面复制。
        /// </summary>
        /// <param name="dpi">
        /// 复制帧数。
        /// </param>
        public void Start(int dpi)
        {
            
        }


        /// <summary>
        /// 屏幕捕捉核心线程。
        /// </summary>
        protected void Capture()
        {
            

        }

        /// <inheritdoc />
        /// <summary>
        /// 析构对象。
        /// </summary>
        public void Dispose()
        {
            Duplicate?.Dispose();
            DxModel?.Dispose();
        }
    }
}
