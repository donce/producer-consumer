using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class MiddleMan
    {
        private IBuffer _bufferIn, _bufferOut;

        public MiddleMan(IBuffer bufferIn, IBuffer bufferOut)
        {
            _bufferIn = bufferIn;
            _bufferOut = bufferOut;
        }

        public void Run()
        {
            int item;
            do
            {
                item = _bufferIn.Take();
                _bufferOut.Put(item);
            } while (item != Producer.LastElement);
        }
    }
}
