using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class FilterWorker
    {
        private BlockingCollection<int> _collectionIn, _collectionOut;
        private Predicate<int> _filter; 
        
        public FilterWorker(BlockingCollection<int> collectionIn, BlockingCollection<int> collectionOut, Predicate<int> filter)
        {
            _collectionIn = collectionIn;
            _collectionOut = collectionOut;
            _filter = filter;
        }

        public void Run()
        {
            while (!_collectionIn.IsCompleted)
            {
                try
                {
                    int item = _collectionIn.Take();
                    if (_filter(item))
                        _collectionOut.Add(item);
                    Console.WriteLine("Take {0}.", item);
                }
                catch (InvalidOperationException) { }
            }
            _collectionOut.CompleteAdding();
        }
    }
}
