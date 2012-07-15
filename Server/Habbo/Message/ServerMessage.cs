using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageServer.Server.Habbo.Message
{
    /// <summary>
    /// A class to handle server messages (server->client)
    /// </summary>
    class ServerMessage
    {
        /// <summary>
        /// The id of the message (stored as a short)
        /// </summary>
        private short id;

        /// <summary>
        /// The message buffer
        /// </summary>
        private StringBuilder body;

        public ServerMessage(int id, int length)
        {
            this.body = new StringBuilder(length);
            this.id = (short)id;
        }

        public ServerMessage(int id)
        {
            this.body = new StringBuilder(1024);
            this.id = (short)id;
        }

        public void appendInt(int i)
        {
            this.body.append(i);
        }

        public void appendShort(short s)
        {
            this.body.append(s);
        }

        public void appendChar(char c)
        {
            this.body.append(c);
        }

        public void appendByte(byte b)
        {
            this.body.append(b);
        }

        public void appendDouble(double d)
        {
            this.body.append(d);
        }

        public void appendBytes(byte[] b)
        {
            this.body.append(b);
        }

        public void appendString(string s)
        {
            this.body.append(s);
        }

        public string getBody()
        {
            return this.body.toString();
        }
        
    }
}
