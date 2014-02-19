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
//            Duplicator duplicator = new Duplicator(collectionA, new BlockingCollection<int>[] {collectionB, collectionC});
            DivideWorker<int> divideWorker = new DivideWorker<int>(collectionA, new BlockingCollection<int>[] { collectionB, collectionC }, IntMod2);
            Consumer<int> consumer = new Consumer<int>(collectionA);
            Consumer<int> consumer2 = new Consumer<int>(collectionC);

            List<Task> tasks = new List<Task>();
//            tasks.Add(Task.Factory.StartNew(producer.Run));
//            tasks.Add(Task.Factory.StartNew(middleman.Run));
//            tasks.Add(Task.Factory.StartNew(filterWorker.Run));
//            divideWorker.Run();

            producer.Run();
            Console.WriteLine("A");
            consumer.Run();
//            Console.WriteLine("B");
//            consumer2.Run();
//            tasks.Add(Task.Factory.StartNew(consumer2.Run));

            //TODO: "Use thread pools in mains"
//            Task.WaitAll(tasks.ToArray());
        }

        static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        static int IntMod2(int number)
        {
            return number % 2;
        }
    }
}
