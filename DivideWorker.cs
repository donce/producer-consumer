using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class DivideWorker<T> : Worker<T>
    {
        public delegate int HashDelegate(T item);
        
        private readonly Buffer<T> _collectionIn;
        private readonly Buffer<T>[] _collectionsOut;
        private readonly HashDelegate _hashFunction;

        public DivideWorker(Buffer<T> collectionIn, Buffer<T>[] collectionsOut, HashDelegate hashFunction) : base("Divide Worker")
        {
            _collectionIn = collectionIn;
            _collectionsOut = collectionsOut;
            _hashFunction = hashFunction;
        }

        protected override void Run()
        {
            while (!_collectionIn.IsCompleted)
            {
                try
                {
                    T item = _collectionIn.Take();
                    Current = item;
                    int index = _hashFunction(item);
                    if (index < 0 || index >= _collectionsOut.Length)
                        throw new Exception();
                    _collectionsOut[index].Add(item);
                }
                catch (InvalidOperationException)
                {
                }
            }
            Current = default(T);
            foreach (Buffer<T> collection in _collectionsOut)
            {
                collection.CompleteAdding();
            }
        }
    }
}
