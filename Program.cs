using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;

namespace ProducerCustomer
{
    class Program
    {
        private static List<Worker> workers = new List<Worker>();
        private static List<Task> tasks = new List<Task>();

        public static readonly ILog log = LogManager.GetLogger(typeof(Program));

        [STAThread]
        static void Main(string[] args)
        {
//            FileInfo fileInfo = ;
            FileInfo info = new FileInfo(@"../../logconfig.xml");
            Console.WriteLine(info);
            XmlConfigurator.Configure(info);
//            BasicConfigurator.Configure();
            log.Info("Program started");

//            ConsoleAppender appender = new ConsoleAppender();
//            LoggingEventData data = new LoggingEventData();
//            LoggingEvent myEvent = new LoggingEvent(data);
//            appender.DoAppend(myEvent);
//            ILog logger = 

            Buffer<int> collectionA = new Buffer<int>("First");
            Buffer<int> collectionB = new Buffer<int>("Second");
            Buffer<int> collectionC = new Buffer<int>("Third");
            Buffer<int> collectionD = new Buffer<int>("Fourth");

            ShowCollection(collectionA);
            ShowCollection(collectionB);
            ShowCollection(collectionC);
            ShowCollection(collectionD);

            AddWorker(new Producer(collectionA, 30));
            AddWorker(new FilterWorker<int>(collectionA, collectionB, IsPrime));
            AddWorker(new DivideWorker<int>(collectionB, new Buffer<int>[] { collectionC, collectionD }, DigitSumMod2));
//            AddWorker(new Consumer<int>(collectionC));
//            AddWorker(new Consumer<int>(collectionD));


            StartForm(new ControlWindow());
        }

        public static void Start()
        {
            StartWorkers();
//            Task.WaitAll(tasks.ToArray());
        }

        static void AddWorker(Worker worker)
        {
            workers.Add(worker);
            StartForm(new WorkerWindow(worker));
        }

        static void StartWorkers()
        {
            log.Info("Workers starting...");
            foreach (Worker worker in workers)
            {
                tasks.Add(Task.Factory.StartNew(worker.Start));
            }
        }

        static void ShowCollection<T>(Buffer<T> collection)
        {
            StartForm(new BufferWindow<T>(collection));
        }

        static Thread StartForm(Form form)
        {
            log.Info("Form starting...");
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

        static int DigitSumMod2(int number)
        {
            int sum = 0;
            while (number != 0)
            {
                sum += number%10;
                number /= 10;
            }
            return sum % 2;
        }
    }
}
