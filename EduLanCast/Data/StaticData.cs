using EduLanCast.Controllers.Managers;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace EduLanCast.Data
{
    /// <summary>
    /// 全局数据类。
    /// </summary>
    public static class StaticData
    {
        /// <summary>
        /// 线程管理器。
        /// </summary>
        public static ThreadManager ThreadMgr { get; }
        /// <summary>
        /// 窗体管理器
        /// </summary>
        public static FormManager FormMgr { get; }
        /// <summary>
        /// 缓冲队列。
        /// </summary>
        public static Queue<Bitmap> Buffer { get; }
        /// <summary>
        /// 全局数据构造函数。
        /// </summary>
        static StaticData()
        {
            ThreadMgr = new ThreadManager();
            FormMgr = new FormManager();
            Buffer = new Queue<Bitmap>();
        }
        /// <summary>
        /// 全局数据终止任务。
        /// </summary>
        /// <returns>
        /// 异步任务运行状态。
        /// </returns>
        public static async Task Terminate()
        {
            await ThreadMgr.Terminate();
            await FormMgr.Terminate();
            Buffer.Clear();
        }
    }
}
