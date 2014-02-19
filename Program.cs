using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
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
            BlockingCollection<int> collectionA = new BlockingCollection<int>();
            BlockingCollection<int> collectionB = new BlockingCollection<int>();
            BlockingCollection<int> collectionC = new BlockingCollection<int>();
            
            Producer producer = new Producer(collectionA, 10);
//            MiddleMan middleman = new MiddleMan(firstCollection, secondCollection);
//            FilterWorker filterWorker = new FilterWorker(firstCollection, secondCollection, IsEven);
//            BlockingCollection<int>[] c = ;
            Duplicator duplicator = new Duplicator(collectionA, new BlockingCollection<int>[] {collectionB, collectionC});
//            DivideWorker divideWorker = new DivideWorker();
            Consumer consumer = new Consumer(collectionB);
            Consumer consumer2 = new Consumer(collectionC);

            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Factory.StartNew(producer.Run));
//            tasks.Add(Task.Factory.StartNew(middleman.Run));
//            tasks.Add(Task.Factory.StartNew(filterWorker.Run));
            duplicator.Run();

            Thread.Sleep(50);
            Console.WriteLine("A");
            consumer.Run();
            Console.WriteLine("B");
            consumer2.Run();
//            tasks.Add(Task.Factory.StartNew(consumer2.Run));

            //TODO: "Use thread pools in mains"
//            Task.WaitAll(tasks.ToArray());
        }

        static bool IsEven(int number)
        {
            return number%2 == 0;
        }

        static int IntGetHashCode(int number)
        {
            //TODO: implement
            return number % 3;
        }
    }
}
