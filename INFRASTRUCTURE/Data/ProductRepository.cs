using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Interfaces;
using INFRASTRUCTURE.Identity;
using Microsoft.EntityFrameworkCore;

namespace INFRASTRUCTURE.Data
{
    public class ProductRepository : IProductRepository
    {
        public AppIdentityDbContext _Context { get; }

        public ProductRepository(AppIdentityDbContext context)
        {
            _Context = context;
            
        }
        public async Task<Products> GetProductByIdAsync(int Id)
        {
            return await _Context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<IReadOnlyList<Products>> GetProductsAsync()
        {
           return await _Context.Products
           .Include(p => p.ProductType)
           .Include(p => p.ProductBrand)
           .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
                return await _Context.ProductBrands.ToListAsync();               
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypsAsync()
        {
            return await _Context.ProductTypes.ToListAsync();     
        }
    }
}