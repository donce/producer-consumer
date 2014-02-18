using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class BoundedBuffer : AbstractBuffer, IBuffer
    {
        private int _capacity;

        public BoundedBuffer(int capacity) : base(capacity)
        {
            _capacity = capacity;
        }

        public Boolean IsFull()
        {
            return Count() == _capacity;
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
                Monitor.Pulse(_lockObject);
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
                while (IsFull())
                {
                    Monitor.Wait(_lockObject);
                }
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
