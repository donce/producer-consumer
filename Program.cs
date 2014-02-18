using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class Program
    {
        static void Main(string[] args)
        {
            BoundedBuffer buffer = new BoundedBuffer(1);
            Producer producer = new Producer(buffer, 10);
            Consumer consumer = new Consumer(buffer);
            Consumer consumer2 = new Consumer(buffer);
            Task[] tasks = new Task[3];
            tasks[2] = Task.Factory.StartNew(consumer2.Run);
            tasks[1] = Task.Factory.StartNew(consumer.Run);
            Thread.Sleep(1000);
            tasks[0] = Task.Factory.StartNew(producer.Run);
//            tasks[2] = Task.Factory.StartNew(consumer2.Run);

            //TODO: "Use thread pools in mains"

            Task.WaitAll(tasks);
        }
    }
}
