using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    public class Buffer<T> : BlockingCollection<T>
    {
        public new void Add(T item)
        {
            Thread.Sleep(1000);
            base.Add(item);
        }

        public new T Take()
        {
            Thread.Sleep(1000);
            return base.Take();
        }
    }
}
