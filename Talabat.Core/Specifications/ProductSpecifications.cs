using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product, int>
    {
        public ProductSpecifications(int id) : base(P => P.Id == id)
        {
            AddIncluds();
        }
        public ProductSpecifications(string? sort, int? brandId, int? typeId, int? pageSize, int? pageIndex) : base(P =>
            (!brandId.HasValue || P.BrandId == brandId) &&
            (!typeId.HasValue || P.CategoryId == typeId)
            )
        {
            AddIncluds();
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {

                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
               
                if (pageSize.HasValue && pageIndex.HasValue)
                {
                    ApplyPaging((pageIndex.Value - 1) * pageSize.Value, pageSize.Value);
                }
            }

        }
        private void AddIncluds()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Catgory);
        }

    }
}
