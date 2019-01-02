using EduLanCastCore.Interfaces;
using System;
using System.Threading.Tasks;

namespace EduLanCastCore.Controllers.Duplicators
{
    /// <inheritdoc cref="ITerminate"/>
    /// <summary>
    /// 桌面展现类。
    /// </summary>
    public class Presentation : ITerminate
    {
        /// <summary>
        /// 桌面展现类构造函数。
        /// </summary>
        public Presentation()
        {

        }
        /// <inheritdoc />
        /// <summary>
        /// 终止桌面展示。
        /// </summary>
        /// <returns>
        /// 异步任务运行状态。
        /// </returns>
        public Task Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
