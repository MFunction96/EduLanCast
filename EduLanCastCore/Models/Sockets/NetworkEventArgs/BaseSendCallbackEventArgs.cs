using System.Net.Sockets;

namespace EduLanCastCore.Models.Sockets.NetworkEventArgs
{
    public abstract class BaseSendCallbackEventArgs : BaseNetworkEventArgs
    {
        protected BaseSendCallbackEventArgs(Socket handler) : base(handler)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            base.Dispose(true);
        }

        ~BaseSendCallbackEventArgs()
        {
            Dispose(false);
        }
    }
}
