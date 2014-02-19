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
        public delegate int HashDelegate(int value);

        private BlockingCollection<int> _collectionIn;
        private BlockingCollection<int>[] _collectionsOut;
        private HashDelegate _hashFunction;


        public DivideWorker(BlockingCollection<int> collectionIn, BlockingCollection<int>[] collectionsOut, HashDelegate hashFunction)
        {
            _collectionIn = collectionIn;
            _collectionsOut = collectionsOut;
            _hashFunction = hashFunction;
        }

        public void Run()
        {
            int item;
            do
            {
                item = _collectionIn.Take();
                int index = _hashFunction(item);
                if (index < 0 || index >= _collectionsOut.Length)
                    throw new Exception();
                _collectionsOut[index].Add(item);
            } while (item != Producer.LastElement);
        }
    }
}
