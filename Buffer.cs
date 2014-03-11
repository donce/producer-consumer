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
        protected string _name;

        private int producersLeft;

        public string Name
        {
            get { return _name; }
        }

        public Buffer(string name) : this(name, 1)
        {
        }


        public Buffer(string name, int producers)
        {
            _name = name;
            producersLeft = producers;
        }

        public new void Add(T item)
        {
            Thread.Sleep(1000);
            base.Add(item);
        }

        public new T Take()
        {
            T item = base.Take();
            Thread.Sleep(1000);
            return item;
        }

        public void CompleteAdding()
        {
            lock (this)
            {
                if (--producersLeft == 0)
                    base.CompleteAdding();
                if (producersLeft < 0)
                    throw new Exception("Trying to complete the buffer when it is already completed.");
            }
        }
    }
}
