using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Orders
{
    public class OrderItem : BaseEntity<Guid>
    {
        public ProductInOrderItem ItemOrdered { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public OrderItem()
        {
        }
        public OrderItem(ProductInOrderItem itemOrdered, decimal price, int quantity)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }
    }
}
