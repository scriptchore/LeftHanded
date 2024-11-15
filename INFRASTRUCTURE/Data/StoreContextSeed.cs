using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using CORE.Entities;
using CORE.Entities.OrderAggregate;
using INFRASTRUCTURE.Identity;
using Microsoft.EntityFrameworkCore;


namespace INFRASTRUCTURE.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(AppIdentityDbContext context)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!context.ProductBrands.Any())
            {
                var brandData = File.ReadAllText(path + @"/Data/Seeddata/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                //await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductBrands ON");
                context.ProductBrands.AddRange(brands);
                //await context.SaveChangesAsync();
               // await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductBrands OFF");
            }

            if (!context.ProductTypes.Any())
            {
                var typeData = File.ReadAllText(path + @"/Data/Seeddata/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

                //await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductTypes ON");
               // context.ProductTypes.AddRange(types);
                //await context.SaveChangesAsync();
                //await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductTypes OFF");
            }

            if (!context.Products.Any())
            {
                var productData = File.ReadAllText(path + @"/Data/Seeddata/products.json");
                var products = JsonSerializer.Deserialize<List<Products>>(productData);
                context.Products.AddRange(products);
            }

            if(!context.DeliveryMethods.Any())
            {
                var deliveryData = File.ReadAllText(path + @"/Data/Seeddata/delivery.json");
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                context.DeliveryMethods.AddRange(methods);
            }

            if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();


        }
    }
}