using System.Net.Sockets;

namespace EduLanCastCore.Models.Sockets.NetworkEventArgs
{
    public abstract class BaseReceiveCallbackEventArgs : BaseNetworkEventArgs
    {
        protected BaseReceiveCallbackEventArgs(Socket handler) : base(handler)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            base.Dispose(true);
        }

        ~BaseReceiveCallbackEventArgs()
        {
            Dispose(false);
        }
    }
}
