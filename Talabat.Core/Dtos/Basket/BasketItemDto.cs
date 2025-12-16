using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Dtos.Basket
{
    public class BasketItemDto
    {
        public int Id { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public string PictureUrl { get; init; } = string.Empty;
        [Range(1, double.MaxValue)]
        public decimal Price { get; init; }
        [Range(1, 99)]
        public int Quantity { get; init; }
        public string Brand { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
        public decimal TotalPrice => Price * Quantity;
    }
}
