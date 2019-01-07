using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace EduLanCast.Controllers.Network
{
    internal class SocketManager : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<string, Socket> SocketDictionary { get; }
        /// <summary>
        /// 
        /// </summary>
        public SocketManager()
        {
            SocketDictionary = new Dictionary<string, Socket>();
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
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
            foreach (var socket in SocketDictionary)
            {
                socket.Value.Close();
            }
            SocketDictionary.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        ~SocketManager()
        {
            Dispose(false);
        }
    }
}
