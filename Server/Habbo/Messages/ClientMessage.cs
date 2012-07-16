using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageServer.Server.Habbo.Messages
{
    /// <summary>
    /// A class to parse client messages (client->server)
    /// </summary>
    class ClientMessage
    {
        private short _headerID;
        private byte[] _content;
        private short _reader;

        public short HeaderID { get { return _headerID; } }

        public static ClientMessage Parse(byte[] data)
        {
            ClientMessage request = new ClientMessage();
            request._content = data;
            request._headerID = request.ReadInt16();

            return request;
        }
        public string ReadString()
        {
            short length = ReadInt16();
            string s = Encoding.UTF8.GetString(_content, _reader, length);
            _reader += length;

            return s;
        }
        public int ReadInt32()
        {
            int i = BitConverter.ToInt32(new[] { _content[_reader + 3], _content[_reader + 2], _content[_reader + 1], _content[_reader] }, 0);
            _reader += 4;
            return i;
        }
        public short ReadInt16()
        {
            short s = BitConverter.ToInt16(new[] { _content[_reader + 1], _content[_reader] }, 0);
            _reader += 2;
            return s;
        }
        public bool ReadBoolean()
        {
            return BitConverter.ToBoolean(_content, _reader++);
        }
    }
}
