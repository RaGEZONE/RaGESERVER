using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace Mango.Communication
{
    sealed class SocketAsyncEventArgsPool
    {
        private ConcurrentStack<SocketAsyncEventArgs> pool;

        public SocketAsyncEventArgsPool(int capacity)
        {
            this.pool = new ConcurrentStack<SocketAsyncEventArgs>();
        }

        public bool TryPop(out SocketAsyncEventArgs args)
        {
            return this.pool.TryPop(out args);
        }

        public void Push(SocketAsyncEventArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("Items added to a SocketAsyncEventArgsPool cannot be null");
            }

            this.pool.Push(args);
        }

        public void Dispose()
        {
            SocketAsyncEventArgs eventArgs;

            while (this.pool.Count > 0)
            {
                if (this.pool.TryPop(out eventArgs))
                {
                    eventArgs.Dispose();
                }
            }
        }
    }
}
