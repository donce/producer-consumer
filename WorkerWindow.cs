using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProducerCustomer
{
    public partial class WorkerWindow : Form
    {
        private Worker worker;
        public WorkerWindow(Worker worker)
        {
            if (worker == null)
                throw new ArgumentNullException("worker");
            this.worker = worker;
            InitializeComponent();
            label1.Text = worker.name;
        }

        private void updateWindow()
        {
            while (true)
            {
            }
        }

        private void WorkerWindow_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(updateWindow);
        }
    }
}
