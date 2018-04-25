using System.Threading.Tasks;
using System.Windows.Forms;

namespace EduLanCast.Controllers.Managers
{
    /// <inheritdoc />
    /// <summary>
    /// 窗体管理器。
    /// </summary>
    public class FormManager : Manager<Form>
    {
        /// <inheritdoc />
        /// <summary>
        /// 窗体管理器启动核心逻辑。
        /// </summary>
        /// <param name="obj">
        /// 操作的单一对象。
        /// </param>
        /// <returns>
        /// 异步任务运行状态。
        /// </returns>
        protected override Task StartCore(Form obj)
        {
            return Task.Run(() => { obj.Show(); });
        }
        /// <inheritdoc />
        /// <summary>
        /// 窗体管理器终止核心逻辑。
        /// </summary>
        /// <param name="obj">
        /// 操作的单一对象。
        /// </param>
        /// <returns>
        /// 异步任务运行状态。
        /// </returns>
        protected override Task TerminateCore(Form obj)
        {
            return Task.Run(() => { obj.Close(); });
        }
    }
}
