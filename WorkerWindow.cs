using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProducerCustomer
{
    public partial class WorkerWindow : Form
    {
        private Worker<int> worker;
        private Worker<int>.State lastState = Worker<int>.State.New;
        private int lastCurrent = default(int);

        public WorkerWindow(Worker<int> worker)
        {
            if (worker == null)
                throw new ArgumentNullException("worker");
            this.worker = worker;
            InitializeComponent();
            this.Text = worker.name;
//            nameLabel.Text = worker.name;
        }

        private void updateWindow()
        {
            while (true)
            {
                if (worker.state != lastState || worker.Current != lastCurrent)
                {
                    try
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            stateLabel.Text = worker.stateTitle;
                            currentLabel.Text = worker.Current == 0 ? "Empty" : worker.Current.ToString();
                        });
                    }
                    catch (ObjectDisposedException)
                    {
                        return;
                    }
                    lastState = worker.state;
                    lastCurrent = worker.Current;
                }
                Thread.Sleep(200);
            }
        }

        private void WorkerWindow_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(updateWindow);
        }
    }
}
