using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Abstractions.Dto;
using OrderManagement.Application.Abstractions.Services;

namespace OrderManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto product)
        {
            await productService.CreateProduct(
                product.Name,
                product.Price);

            return Created();
        }

        [HttpPut("{id}/discount")]
        public async Task<IActionResult> ApplyDiscount(int id, [FromBody] CreateDiscountDto discount)
        {
            await productService.ApplyDiscount(
                id,
                discount.Percentage,
                discount.Quantity);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] string query = "")
        {
            var products = await productService.SearchProducts(query);

            return Ok(products);
        }

        [HttpGet("{id}/report")]
        public async Task<IActionResult> GetReport(int id)
        {
            var report = await productService.GetDiscountedProductReport(id);

            return Ok(report);
        }
    }
}
