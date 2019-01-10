using System.Net.Sockets;

namespace EduLanCastCore.Models.Sockets.NetworkEventArgs
{
    public abstract class BaseCloseEventArgs : BaseNetworkEventArgs
    {
        protected BaseCloseEventArgs(Socket handler) : base(handler)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            base.Dispose(true);
        }

        ~BaseCloseEventArgs()
        {
            Dispose(false);
        }
    }
}
