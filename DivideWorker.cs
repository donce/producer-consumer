using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class DivideWorker
    {
        public delegate int HashDelegate(int item);
        
        private readonly BlockingCollection<int> _collectionIn;
        private readonly BlockingCollection<int>[] _collectionsOut;
        private readonly HashDelegate _hashFunction;

        public DivideWorker(BlockingCollection<int> collectionIn, BlockingCollection<int>[] collectionsOut, HashDelegate hashFunction)
        {
            _collectionIn = collectionIn;
            _collectionsOut = collectionsOut;
            _hashFunction = hashFunction;
        }

        public void Run()
        {
            while (!_collectionIn.IsCompleted)
            {
                try
                {
                    int item = _collectionIn.Take();
                    int index = _hashFunction(item);
                    if (index < 0 || index >= _collectionsOut.Length)
                        throw new Exception();
                    _collectionsOut[index].Add(item);
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
