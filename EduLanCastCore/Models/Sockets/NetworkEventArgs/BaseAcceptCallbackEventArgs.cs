using System.Net.Sockets;

namespace EduLanCastCore.Models.Sockets.NetworkEventArgs
{
    public abstract class BaseAcceptCallbackEventArgs : BaseNetworkEventArgs
    {
        protected BaseAcceptCallbackEventArgs(Socket handler) : base(handler)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            base.Dispose(true);
        }

        ~BaseAcceptCallbackEventArgs()
        {
            Dispose(false);
        }
    }
}
