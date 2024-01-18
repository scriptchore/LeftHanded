using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE.Entities;

namespace CORE.Specifications
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Products>
    {
        public ProductsWithFiltersForCountSpecification(ProductSpecparams productParams) : 
        base(x => 
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        )
        {
            
        }
    }
}