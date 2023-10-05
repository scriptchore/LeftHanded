using System.Reflection;
using CORE.Entities;
using Microsoft.EntityFrameworkCore;

namespace INFRASTRUCTURE.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
         public DbSet<ProductType> ProductTypes { get; set; }
          public DbSet<ProductBrand> ProductBrands { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}