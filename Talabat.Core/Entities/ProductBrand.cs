using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        //public ICollection<Prouduct> Prouducts { get; set; } = new HashSet<Prouduct>();
    }
}
