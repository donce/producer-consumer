﻿using System;
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

        [STAThread]
        static void Main(string[] args)
        {
//            new BufferWindow().Visible = true;
//            Task.Factory.StartNew(() => new BufferWindow().Visible = true);
//            BufferWindow window = new BufferWindow();
//            window.Visible = true;
//            window.
//            window.Update();
//            window.Activate();


//            Console.WriteLine(window.Visible);
            BlockingCollection<int> collectionA = new BlockingCollection<int>();
            BlockingCollection<int> collectionB = new BlockingCollection<int>();
//            BlockingCollection<int> collectionC = new BlockingCollection<int>();
//            BlockingCollection<int> collectionD = new BlockingCollection<int>();

            showCollection(collectionA);
            showCollection(collectionB);

            Producer producer = new Producer(collectionA, 10);
            startWorker(producer);
            FilterWorker<int> filterWorker = new FilterWorker<int>(collectionA, collectionB, IsPrime);
            startWorker(filterWorker);
//            DivideWorker<int> divideWorker = new DivideWorker<int>(collectionB, new BlockingCollection<int>[] { collectionC, collectionD }, IntMod2);
//            Consumer<int> consumerA = new Consumer<int>(collectionC);
//            Consumer<int> consumerB = new Consumer<int>(collectionD);

            List<Task> tasks = new List<Task>();
//            tasks.Add(Task.Factory.StartNew(producer.Run));
//            tasks.Add(Task.Factory.StartNew(filterWorker.Run));
//            tasks.Add(Task.Factory.StartNew(divideWorker.Run));
//            tasks.Add(Task.Factory.StartNew(consumerA.Run));
//            tasks.Add(Task.Factory.StartNew(consumerB.Run));

//            Application.Run(new BufferWindow<int>(collectionA));
//            Application.Run(new WorkerWindow(producer));
//            tasks.Add(Task.Factory.StartNew(() => Application.Run(new WorkerWindow(producer))));
//            Task task = new Task();
//            task.Set
//            Thread t = new Thread(() => Application.Run(new WorkerWindow(producer)));
//            t.SetApartmentState(ApartmentState.STA);
//            t.Start();

//            startForm(new WorkerWindow(producer));
//            startForm(new WorkerWindow(producer));
//            while (true) ;

            //TODO: "Use thread pools in mains"
            Task.WaitAll(tasks.ToArray());
        }

        static void startWorker(Worker worker)
        {
            Task.Factory.StartNew(worker.Run);
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
