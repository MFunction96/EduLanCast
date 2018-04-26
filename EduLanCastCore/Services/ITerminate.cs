using System.Threading.Tasks;

namespace EduLanCastCore.Services
{
    /// <summary>
    /// 任务结束接口。
    /// </summary>
    internal interface ITerminate
    {
        /// <summary>
        /// 任务结束。
        /// </summary>
        /// <returns>
        /// 任务完成状态。
        /// </returns>
        Task Terminate();
    }
}
