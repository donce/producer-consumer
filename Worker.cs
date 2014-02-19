using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    abstract public class Worker
    {
        private readonly String _name;

        public String name
        {
            get { return _name; }
        }

        protected Worker(String name)
        {
            _name = name;
        }

        public abstract void Run();
    }
}
