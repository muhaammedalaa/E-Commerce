using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Dtos.Orders
{
    public class DeliveryMethodResult
    {
        public int Id { get; init; }
        public string ShortName { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public string DeliveryTime { get; init; } = string.Empty;
    }
}
