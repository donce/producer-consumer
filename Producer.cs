using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class Producer
    {
        private BlockingCollection<int> _collection;
        private int _howMany;

        public Producer(BlockingCollection<int> collection, int howMany)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            if (howMany < 0)
                throw new ArgumentOutOfRangeException("howMany");
            _collection = collection;
            _howMany = howMany;
        }

        public void Run()
        {
            for (int i = 1; i <= _howMany; ++i)
            {
                _collection.Add(i);
                Console.WriteLine("Put {0}.", i);
            }
            _collection.CompleteAdding();
        }
    }
}
