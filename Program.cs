using System;
using System.Collections.Concurrent;
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
            BlockingCollection<int> collection = new BlockingCollection<int>();
            Producer producer = new Producer(collection, 10);
            Consumer consumer = new Consumer(collection);
            Consumer consumer2 = new Consumer(collection);
            Task[] tasks = new Task[3];
            tasks[2] = Task.Factory.StartNew(consumer2.Run);
            tasks[1] = Task.Factory.StartNew(consumer.Run);
            Thread.Sleep(1000);
            tasks[0] = Task.Factory.StartNew(producer.Run);

            //TODO: "Use thread pools in mains"
            Task.WaitAll(tasks);
        }
    }
}
