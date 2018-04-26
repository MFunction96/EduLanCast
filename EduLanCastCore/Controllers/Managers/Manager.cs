using EduLanCastCore.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduLanCastCore.Controllers.Managers
{
    /// <inheritdoc />
    /// <summary>
    /// 管理器抽象类。
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public abstract class Manager<TType> : ITerminate
    {
        /// <summary>
        /// 受管理对象字典。
        /// </summary>
        public Dictionary<string, TType> ManageObject { get; }
        /// <summary>
        /// 管理器构造函数。
        /// </summary>
        protected Manager()
        {
            ManageObject = new Dictionary<string, TType>();
        }
        /// <summary>
        /// 启动所有受管理对象。
        /// </summary>
        /// <returns>
        /// 异步任务运行状态。
        /// </returns>
        public async Task Start()
        {
            foreach (var obj in ManageObject)
            {
                await TerminateCore(obj.Value);
            }
        }
        /// <inheritdoc />
        /// <summary>
        /// 停止所有受管理对象。
        /// </summary>
        /// <returns>
        /// 异步任务运行状态。
        /// </returns>
        public async Task Terminate()
        {
            foreach (var obj in ManageObject)
            {
                await TerminateCore(obj.Value);
            }
        }
        /// <summary>
        /// 解释如何启动对象。
        /// </summary>
        /// <param name="obj">
        /// 将启动的对象。
        /// </param>
        /// <returns>
        /// 异步任务运行状态。
        /// </returns>
        protected abstract Task StartCore(TType obj);
        /// <summary>
        /// 解释如何停止对象。
        /// </summary>
        /// <param name="obj">
        /// 将停止的对象。
        /// </param>
        /// <returns>
        /// 异步任务运行状态。
        /// </returns>
        protected abstract Task TerminateCore(TType obj);
    }
}
