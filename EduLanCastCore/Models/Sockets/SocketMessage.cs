using EduLanCastCore.Models.Configs;
using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace EduLanCastCore.Models.Sockets
{
    // State object for reading client data asynchronously  
    /// <summary>
    /// 
    /// </summary>
    public class StateObject
    {
        // Client  socket.  
        /// <summary>
        /// 
        /// </summary>
        public Socket WorkSocket;
        // Size of receive buffer.  
        /// <summary>
        /// 
        /// </summary>
        public int BufferSize;
        // Receive buffer.  
        /// <summary>
        /// 
        /// </summary>
        public byte[] Buffer;
        /// <summary>
        /// 
        /// </summary>
        public int ReceiveSize;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="config"></param>
        public StateObject(Socket socket, AppConfig config)
        {
            WorkSocket = socket;
            BufferSize = config.BufferSize;
            Buffer = new byte[BufferSize];
            ReceiveSize = 0;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum MessageType : byte
    {
        /// <summary>
        /// 
        /// </summary>
        ConfigMsg = 0,
        /// <summary>
        /// 
        /// </summary>
        CommandMsg = 1,
        /// <summary>
        /// 
        /// </summary>
        FileMsg = 2
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class SocketHeader : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageType MessageType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Type AdditionHeaderType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object AdditionHeader { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Type BodyType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class SocketMessage : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public SocketHeader Headers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object Body { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SocketMessage()
        {
            Headers = new SocketHeader();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        }
    }
}
