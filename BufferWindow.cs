using System;
using System.Collections.Concurrent;
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
    public partial class BufferWindow<T> : Form
    {
        public BufferWindow(BlockingCollection<T> collection)
        {
            InitializeComponent();
        }
    }
}
