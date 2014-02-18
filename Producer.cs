using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class Producer
    {
        public static int LastElement = -1;

        private IBuffer _buffer;
        private int _howMany;

        public Producer(IBuffer buffer, int howMany)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if (howMany < 0)
                throw new ArgumentOutOfRangeException("howMany");
            _buffer = buffer;
            _howMany = howMany;
        }

        public void Run()
        {
            for (int i = 1; i <= _howMany; ++i)
            {
                _buffer.Put(i);
                Console.WriteLine("Put {0}.", i);
            }
            _buffer.Put(LastElement);
        }
    }
}
