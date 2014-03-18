using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    class IntegerFactory : IElementFactory<int>
    {
        private static IntegerFactory _instance;

        public static IntegerFactory Instance()
        {
            if (_instance == null)
                _instance = new IntegerFactory();
            return _instance;
        }

        public int GetElement(int i)
        {
            return i;
        }
    }
}
