using System;
using System.Net.Sockets;
using EduLanCastCore.Controllers.Threads;
using EduLanCastCore.Interfaces.NetworkEventArgs;
using EduLanCastCore.Models.Sockets;

namespace EduLanCastClient.Controllers.Network
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class ClientSocket : SocketThread
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
        public override event EventHandler<IConnectCallbackEventArgs> ConnectCallbackHandler;
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override event EventHandler<IAcceptCallbackEventArgs> AcceptCallbackHandler;
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override event EventHandler<IReceiveEventArgs> ReceiveHandler;
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override event EventHandler<IReceiveCallbackEventArgs> ReceiveCallbackHandler;
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override event EventHandler<ISendEventArgs> SendHandler;
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override event EventHandler<ISendCallbackEventArgs> SendCallbackHandler;
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
