using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class FilterWorker<T> : Worker
    {
        private BlockingCollection<T> _collectionIn, _collectionOut;
        private Predicate<T> _filter; 
        
        public FilterWorker(BlockingCollection<T> collectionIn, BlockingCollection<T> collectionOut, Predicate<T> filter) : base("Filter worker")
        {
            _collectionIn = collectionIn;
            _collectionOut = collectionOut;
            _filter = filter;
        }

        public override void Run()
        {
            while (!_collectionIn.IsCompleted)
            {
                try
                {
                    T item = _collectionIn.Take();
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
