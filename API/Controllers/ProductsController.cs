
using System.Collections.Generic;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Interfaces;
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
      
        public ProductsController(IGenericRepository<Products> productsRepo,
         IGenericRepository<ProductType> productsTypeRepo, IGenericRepository<ProductBrand> productsBrandRepo)
        {
            _productsBrandRepo = productsBrandRepo;
            _productsTypeRepo = productsTypeRepo;
            _productsRepo = productsRepo;
           
           
        }

        [HttpGet]
        public async Task<ActionResult<List<Products>>> GetProducts()
        {
            var products = await _productsRepo.ListAllAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>>  GetProduct(int id)
        {
            return await _productsRepo.GetByIdAsync(id);
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