using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Dtos.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int? BrandId { get; set; } // Forign Key
        public string BrandName { get; set; } = null!;
        public int? CategoryId { get; set; }
        public string CatgoryName { get; set; } = null!;
    }
}
