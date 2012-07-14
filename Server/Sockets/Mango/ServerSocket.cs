using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

using Mango.Communication.Sessions;
using log4net;

namespace Mango.Communication
{
    sealed class ServerSocket
    {
        private static readonly ILog log = LogManager.GetLogger("Mango.Communication.ServerSocket");

        /// <summary>
        /// The settings to use with this ServerSocket.
        /// </summary>
        ServerSocketSettings Settings;

        /// <summary>
        /// The buffer manager for allocation a buffer block to a SocketAsyncEventArgs.
        /// </summary>
        BufferManager BufferManager;

        /// <summary>
        /// The semaphore used for controlling the max connections to the server.
        /// </summary>
        SemaphoreSlim MaxConnectionsEnforcer;

        /// <summary>
        /// The semaphore used for controlling the max SocketAsyncEventArgs to be used for send operations at a time.
        /// </summary>
        SemaphoreSlim MaxSaeaSendEnforcer;

        /// <summary>
        /// The semaphore used for controlling the max socket accept operations at a time.
        /// </summary>
        SemaphoreSlim MaxAcceptOpsEnforcer;

        /// <summary>
        /// The socket used for listening for incoming connections.
        /// </summary>
        Socket ListenSocket;

        /// <summary>
        /// The pool of re-usable SocketAsyncEventArgs for accept operations.
        /// </summary>
        SocketAsyncEventArgsPool PoolOfAcceptEventArgs;

        /// <summary>
        /// The pool of re-usable SocketAsyncEventArgs for receiving data.
        /// </summary>
        SocketAsyncEventArgsPool PoolOfRecEventArgs;

        /// <summary>
        /// The pool of re-usable SocketAsyncEventArgs for sending data.
        /// </summary>
        SocketAsyncEventArgsPool PoolOfSendEventArgs;

        /// <summary>
        /// Initializes a new instance of the ServerSocket.
        /// </summary>
        /// <param name="settings">The settings to use with this ServerSocket.</param>
        public ServerSocket(ServerSocketSettings settings)
        {
            this.Settings = settings;

            this.BufferManager = new BufferManager((this.Settings.BufferSize * this.Settings.NumOfSaeaForRec) +  (this.Settings.BufferSize * this.Settings.NumOfSaeaForSend),
                this.Settings.BufferSize);
            this.PoolOfAcceptEventArgs = new SocketAsyncEventArgsPool(this.Settings.MaxSimultaneousAcceptOps);
            this.PoolOfRecEventArgs = new SocketAsyncEventArgsPool(this.Settings.NumOfSaeaForRec);
            this.PoolOfSendEventArgs = new SocketAsyncEventArgsPool(this.Settings.NumOfSaeaForSend);

            this.MaxConnectionsEnforcer = new SemaphoreSlim(this.Settings.MaxConnections, this.Settings.MaxConnections);
            this.MaxSaeaSendEnforcer = new SemaphoreSlim(this.Settings.NumOfSaeaForSend, this.Settings.NumOfSaeaForSend);
            this.MaxAcceptOpsEnforcer = new SemaphoreSlim(this.Settings.MaxSimultaneousAcceptOps, this.Settings.MaxSimultaneousAcceptOps);
        }

        public void Init()
        {
            this.BufferManager.InitBuffer();

            for (int i = 0; i < this.Settings.MaxSimultaneousAcceptOps; i++)
            {
                SocketAsyncEventArgs acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed +=
                    new EventHandler<SocketAsyncEventArgs>(AcceptEventArg_Completed);

                this.PoolOfAcceptEventArgs.Push(acceptEventArg);
            }

            // receive objects
            for (int i = 0; i < this.Settings.NumOfSaeaForRec; i++)
            {
                SocketAsyncEventArgs eventArgObjectForPool = new SocketAsyncEventArgs();
                this.BufferManager.SetBuffer(eventArgObjectForPool);

                eventArgObjectForPool.Completed +=
                    new EventHandler<SocketAsyncEventArgs>(IO_ReceiveCompleted);
                eventArgObjectForPool.UserToken = new Session(i, this);
                this.PoolOfRecEventArgs.Push(eventArgObjectForPool);
            }

            // send objects
            for (int i = 0; i < this.Settings.NumOfSaeaForSend; i++)
            {
                SocketAsyncEventArgs eventArgObjectForPool = new SocketAsyncEventArgs();
                this.BufferManager.SetBuffer(eventArgObjectForPool);

                eventArgObjectForPool.Completed +=
                    new EventHandler<SocketAsyncEventArgs>(IO_SendCompleted);
                eventArgObjectForPool.UserToken = new SendDataToken();
                this.PoolOfSendEventArgs.Push(eventArgObjectForPool);
            }
        }

        public void StartListen()
        {
            this.ListenSocket = new Socket(this.Settings.Endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.ListenSocket.Bind(this.Settings.Endpoint);
            this.ListenSocket.Listen(this.Settings.Backlog);

            StartAccept();
        }

        private void StartAccept()
        {
            SocketAsyncEventArgs acceptEventArgs;

            this.MaxAcceptOpsEnforcer.Wait();

            if (this.PoolOfAcceptEventArgs.TryPop(out acceptEventArgs))
            {
                this.MaxConnectionsEnforcer.Wait();
                bool willRaiseEvent = this.ListenSocket.AcceptAsync(acceptEventArgs);

                if (!willRaiseEvent)
                {
                    ProcessAccept(acceptEventArgs);
                }
            }
        }

        private void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        private void ProcessAccept(SocketAsyncEventArgs acceptEventArgs)
        {
            StartAccept();

            if (acceptEventArgs.SocketError != SocketError.Success)
            {
                HandleBadAccept(acceptEventArgs);
                this.MaxAcceptOpsEnforcer.Release();
                return;
            }

            SocketAsyncEventArgs recEventArgs;

            if (this.PoolOfRecEventArgs.TryPop(out recEventArgs))
            {
                ((Session)recEventArgs.UserToken).Socket = acceptEventArgs.AcceptSocket;

                acceptEventArgs.AcceptSocket = null;
                this.PoolOfAcceptEventArgs.Push(acceptEventArgs);
                this.MaxAcceptOpsEnforcer.Release();

                log.Debug("<Session " + ((Session)recEventArgs.UserToken).Id + "> is now in use.");

                StartReceive(recEventArgs);
            }
            else
            {
                HandleBadAccept(acceptEventArgs);
                log.Fatal("Cannot handle this session, there are no more receive objects available for us.");
            }
        }

        private void IO_SendCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.LastOperation != SocketAsyncOperation.Send)
            {
                throw new InvalidOperationException("Tried to pass a send operation but the operation expected was not a send.");
            }

            ProcessSend(e);
        }

        private void IO_ReceiveCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.LastOperation != SocketAsyncOperation.Receive)
            {
                throw new InvalidOperationException("Tried to pass a receive operation but the operation expected was not a receive.");
            }

            ProcessReceive(e);
        }

        private void StartReceive(SocketAsyncEventArgs receiveEventArgs)
        {
            Session token = (Session)receiveEventArgs.UserToken;

            bool willRaiseEvent = token.Socket.ReceiveAsync(receiveEventArgs);

            if (!willRaiseEvent)
            {
                ProcessReceive(receiveEventArgs);
            }
        }

        private void ProcessReceive(SocketAsyncEventArgs receiveEventArgs)
        {
            Session token = (Session)receiveEventArgs.UserToken;

            if (receiveEventArgs.BytesTransferred > 0 && receiveEventArgs.SocketError == SocketError.Success)
            {
                byte[] dataReceived = new byte[receiveEventArgs.BytesTransferred];
                Buffer.BlockCopy(receiveEventArgs.Buffer, receiveEventArgs.Offset, dataReceived, 0, receiveEventArgs.BytesTransferred);
                token.OnReceiveData(dataReceived);

                StartReceive(receiveEventArgs);
            }
            else
            {
                CloseClientSocket(receiveEventArgs);
                ReturnReceiveSaea(receiveEventArgs);
            }
        }

        public void SendData(Socket socket, byte[] data)
        {
            this.MaxSaeaSendEnforcer.Wait();
            SocketAsyncEventArgs sendEventArgs;
            this.PoolOfSendEventArgs.TryPop(out sendEventArgs);

            SendDataToken token = (SendDataToken)sendEventArgs.UserToken;
            token.DataToSend = data;
            token.SendBytesRemainingCount = data.Length;

            sendEventArgs.AcceptSocket = socket;
            StartSend(sendEventArgs);
        }

        private void StartSend(SocketAsyncEventArgs sendEventArgs)
        {
            SendDataToken token = (SendDataToken)sendEventArgs.UserToken;

            if (token.SendBytesRemainingCount <= this.Settings.BufferSize)
            {
                sendEventArgs.SetBuffer(sendEventArgs.Offset, token.SendBytesRemainingCount);
                Buffer.BlockCopy(token.DataToSend, token.BytesSentAlreadyCount, sendEventArgs.Buffer, sendEventArgs.Offset, token.SendBytesRemainingCount);
            }
            else
            {
                sendEventArgs.SetBuffer(sendEventArgs.Offset, this.Settings.BufferSize);
                Buffer.BlockCopy(token.DataToSend, token.BytesSentAlreadyCount, sendEventArgs.Buffer, sendEventArgs.Offset, this.Settings.BufferSize);
            }

            bool willRaiseEvent = sendEventArgs.AcceptSocket.SendAsync(sendEventArgs);

            if (!willRaiseEvent)
            {
                ProcessSend(sendEventArgs);
            }
        }

        private void ProcessSend(SocketAsyncEventArgs sendEventArgs)
        {
            SendDataToken token = (SendDataToken)sendEventArgs.UserToken;

            if (sendEventArgs.SocketError == SocketError.Success)
            {
                token.SendBytesRemainingCount = token.SendBytesRemainingCount - sendEventArgs.BytesTransferred;

                if (token.SendBytesRemainingCount == 0)
                {
                    token.Reset();
                    ReturnSendSaea(sendEventArgs);
                }
                else
                {
                    token.BytesSentAlreadyCount += sendEventArgs.BytesTransferred;
                    StartSend(sendEventArgs);
                }
            }
            else
            {
                token.Reset();
                CloseClientSocket(sendEventArgs);
                ReturnSendSaea(sendEventArgs);
            }
        }

        private void CloseClientSocket(SocketAsyncEventArgs args)
        {
            Session con = (Session)args.UserToken;

            try
            {
                con.Socket.Shutdown(SocketShutdown.Both);
            }
            catch (SocketException) { }

            con.Socket.Close();

            log.Debug("<Session " + con.Id + "> is no longer in use.");
        }

        private void ReturnReceiveSaea(SocketAsyncEventArgs args)
        {
            this.PoolOfRecEventArgs.Push(args);
            this.MaxConnectionsEnforcer.Release();
        }

        private void ReturnSendSaea(SocketAsyncEventArgs args)
        {
            this.PoolOfSendEventArgs.Push(args);
            this.MaxSaeaSendEnforcer.Release();
        }

        private void HandleBadAccept(SocketAsyncEventArgs acceptEventArgs)
        {
            acceptEventArgs.AcceptSocket.Shutdown(SocketShutdown.Both);
            acceptEventArgs.AcceptSocket.Close();
            this.PoolOfAcceptEventArgs.Push(acceptEventArgs);
        }

        [Obsolete]
        public void Shutdown()
        {
            this.ListenSocket.Shutdown(SocketShutdown.Both);
            this.ListenSocket.Close();

            DisposeAllSaeaObjects();
        }

        private void DisposeAllSaeaObjects()
        {
            this.PoolOfAcceptEventArgs.Dispose();
            this.PoolOfSendEventArgs.Dispose();
            this.PoolOfRecEventArgs.Dispose();
        }
    }
}
