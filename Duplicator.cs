﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class Duplicator<T>
    {
        private Buffer<T> _collectionIn;
        private Buffer<T>[] _collectionsOut;

        public Duplicator(Buffer<T> collectionIn, Buffer<T>[] collectionsOut)
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
                    foreach (Buffer<T> collection in _collectionsOut)
                    {
                        collection.Add(item);
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
            foreach (Buffer<T> collection in _collectionsOut)
            {
                collection.CompleteAdding();
            }
        }
    }
}
