using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class DivideWorker<T>
    {
        public delegate int HashDelegate(T item);
        
        private readonly BlockingCollection<T> _collectionIn;
        private readonly BlockingCollection<T>[] _collectionsOut;
        private readonly HashDelegate _hashFunction;

        public DivideWorker(BlockingCollection<T> collectionIn, BlockingCollection<T>[] collectionsOut, HashDelegate hashFunction)
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
                    T item = _collectionIn.Take();
                    int index = _hashFunction(item);
                    if (index < 0 || index >= _collectionsOut.Length)
                        throw new Exception();
                    _collectionsOut[index].Add(item);
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
