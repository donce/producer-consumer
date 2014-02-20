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

        public enum State
        {
            Ready,
            Working,
            Complete,
        };

        public State state = State.Ready;

        public String name
        {
            get { return _name; }
        }

        protected Worker(String name)
        {
            _name = name;
        }

        protected abstract void Run();

        public void Start()
        {
            state = State.Working;
            Run();
            state = State.Complete;
        }
    }
}
