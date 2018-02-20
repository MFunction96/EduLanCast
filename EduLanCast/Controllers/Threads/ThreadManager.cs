using System.Collections.Generic;
using System.Threading;

namespace EduLanCast.Controllers.Threads
{
    public static class ThreadManager
    {
        public static Dictionary<string, Thread> Threads { get; }

        static ThreadManager()
        {
            Threads = new Dictionary<string, Thread>();
        }

        public static void Terminate()
        {
            foreach (var thread in Threads)
            {
                thread.Value.Interrupt();
            }
        }
    }
}
