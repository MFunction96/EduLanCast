using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduLanCast.Controllers.Managers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public abstract class Manager<TType>
    {
        public Dictionary<string, TType> ManageObject { get; }

        protected Manager()
        {
            ManageObject = new Dictionary<string, TType>();
        }

        public async Task Start()
        {
            foreach (var obj in ManageObject)
            {
                await TerminateCore(obj.Value);
            }
        }

        public async Task Terminate()
        {
            foreach (var obj in ManageObject)
            {
                await TerminateCore(obj.Value);
            }
        }
        protected abstract Task StartCore(TType obj);
        protected abstract Task TerminateCore(TType obj);
    }
}
