using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class Producer<T> : Worker<T>
    {
        private Buffer<T> _collection;
        private int _howMany;
        private IElementFactory<T> _factory; 

        public Producer(Buffer<T> collection, int howMany, IElementFactory<T> factory) : base("Producer")
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            if (howMany < 0)
                throw new ArgumentOutOfRangeException("howMany");
            _collection = collection;
            _howMany = howMany;
            _factory = factory;
        }

        protected override void Run()
        {
            for (int i = 1; i <= _howMany; ++i)
            {
                T now = _factory.GetElement(i);
                Current = now;
                _collection.Add(now);
            }
            _collection.CompleteAdding();
            Current = default(T);
        }
    }
}
