
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using CORE.Entities;
using CORE.Interfaces;
using CORE.Specifications;
using INFRASTRUCTURE.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    
    public class ProductsController : BaseApiController 
    {

        
        private readonly IGenericRepository<Products> _productsRepo;
        public IGenericRepository<ProductType> _productsTypeRepo { get; }
        public IGenericRepository<ProductBrand> _productsBrandRepo { get; }
        private readonly IMapper _mapper;
      
        public ProductsController(IGenericRepository<Products> productsRepo,
         IGenericRepository<ProductType> productsTypeRepo, IGenericRepository<ProductBrand> productsBrandRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productsBrandRepo = productsBrandRepo;
            _productsTypeRepo = productsTypeRepo;
            _productsRepo = productsRepo;
           
           
        }


        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery]ProductSpecparams productParams)
        {
            var spec = new ProductsWithTypesandBrandsSpec(productParams);

            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);

            var product = await _productsRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Products>, IReadOnlyList<ProductDto>>(product);

            return Ok(new Pagination<ProductDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [Cached(600)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>>  GetProduct(int id)
        {
             var spec = new ProductsWithTypesandBrandsSpec(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Products, ProductDto>(product);
        }

        [Cached(600)]
         [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>>  GetProductBrands()
        {
            return Ok(await _productsBrandRepo.ListAllAsync());
        }

        [Cached(600)]
        [HttpGet("types")]
        public async Task<ActionResult<ProductType>>  GetProductTypes()
        {
            return Ok(await _productsTypeRepo.ListAllAsync());
        }
    }
}