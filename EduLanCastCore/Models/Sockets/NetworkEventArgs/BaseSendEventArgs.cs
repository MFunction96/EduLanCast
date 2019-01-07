using System.Net.Sockets;

namespace EduLanCastCore.Models.Sockets.NetworkEventArgs
{
    public abstract class BaseSendEventArgs : BaseNetworkEventArgs
    {
        protected BaseSendEventArgs(Socket handler) : base(handler)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            base.Dispose(true);
        }

        ~BaseSendEventArgs()
        {
            Dispose(false);
        }
    }
}
