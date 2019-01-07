using System.Net.Sockets;

namespace EduLanCastCore.Models.Sockets.NetworkEventArgs
{
    public abstract class BaseReceiveEventArgs : BaseNetworkEventArgs
    {
        protected BaseReceiveEventArgs(Socket handler) : base(handler)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            base.Dispose(true);
        }

        ~BaseReceiveEventArgs()
        {
            Dispose(false);
        }
    }
}
