using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class Consumer<T> : Worker
    {
        private readonly BlockingCollection<T> _collection;
        
        public Consumer(BlockingCollection<T> collection) : base("Consumer")
        {
            if (collection == null)
                throw new ArgumentNullException();
            _collection = collection;
        }

        public override void Run()
        {
            while (!_collection.IsCompleted)
            {
                try
                {
                    T item = _collection.Take();
                    Console.WriteLine("Take {0}.", item);
                }
                catch (InvalidOperationException) { }
            }
        }

    }
}
