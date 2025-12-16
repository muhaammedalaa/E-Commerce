using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class CustomerBasket
    {

        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
        public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
        public decimal? ShippingPrice { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

        public CustomerBasket(string id)
        {
            Id = id;
            Items = new List<BasketItem>();
        }

        public void AddItem(BasketItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem is null)
            {
                Items.Add(item);
            }
            else
            {
                existingItem.Quantity += item.Quantity;
            }
        }
    }
}
