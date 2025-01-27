using InstrumentSite.Dtos.Product;
using InstrumentSite.Services;
using InstrumentSite.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstrumentSite.Controllers
{
    [ApiController]
    [Route("api/products")] // Base path for products
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(Name = "GetAllProducts")] // GET /api/products
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            foreach (var product in products)
            {
                // Assuming 'ImageUrl' currently holds relative paths like "uploads/Guitar.jpg"
                product.ImageUrl = $"{baseUrl}/{product.ImageUrl}"; // Convert to absolute URL
            }

            return Ok(products);
        }



        [HttpGet("details/{id}", Name = "GetProductDetails")] // GET /api/products/details/{id}
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("filter", Name = "FilterProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByType(bool isSecondHand)
        {
            var products = await _productService.GetProductsByTypeAsync(isSecondHand);
            return Ok(products);
        }

        [HttpGet("related", Name = "GetRelatedProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategory([FromQuery] string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest(new { message = "Category parameter is required." });
            }

            var products = await _productService.GetProductsByCategoryAsync(category);

            if (products == null || !products.Any())
            {
                return NotFound(new { message = $"No products found for category '{category}'." });
            }

            return Ok(products);
        }



        [HttpPost("create", Name = "CreateProduct")]
        public async Task<ActionResult<ProductDTO>> AddProduct([FromForm] CreateProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var isAdmin = User.IsInRole("Admin");
                var productId = await _productService.AddProductAsync(productDto, !isAdmin, Request);

                // Fetch the created product by ID to include all its details
                var createdProduct = await _productService.GetProductByIdAsync(productId);

                return CreatedAtAction(nameof(GetProductById), new { id = productId }, createdProduct);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }




        [HttpPut("update/{id}", Name = "UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductDTO productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest(new { message = "Product ID in the URL does not match the ID in the body." });
            }

            try
            {
                var updated = await _productService.UpdateProductAsync(productDto, Request);
                if (!updated)
                {
                    return NotFound(new { message = $"Product with ID {id} not found." });
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpDelete("remove/{id}", Name = "DeleteProduct")] // DELETE /api/products/remove/{id}
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
