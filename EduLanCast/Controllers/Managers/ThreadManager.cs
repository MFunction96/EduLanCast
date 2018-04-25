using System.Threading;
using System.Threading.Tasks;

namespace EduLanCast.Controllers.Managers
{
    /// <inheritdoc />
    /// <summary>
    /// 线程管理模块。
    /// </summary>
    public class ThreadManager : Manager<Thread>
    {
        /// <inheritdoc />
        /// <summary>
        /// 线程管理器启动核心逻辑。
        /// </summary>
        /// <param name="obj">
        /// 操作的单一对象。
        /// </param>
        /// <returns>
        /// 异步任务运行状态。
        /// </returns>
        protected override Task StartCore(Thread obj)
        {
            return Task.Run(() => { obj.Start(); });
        }
        /// <inheritdoc />
        /// <summary>
        /// 线程管理器终止核心逻辑。
        /// </summary>
        /// <param name="obj">
        /// 操作的单一对象。
        /// </param>
        /// <returns>
        /// 异步任务运行状态。
        /// </returns>
        protected override Task TerminateCore(Thread obj)
        {
            return Task.Run(() => { obj.Interrupt(); });
        }
    }
}
