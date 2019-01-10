using EduLanCast.Models.Network;
using EduLanCastCore.Controllers.Threads;
using EduLanCastCore.Controllers.Utils;
using EduLanCastCore.Models.Configs;
using EduLanCastCore.Models.Sockets;
using System;
using System.Net;
using System.Net.Sockets;

namespace EduLanCast.Controllers.Network
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    internal class ServerSocket : SocketThread
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        protected AppConfig Config;
        /// <summary>
        /// 
        /// </summary>
        protected SocketManager Manager;
        /// <summary>
        /// 
        /// </summary>
        public string ReceiveFilePath { get; set; }
        #endregion

        #region Construction
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="config"></param>
        public ServerSocket(ref AppConfig config)
        {
            Config = config;
            ReceiveFilePath = string.Empty;
            Manager = new SocketManager();
        }

        #endregion

        #region Methods

        #region Implement

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override void Operation()
        {
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            //var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //var ipAddress = ipHostInfo.AddressList[0];
            var localEndPoint = new IPEndPoint(IPAddress.Any, Config.Port);
            
            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                Socketv4.Bind(localEndPoint);
                Socketv4.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    AcceptDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    //Console.WriteLine("Waiting for a connection...");
                    Socketv4.BeginAccept(AcceptCallback, Socketv4);

                    // Wait until a connection is made before continuing.  
                    AcceptDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                ErrorUtil.WriteError(e);
            }

            //Console.WriteLine("\nPress ENTER to continue...");
            //Console.Read();
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
            // Signal the main thread to continue.  
            AcceptDone.Set();

            try
            {
                // Get the socket that handles the client request.  
                if (!(ar.AsyncState is Socket listener)) return;
                var handler = listener.EndAccept(ar);

                OnRaiseAcceptCallbackEvent(new AcceptCallbackEventArgs(ref Manager, handler));

                // Create the state object.  
                var state = new StateObject(handler, Config);
                handler.BeginReceive(state.Buffer, 0, state.BufferSize, 0,
                    ReceiveCallback, state);
            }
            catch (Exception e)
            {
                ErrorUtil.WriteError(e);
            }
            
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
            if (!(ar.AsyncState is StateObject state)) return;
            var handler = state.WorkSocket;
            var bytesRead = handler.EndReceive(ar);

            if (string.IsNullOrEmpty(ReceiveFilePath))
            {

            }
            else
            {
                if (bytesRead > 0)
                {

                }
                else
                {
                    handler.BeginReceive(state.Buffer, 0, state.BufferSize, 0, ReceiveCallback, state);
                }
            }
            
/*            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.   
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read   
                // more data.  
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the   
                    // client. Display it on the console.  
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);
                    // Echo the data back to the client.  
                    Send(handler, content);
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.Buffer, 0, state.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
            }*/
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
        /// <summary>
        /// 
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            Manager.Dispose();
            base.Dispose(true);
        }
        /// <summary>
        /// 
        /// </summary>
        ~ServerSocket()
        {
            Dispose(false);
        }
    }

    #endregion

    #endregion

}
