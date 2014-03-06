using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class FilterWorker<T> : Worker<T>
    {
        private Buffer<T> _collectionIn, _collectionOut;
        private Predicate<T> _filter; 
        
        public FilterWorker(Buffer<T> collectionIn, Buffer<T> collectionOut, Predicate<T> filter) : base("Filter worker")
        {
            _collectionIn = collectionIn;
            _collectionOut = collectionOut;
            _filter = filter;
        }

        protected override void Run()
        {
            while (!_collectionIn.IsCompleted)
            {
                try
                {
                    T item = _collectionIn.Take();
                    Current = item;
                    if (_filter(item))
                        _collectionOut.Add(item);
                }
                catch (InvalidOperationException) { }
            }
            _collectionOut.CompleteAdding();
            Current = default(T);
        }
    }
}
