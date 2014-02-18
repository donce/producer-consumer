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
            BlockingCollection<int> firstCollection = new BlockingCollection<int>();
            BlockingCollection<int> secondCollection = new BlockingCollection<int>();
            Producer producer = new Producer(firstCollection, 10);
//            MiddleMan middleman = new MiddleMan(firstCollection, secondCollection);
            FilterWorker filterWorker = new FilterWorker(firstCollection, secondCollection, IsEven);
            Consumer consumer = new Consumer(secondCollection);
//            Consumer consumer2 = new Consumer(collection);

            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Factory.StartNew(producer.Run));
//            tasks.Add(Task.Factory.StartNew(middleman.Run));
            tasks.Add(Task.Factory.StartNew(filterWorker.Run));
            tasks.Add(Task.Factory.StartNew(consumer.Run));

            //TODO: "Use thread pools in mains"
            Task.WaitAll(tasks.ToArray());
        }

        static bool IsEven(int number)
        {
            return number%2 == 0;
        }
    }
}
