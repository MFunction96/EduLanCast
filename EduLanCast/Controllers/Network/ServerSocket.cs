using EduLanCastCore.Controllers.Threads;
using EduLanCastCore.Controllers.Utils;
using EduLanCastCore.Interfaces.NetworkEventArgs;
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
    public class ServerSocket : SocketThread
    {
        #region Properties

        protected AppConfig Config;
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
        #endregion

        #region Construction

        public ServerSocket(ref AppConfig config)
        {
            Config = config;
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
            var state = ar.AsyncState as StateObject;

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
    }

    #endregion

    #endregion

}
