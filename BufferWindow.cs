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
        private BlockingCollection<T> collection; 
        public BufferWindow(BlockingCollection<T> collection)
        {
            this.collection = collection;
            InitializeComponent();
        }

        public void updateWindow()
        {
            while (true)
            {
                T[] array = collection.ToArray();
                try
                {
                    this.Invoke((MethodInvoker) delegate
                    {
                        String text = "";
                        foreach (T item in array)
                        {
                            text += (text != "" ? " " : "") + item;
                        }
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
