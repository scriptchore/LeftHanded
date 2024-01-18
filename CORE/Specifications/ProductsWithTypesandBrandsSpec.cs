using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CORE.Entities;

namespace CORE.Specifications
{
    public class ProductsWithTypesandBrandsSpec : BaseSpecification<Products>
    {
        public ProductsWithTypesandBrandsSpec(ProductSpecparams productParams) : 
        base(x => 
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                    break;
                     case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                    break;
                    default:
                        AddOrderBy(p => p.Name);
                    break;
                }

            }
        }

        public ProductsWithTypesandBrandsSpec(int Id) : base(x => x.Id == Id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}