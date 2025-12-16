using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Dtos.Orders
{
    public class OrderRequest
    {
        public string BasketId { get; init; } = string.Empty;
        public AddressDto ShippingAddress { get; init; }
        public int DeliveryMethodId { get; init; }
    }
}
