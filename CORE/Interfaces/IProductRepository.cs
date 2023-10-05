using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE.Entities;

namespace CORE.Interfaces
{
    public interface IProductRepository
    {
        Task<Products> GetProductByIdAsync(int Id);
        Task<IReadOnlyList<Products>> GetProductsAsync();

        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypsAsync();
    }
}