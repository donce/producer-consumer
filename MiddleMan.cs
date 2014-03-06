using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class MiddleMan<T> : Worker<T>
    {
        private Buffer<T> _collectionIn, _collectionOut;

        public MiddleMan(Buffer<T> collectionIn, Buffer<T> collectionOut) : base("Middle man")
        {
            _collectionIn = collectionIn;
            _collectionOut = collectionOut;
        }

        protected override void Run()
        {
            while (!_collectionIn.IsCompleted)
            {
                try
                {
                    T item = _collectionIn.Take();
                    _collectionOut.Add(item);
                }
                catch (InvalidOperationException) { }
            }
            _collectionOut.CompleteAdding();
        }
    }
}
