using EduLanCastCore.Models.Configs;
using System.Net.Sockets;

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
}
