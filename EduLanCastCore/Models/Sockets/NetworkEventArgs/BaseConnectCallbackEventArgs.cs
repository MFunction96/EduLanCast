using System.Net.Sockets;

namespace EduLanCastCore.Models.Sockets.NetworkEventArgs
{
    public abstract class BaseConnectCallbackEventArgs : BaseNetworkEventArgs
    {
        protected BaseConnectCallbackEventArgs(Socket handler) : base(handler)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            base.Dispose(true);
        }

        ~BaseConnectCallbackEventArgs()
        {
            Dispose(false);
        }
    }
}
