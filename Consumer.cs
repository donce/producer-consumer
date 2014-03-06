using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class Consumer<T> : Worker<T>
    {
        private readonly Buffer<T> _collection;
        
        public Consumer(Buffer<T> collection) : base("Consumer")
        {
            if (collection == null)
                throw new ArgumentNullException();
            _collection = collection;
        }

        protected override void Run()
        {
            while (!_collection.IsCompleted)
            {
                try
                {
                    T item = _collection.Take();
                }
                catch (InvalidOperationException) { }
            }
        }

    }
}
