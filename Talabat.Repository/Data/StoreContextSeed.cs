using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Orders;
using Talabat.Repository.Data.Contexts;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbContext)
        {
            if (_dbContext.productBrands.Count() == 0)
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count() > 0)
                {
                    // _dbContext.productBrands.AddRange(brands); is Best
                    foreach (var brand in brands)
                    {
                        _dbContext.productBrands.Add(brand);

                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (_dbContext.categories.Count() == 0)
            {
                var categoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCatgory>>(categoryData);
                if (categories?.Count() > 0)
                {
                    foreach (var category in categories)
                    {
                        _dbContext.categories.Add(category);

                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (_dbContext.products.Count() == 0)
            {
                var ProductsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (products?.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        _dbContext.products.Add(product);

                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            if (_dbContext.DeliveryMethods.Count() == 0)
            {
                var DeliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var deliveryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                if (deliveryMethod?.Count() > 0)
                {
                    foreach (var delivery in deliveryMethod)
                    {
                        _dbContext.DeliveryMethods.Add(delivery);

                    }
                    await _dbContext.SaveChangesAsync();
                }
            }


        }
    }
}
