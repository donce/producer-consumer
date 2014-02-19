using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class Duplicator
    {
        private BlockingCollection<int> _collectionIn;
        private BlockingCollection<int>[] _collectionsOut;

        public Duplicator(BlockingCollection<int> collectionIn, BlockingCollection<int>[] collectionsOut)
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
                    int item = _collectionIn.Take();
                    foreach (BlockingCollection<int> collection in _collectionsOut)
                    {
                        collection.Add(item);
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
            foreach (BlockingCollection<int> collection in _collectionsOut)
            {
                collection.CompleteAdding();
            }
        }
    }
}
