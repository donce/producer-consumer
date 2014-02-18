using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class AbstractBuffer
    {
        private Queue<int> _queue;
        protected readonly Object _lockObject = new Object();

        public AbstractBuffer(int capacity)
        {
            if (capacity >= 0)
                _queue = new Queue<int>(capacity);
            else
                _queue = new Queue<int>();
        }

        protected void Push(int element)
        {
            _queue.Enqueue(element);
            Console.WriteLine(">Putting");
        }

        protected int Pop()
        {
            int item = _queue.Peek();
            if (item != Producer.LastElement)
                _queue.Dequeue();
            return item;
        }

        public int Count()
        {
            return _queue.Count;
        }

        protected bool IsEmpty()
        {
            return Count() == 0;
        }
    }
}
