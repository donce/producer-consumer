using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerCustomer
{
    interface ITakeBuffer
    {
        int Take();
        int Count();
    }
}
