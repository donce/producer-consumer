using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    interface IElementFactory<T>
    {
        T GetElement(int i);
    }
}
