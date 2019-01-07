using System;
using System.Net.Sockets;

namespace EduLanCastCore.Models.Sockets
{
    public class BaseNetworkEventArgs : EventArgs, IDisposable
    {
        protected Socket Handler { get; }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="handler"></param>
        protected BaseNetworkEventArgs(Socket handler)
        {
            Handler = handler;
        }
        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
        }
        /// <summary>
        /// 
        /// </summary>
        ~BaseNetworkEventArgs()
        {
            Dispose(false);
        }
    }
}
