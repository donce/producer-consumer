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

        public string Name
        {
            get { return _name; }
        }

        public Buffer(string name)
        {
            _name = name;
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
    }
}
