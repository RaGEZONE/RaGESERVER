using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageServer.Server.Habbo.Messages
{
    /// <summary>
    /// A class to construct server messages (server->client)
    /// </summary>
    class ServerMessage
    {
        private List<byte> _builder;

        public ServerMessage(short messageID)
        {
            Init(messageID);
        }
        public ServerMessage()
        {
            _builder = new List<byte>();
        }
        public void Init(short messageID)
        {
            _builder = new List<byte>();
            AppendInt16(messageID);
        }

        public void Append(object o)
        {
            _builder.AddRange(Encoding.UTF8.GetBytes(o.ToString()));
        }
        public void AppendChar(int i)
        {
            Append(Convert.ToChar(i));
        }
        public void AppendByte(byte b)
        {
            _builder.Add(b);
        }
        public void AppendString(string s)
        {
            _builder.AddRange(Reorder(BitConverter.GetBytes((short)s.Length)));
            Append(s);
        }
        public void AppendInt32(int i)
        {
            byte[] b = BitConverter.GetBytes(i);
            Array.Reverse(b);
            _builder.AddRange(b);
        }
        public void AppendInt16(short i)
        {
            byte[] b = BitConverter.GetBytes(i);
            Array.Reverse(b);
            _builder.AddRange(b);
        }
        public void AppendBoolean(bool b)
        {
            _builder.Add(b ? (byte)1 : (byte)0);
        }

        public List<byte> ToBytes()
        {
            List<byte> sendMessage = new List<byte>();

            sendMessage.AddRange(BitConverter.GetBytes(_builder.Count));
            sendMessage.Reverse();
            sendMessage.AddRange(_builder);
            return sendMessage;
        }
        public bool HasContent()
        {
            return _builder.Count > 2;
        }
        private static byte[] Reorder(byte[] b)
        {
            Array.Reverse(b);
            return b;
        }
    }
}
