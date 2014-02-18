using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class Consumer
    {
        private readonly BlockingCollection<int> _collection;
        
        public Consumer(BlockingCollection<int> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();
            _collection = collection;
        }

        public void Run()
        {
            while (!_collection.IsCompleted)
            {
                try
                {
                    int item = _collection.Take();
                    Console.WriteLine("Take {0}.", item);
                }
                catch (InvalidOperationException) { }
            }
        }

    }
}
