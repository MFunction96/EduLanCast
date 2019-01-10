using EduLanCast.Controllers.Network;
using EduLanCastCore.Models.Sockets.NetworkEventArgs;
using System.Net.Sockets;

namespace EduLanCast.Models.Network
{
    /// <summary>
    /// 
    /// </summary>
    internal class AcceptCallbackEventArgs : BaseAcceptCallbackEventArgs
    {
        protected SocketManager Manager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="socket"></param>
        internal AcceptCallbackEventArgs(ref SocketManager manager,  Socket socket) : base(socket)
        {
            Manager = manager;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            base.Dispose(true);
        }
        /// <summary>
        /// 
        /// </summary>
        ~AcceptCallbackEventArgs()
        {
            Dispose(false);
        }
    }
}
