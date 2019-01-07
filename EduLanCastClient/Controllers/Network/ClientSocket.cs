using EduLanCastCore.Controllers.Threads;
using EduLanCastCore.Models.Sockets;
using System;
using System.Net.Sockets;

namespace EduLanCastClient.Controllers.Network
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    internal class ClientSocket : SocketThread
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override void Operation()
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        protected override void Initialization()
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        protected override void ConnectCallback(IAsyncResult ar)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        protected override void AcceptCallback(IAsyncResult ar)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        protected override void Receive(Socket client)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        protected override void ReceiveCallback(IAsyncResult ar)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        protected override void Send(Socket handler, SocketMessage message)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        protected override void SendCallback(IAsyncResult ar)
        {
            throw new NotImplementedException();
        }
    }
}
