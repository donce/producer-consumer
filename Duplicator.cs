using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class Duplicator
    {
        private IBuffer _bufferIn;
        private IBuffer[] _buffersOut;

        public Duplicator(IBuffer bufferIn, IBuffer[] buffersOut)
        {
            _bufferIn = bufferIn;
            _buffersOut = buffersOut;
        }

        public void Run()
        {
            int item;
            do
            {
                item = _bufferIn.Take();
                foreach (IBuffer buffer in _buffersOut)
                {
                    buffer.Put(item);
                }
            } while (item != Producer.LastElement);
        }
    }
}
