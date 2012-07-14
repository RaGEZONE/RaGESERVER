using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace Mango.Communication.Sessions
{
    sealed class Session
    {

        /// <summary>
        /// Unique ID which indentifies this Session. (It may only be used for debugging purposes)
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// The manager that was used to create this session.
        /// </summary>
        public ServerSocket Manager { get; private set; }

        /// <summary>
        /// The Socket used for the backend of the communication.
        /// </summary>
        public Socket Socket { get; set; }

        /// <summary>
        /// Initializes a new instance of the Session class.
        /// </summary>
        /// <param name="manager">The manager which created this session.</param>
        public Session(int id, ServerSocket manager)
        {
            this.Id = id;
            this.Manager = manager;
        }

        /// <summary>
        /// Gets the IP Address of this connection session.
        /// </summary>
        public string IPAddress
        {
            get { return this.Socket.RemoteEndPoint.ToString().Split(':')[0]; }
        }

        /// <summary>
        /// This method is called when data incoming has been received.
        /// </summary>
        /// <param name="data">The array of data.</param>
        public void OnReceiveData(byte[] data)
        {
           // R63 Protcol Data goes here
        }

        private bool DisconnectedCalled = false;

        /// <summary>
        /// Forces this player to be disconnected
        /// </summary>
        public void Disconnect()
        {
            if (!this.DisconnectedCalled)
            {
                this.DisconnectedCalled = true;
                this.Socket.Disconnect(true); // This needs improving (DisconnectAsync)
            }
        }

        public void SendData(byte[] data)
        {
            this.Manager.SendData(this.Socket, data);
        }

        public void SendData(string Data)
        {
            SendData(Encoding.UTF8.GetBytes(Data));
        }
    }
}
