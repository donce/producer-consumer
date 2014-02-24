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
            New,
            Ready,
            Working,
            Complete,
        };

        public State state = State.Ready;

        public string stateTitle
        {
            get
            {
                if (state == State.Ready)
                    return "Ready";
                if (state == State.Working)
                    return "Working";
                if (state == State.Complete)
                    return "Complete";
                return "";
            }
        }

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
