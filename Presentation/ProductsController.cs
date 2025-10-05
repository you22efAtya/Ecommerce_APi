using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationsParameters parameters)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(parameters);
            return Ok(products);

        }
        [HttpGet("Brands")]
        public async Task<ActionResult<ProductResultDto>> GetAllBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<ProductResultDto>> GetAllTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}
