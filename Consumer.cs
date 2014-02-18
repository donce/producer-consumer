using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class Consumer
    {
        private readonly IBuffer _buffer;
        
        public Consumer(IBuffer buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException();
            _buffer = buffer;
        }

        public void Run()
        {
            int item = _buffer.Take();
            while (item != Producer.LastElement)
            {
                Console.WriteLine("Take {0}.", item);
                item = _buffer.Take();
            }
        }

    }
}
