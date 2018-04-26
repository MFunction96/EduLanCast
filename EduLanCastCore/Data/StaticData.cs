using EduLanCastCore.Controllers.Managers;
using System.Threading.Tasks;

namespace EduLanCastCore.Data
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
        /// 全局数据构造函数。
        /// </summary>
        static StaticData()
        {
            ThreadMgr = new ThreadManager();
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
        }
    }
}
