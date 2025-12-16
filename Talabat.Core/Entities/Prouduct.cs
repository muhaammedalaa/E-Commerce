using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int? BrandId { get; set; } // Forign Key
        public ProductBrand Brand { get; set; } = null!;
        public int? CategoryId { get; set; }
        public ProductCatgory Catgory { get; set; } = null!;

    }
}
