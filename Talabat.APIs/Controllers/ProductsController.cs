using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Attrbiutes;
using Talabat.Core.Dtos.Products;
using Talabat.Core.Service.Contract;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Cache(100)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts([FromQuery] string? sort, [FromQuery] int? brandId, [FromQuery] int? typeId, [FromQuery] int? pageSize, [FromQuery]int? pageIndex)
        {
            var products = await _productService.GetAllProductsAsync(sort,brandId,typeId, pageSize, pageIndex);
            return Ok(products);
        }
        [HttpGet("Categories")]
        public async Task<ActionResult<IEnumerable<CategoryBrandDto>>> GetAllCategories()
        {
            var Category = await _productService.GetAllCategoriesAsync();
            return Ok(Category);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<CategoryBrandDto>>> GetAllBrands()
        {
            var Brand = await _productService.GetAllBrandsAsync();
            return Ok(Brand);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int? id)
        {
            if (id == null) return BadRequest("Invalid Id");
            var product = await _productService.GetProductById(id.Value);
            if (product == null) return NotFound(new { Message = "Not Found",StatusCode =404});
            return Ok(product);
        }

    }
}
