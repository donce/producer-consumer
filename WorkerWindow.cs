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
        private Worker _worker;
        public WorkerWindow(Worker worker)
        {
            if (worker == null)
                throw new ArgumentNullException("worker");
            _worker = worker;
            InitializeComponent();
            label1.Text = worker.name;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
