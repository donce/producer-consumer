using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class UnboundedBuffer : AbstractBuffer, IBuffer
    {
        public UnboundedBuffer() : base(-1)
        {
        }

        public int Take()
        {
            bool token = false;
            try
            {
                Monitor.Enter(_lockObject, ref token);
                while (IsEmpty())
                {
                    Monitor.Wait(_lockObject);
                }
                Console.WriteLine(">Taking");
                return Pop();
            }
            finally
            {
                if (token)
                    Monitor.Exit(_lockObject);
            }
        }

        public void Put(int element)
        {
            bool token = false;
            try
            {
                Monitor.Enter(_lockObject, ref token);
                Push(element);
                Monitor.Pulse(_lockObject);
            }
            finally
            {
                if (token)
                    Monitor.Exit(_lockObject);
            }
        }
    }
}
