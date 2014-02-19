using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class Duplicator<T>
    {
        private BlockingCollection<T> _collectionIn;
        private BlockingCollection<T>[] _collectionsOut;

        public Duplicator(BlockingCollection<T> collectionIn, BlockingCollection<T>[] collectionsOut)
        {
            _collectionIn = collectionIn;
            _collectionsOut = collectionsOut;
        }

        public void Run()
        {
            while (!_collectionIn.IsCompleted)
            {
                try
                {
                    T item = _collectionIn.Take();
                    foreach (BlockingCollection<T> collection in _collectionsOut)
                    {
                        collection.Add(item);
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
            foreach (BlockingCollection<T> collection in _collectionsOut)
            {
                collection.CompleteAdding();
            }
        }
    }
}
