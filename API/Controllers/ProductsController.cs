
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using CORE.Entities;
using CORE.Interfaces;
using CORE.Specifications;
using INFRASTRUCTURE.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase 
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

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesandBrandsSpec();

            var product = await _productsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Products>, IReadOnlyList<ProductDto>>(product));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>>  GetProduct(int id)
        {
             var spec = new ProductsWithTypesandBrandsSpec(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Products, ProductDto>(product);
        }
        
         [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>>  GetProductBrands()
        {
            return Ok(await _productsBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<ProductType>>  GetProductTypes()
        {
            return Ok(await _productsTypeRepo.ListAllAsync());
        }
    }
}