using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProducerCustomer
{
    public partial class BufferWindow<T> : Form
    {
        private Buffer<T> collection; 
        public BufferWindow(Buffer<T> collection)
        {
            this.collection = collection;
            InitializeComponent();
        }

        private void updateWindow()
        {
            while (true)
            {
                String text = "";
                foreach (T item in collection.ToArray())
                {
                    text += (text != "" ? " " : "") + item;
                }
                try
                {
                    this.Invoke((MethodInvoker) delegate
                    {
                        if (label1.Text != text)
                            label1.Text = text;
                    });
                }
                catch (InvalidOperationException)
                {
                    return;
                }
                Thread.Sleep(500);
            }
        }

        private void BufferWindow_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(updateWindow);
        }
    }
}
