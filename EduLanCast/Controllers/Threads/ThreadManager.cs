using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EduLanCast.Controllers.Threads
{
    public static class ThreadManager
    {
        public static Dictionary<string, Thread> Threads { get; }

        static ThreadManager()
        {
            Threads = new Dictionary<string, Thread>();
        }

        public static Task Terminate()
        {
            return Task.Run(() =>
            {
                foreach (var thread in Threads)
                {
                    thread.Value.Interrupt();
                }
            });
        }

        public static Task Start()
        {
            return Task.Run(() =>
            {
                foreach (var thread in Threads)
                {
                    thread.Value.Start();
                }
            });
        }
    }
}
