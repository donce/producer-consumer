using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProducerCustomer
{
    class Program
    {
        private static List<Task> tasks = new List<Task>();
            
        [STAThread]
        static void Main(string[] args)
        {
            BlockingCollection<int> collectionA = new BlockingCollection<int>();
//            BlockingCollection<int> collectionB = new BlockingCollection<int>();
//            BlockingCollection<int> collectionC = new BlockingCollection<int>();
//            BlockingCollection<int> collectionD = new BlockingCollection<int>();

            showCollection(collectionA);
//            showCollection(collectionB);

            startWorker(new Producer(collectionA, 10));
//            startWorker(new FilterWorker<int>(collectionA, collectionB, IsPrime));
//            startWorker(new DivideWorker<int>(collectionB, new BlockingCollection<int>[] { collectionC, collectionD }, IntMod2));
//            startWorker(new Consumer<int>(collectionC));
//            startWorker(new Consumer<int>(collectionD));

            Task.WaitAll(tasks.ToArray());
        }

        static void startWorker(Worker worker)
        {
            tasks.Add(Task.Factory.StartNew(worker.Run));
            startForm(new WorkerWindow(worker));
        }

        static void showCollection<T>(BlockingCollection<T> collection)
        {
            startForm(new BufferWindow<T>(collection));
        }

        static Thread startForm(Form form)
        {
            Thread thread = new Thread(() => Application.Run(form));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return thread;
        }

        static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        static bool IsPrime(int number)
        {
            if (number <= 1)
                return false;
            for (int i = 2; i*i <= number; ++i)
                if (number%i == 0)
                    return false;
            return true;
        }

        static int IntMod2(int number)
        {
            return number % 2;
        }
    }
}
