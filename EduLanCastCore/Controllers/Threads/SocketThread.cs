using EduLanCastCore.Interfaces.NetworkEventArgs;
using EduLanCastCore.Models.Sockets;
using System;
using System.Net.Sockets;

namespace EduLanCastCore.Controllers.Threads
{
    /// <inheritdoc />
    /// <summary>
    /// Socket线程。用于Socket相关任务线程，
    /// </summary>
    public abstract class SocketThread : ServiceThread
    {
        #region Properties
        /// <summary>
        /// Ipv4协议Socket。
        /// </summary>
        protected Socket Socketv4 { get; set; }
        /// <summary>
        /// Ipv6协议Socket。
        /// </summary>
        protected Socket Socketv6 { get; set; }
        /// <summary>
        /// 请求或反馈信息。
        /// </summary>
        public SocketMessage Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public abstract event EventHandler<IConnectCallbackEventArgs> ConnectCallbackHandler;
        /// <summary>
        /// 
        /// </summary>
        public abstract event EventHandler<IAcceptCallbackEventArgs> AcceptCallbackHandler;
        /// <summary>
        /// 
        /// </summary>
        public abstract event EventHandler<IReceiveEventArgs> ReceiveHandler;
        /// <summary>
        /// 
        /// </summary>
        public abstract event EventHandler<IReceiveCallbackEventArgs> ReceiveCallbackHandler;
        /// <summary>
        /// 
        /// </summary>
        public abstract event EventHandler<ISendEventArgs> SendHandler;
        /// <summary>
        /// 
        /// </summary>
        public abstract event EventHandler<ISendCallbackEventArgs> SendCallbackHandler;

        #endregion

        #region Construction

        /// <inheritdoc />
        /// <summary>
        /// Socket监听器默认构造函数。
        /// </summary>
        protected SocketThread()
        {
            Socketv4 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socketv6 = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            Message = new SocketMessage();
            MainThread.IsBackground = true;
        }

        #endregion

        #region Methods

        #region Implement



        #endregion

        #region Public



        #endregion

        #region Protected

        /// <summary>
        /// 初始化Socket监听器。
        /// 请在此方法内初始化Socket。
        /// </summary>
        protected abstract void Initialization();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        protected abstract void ConnectCallback(IAsyncResult ar);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        protected abstract void AcceptCallback(IAsyncResult ar);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        protected abstract void Receive(Socket client);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        protected abstract void ReceiveCallback(IAsyncResult ar);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="message"></param>
        protected abstract void Send(Socket handler, SocketMessage message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        protected abstract void SendCallback(IAsyncResult ar);
        
        #endregion

        #region Private



        #endregion

        #endregion
    }
}
