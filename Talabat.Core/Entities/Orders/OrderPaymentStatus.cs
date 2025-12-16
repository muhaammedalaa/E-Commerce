using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Orders
{
    public enum OrderPaymentStatus
    {
        Pending=0,
        PaymentRecieved = 1,
        PaymentFailed = 2,
    }
}
