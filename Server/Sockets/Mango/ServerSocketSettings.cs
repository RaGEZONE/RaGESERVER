using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Mango.Communication
{
    class ServerSocketSettings
    {
        /// <summary>
        /// The maximum number of connections allowed to connect to this server.
        /// </summary>
        public int MaxConnections { get; set; }

        /// <summary>
        /// This setting sets the number of SocketAsyncEventArgs to allocated for receive operations.
        /// </summary>
        public int NumOfSaeaForRec { get; set; }

        /// <summary>
        /// This setting sets the number of SocketAsyncEventArgs to allocated for send operations.
        /// </summary>
        public int NumOfSaeaForSend { get; set; }

        /// <summary>
        /// Maximum number of pending connections to hold in the queue before rejecting immediately.
        /// </summary>
        public int Backlog { get; set; }

        /// <summary>
        /// Defines the amount of objects to place in the pool for accepting connections.
        /// </summary>
        public int MaxSimultaneousAcceptOps { get; set; }

        /// <summary>
        /// The buffer size for receiving data, set this to a respectable size. (The higher the buffer size, the more memory the server will use up)
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        /// Ops to Pre-Allocate.
        /// </summary>
        public int OpsToPreAllocate { get; set; }

        /// <summary>
        /// The IP Address endpoint for this server to listen on.
        /// </summary>
        public IPEndPoint Endpoint { get; set; }
    }
}
