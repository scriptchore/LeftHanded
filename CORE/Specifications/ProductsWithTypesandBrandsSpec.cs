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
        public ProductsWithTypesandBrandsSpec(string sort)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch(sort)
                {
                    case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                     case "priceDesc":
                    AddOrderByDesc(p => p.Price);
                    break;
                    default:
                    AddOrderBy(n => n.Name);
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