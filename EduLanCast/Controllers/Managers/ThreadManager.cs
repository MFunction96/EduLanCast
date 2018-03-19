using System.Threading;
using System.Threading.Tasks;

namespace EduLanCast.Controllers.Managers
{
    public class ThreadManager : Manager<Thread>
    {
        protected override Task StartCore(Thread obj)
        {
            return Task.Run(() => { obj.Start(); });
        }

        protected override Task TerminateCore(Thread obj)
        {
            return Task.Run(() => { obj.Interrupt(); });
        }
    }
}
