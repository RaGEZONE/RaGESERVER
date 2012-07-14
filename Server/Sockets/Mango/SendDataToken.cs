using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mango.Communication
{
    sealed class SendDataToken
    {
        private int sendBytesRemainingCount = 0;
        private int bytesSentAlreadyCount = 0;
        private byte[] dataToSend = null;

        public int SendBytesRemainingCount
        {
            get
            {
                return sendBytesRemainingCount;
            }
            set
            {
                this.sendBytesRemainingCount = value;
            }
        }

        public int BytesSentAlreadyCount
        {
            get
            {
                return this.bytesSentAlreadyCount;
            }
            set
            {
                this.bytesSentAlreadyCount = value;
            }
        }

        public byte[] DataToSend
        {
            get
            {
                return this.dataToSend;
            }
            set
            {
                this.dataToSend = value;
            }
        }

        public void Reset()
        {
            this.sendBytesRemainingCount = 0;
            this.BytesSentAlreadyCount = 0;
            Array.Clear(DataToSend, 0, DataToSend.Length);
            this.dataToSend = null;
        }
    }
}
