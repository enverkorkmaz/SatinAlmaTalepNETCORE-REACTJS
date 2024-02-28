using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SatinAlmaTalep.Entity.DTOs.Product;
using SatinAlmaTalep.Entity.Entities;
using SatinAlmaTalep.Service.Services.Abstraction;

namespace SatinAlmaTalep.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await productService.GetAllProductsAsync();
            var jsonResponse = JsonConvert.SerializeObject(response);
            return Ok(jsonResponse);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var response = await productService.GetProductByIdAsync(productId);
            var jsonResponse = JsonConvert.SerializeObject(response);
            return Ok(jsonResponse);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is invalid.");
            }

            try
            {
                Product newProduct = new()
                {
                    Price = productDto.Price,
                    Name = productDto.Name
                };
                var createdProduct = await productService.AddProductAsync(newProduct);
                return Ok(createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the product: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto productUpdateDto)
        {
            if (productUpdateDto == null)
            {
                return BadRequest("Product data is invalid.");
            }

            try
            {
                var existingProduct = await productService.GetProductByIdAsync(productUpdateDto.Id);
                if (existingProduct == null)
                {
                    return NotFound("Product not found.");
                }

                existingProduct.Name = productUpdateDto.Name;
                existingProduct.Price = productUpdateDto.Price;

                var updatedProduct = await productService.UpdateProductAsync(existingProduct);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the product: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> SafeDeleteProduct([FromBody] ProductSafeDeleteDto productSafeDeleteDto)
        {
            if (productSafeDeleteDto == null)
            {
                return BadRequest("Product data is invalid.");
            }
            try
            {
                var existingProduct = await productService.GetProductByIdAsync(productSafeDeleteDto.Id);
                if (existingProduct == null)
                {
                    return NotFound("Product not found.");
                }
                existingProduct.isDeleted = productSafeDeleteDto.isDeleted;

                var updatedProduct = await productService.UpdateProductAsync(existingProduct);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred while updating the product: {ex.Message}");
            }
        }
    }
}
