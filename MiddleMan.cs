using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class MiddleMan<T>
    {
        private BlockingCollection<T> _collectionIn, _collectionOut;

        public MiddleMan(BlockingCollection<T> collectionIn, BlockingCollection<T> collectionOut)
        {
            _collectionIn = collectionIn;
            _collectionOut = collectionOut;
        }

        public void Run()
        {
            while (!_collectionIn.IsCompleted)
            {
                try
                {
                    T item = _collectionIn.Take();
                    _collectionOut.Add(item);
                    Console.WriteLine("Take {0}.", item);
                }
                catch (InvalidOperationException) { }
            }
            _collectionOut.CompleteAdding();
        }
    }
}
