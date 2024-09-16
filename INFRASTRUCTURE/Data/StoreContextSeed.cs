using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using CORE.Entities;
using CORE.Entities.OrderAggregate;


namespace INFRASTRUCTURE.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if(!context.ProductBrands.Any())
            {
                var brandData = File.ReadAllText("../INFRASTRUCTURE/Data/Seeddata/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                context.ProductBrands.AddRange(brands);
            }

            if(!context.ProductTypes.Any())
            {
                var typeData = File.ReadAllText("../INFRASTRUCTURE/Data/Seeddata/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                context.ProductTypes.AddRange(types);
            }

            if(!context.Products.Any())
            {
                var productData = File.ReadAllText("../INFRASTRUCTURE/Data/Seeddata/products.json");
                var products = JsonSerializer.Deserialize<List<Products>>(productData);
                context.Products.AddRange(products);
            }

            if(!context.DeliveryMethods.Any())
            {
                var deliveryData = File.ReadAllText("../INFRASTRUCTURE/Data/Seeddata/delivery.json");
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                context.DeliveryMethods.AddRange(methods);
            }

            if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();


        }
    }
}