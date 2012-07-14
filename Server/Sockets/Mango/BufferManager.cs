using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Mango.Communication
{
    sealed class BufferManager
    {
        private int totalBytesInBufferBlock;

        private byte[] bufferBlock;
        private Stack<int> freeIndexPool;
        private int currentIndex;
        private int bufferBytesAllocatedForEachSaea;

        public BufferManager(int totalBytes, int totalBufferBytesInEachSaeaObject)
        {
            this.totalBytesInBufferBlock = totalBytes;
            this.currentIndex = 0;
            this.bufferBytesAllocatedForEachSaea = totalBufferBytesInEachSaeaObject;
            this.freeIndexPool = new Stack<int>();
        }

        public void InitBuffer()
        {
            this.bufferBlock = new byte[totalBytesInBufferBlock];
        }

        public bool SetBuffer(SocketAsyncEventArgs args)
        {
            if (this.freeIndexPool.Count > 0)
            {
                args.SetBuffer(this.bufferBlock, this.freeIndexPool.Pop(), this.bufferBytesAllocatedForEachSaea);
            }
            else
            {
                if ((totalBytesInBufferBlock - this.bufferBytesAllocatedForEachSaea) < this.currentIndex)
                {
                    return false;
                }

                args.SetBuffer(this.bufferBlock, this.currentIndex, this.bufferBytesAllocatedForEachSaea);
                this.currentIndex += this.bufferBytesAllocatedForEachSaea;
            }

            return true;
        }

        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            this.freeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }
    }
}
