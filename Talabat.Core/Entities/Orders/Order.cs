using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Orders
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
        }

        public Order(string userEmail, decimal subtotal, ICollection<OrderItem> orderItems, Address shipToAddress, DeliveryMethod deliveryMethod, string paymentIntentId)
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            Subtotal = subtotal;
            OrderItems = orderItems;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Address ShipToAddress { get; set; } = null!;
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
        public int? DeliveryMethodId { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
