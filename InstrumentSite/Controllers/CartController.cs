using InstrumentSite.Dtos.Cart;
using InstrumentSite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InstrumentSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        // Get cart by userId
        [HttpGet("{userId}")]
        public async Task<ActionResult<CartDTO>> GetCart(int userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null || cart.Items.Count == 0)
            {
                return NotFound(new { message = $"Cart for user with ID {userId} is empty or does not exist." });
            }

            return Ok(cart);
        }

        // Add an item to the cart
        [HttpPost("AddItem")]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDTO cartItemDto)
        {
            if (cartItemDto.ProductId <= 0)
            {
                return BadRequest(new { message = "Invalid Product ID." });
            }

            var productExists = await _cartService.CheckProductExistsAsync(cartItemDto.ProductId);
            if (!productExists)
            {
                return NotFound(new { message = $"Product with ID {cartItemDto.ProductId} not found." });
            }

            await _cartService.AddToCartAsync(cartItemDto.UserId, cartItemDto.ProductId, cartItemDto.Quantity, cartItemDto.UnitPrice);
            return Ok(new { message = "Item added to cart successfully." });
        }


        // Remove an item from the cart
        [HttpDelete("RemoveItem/{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var result = await _cartService.RemoveFromCartAsync(cartItemId);
            if (!result)
            {
                return NotFound(new { message = $"Cart item with ID {cartItemId} not found." });
            }

            return Ok(new { message = "Item removed from cart successfully." });
        }

        // Clear the cart for a specific user
        [HttpDelete("ClearCart/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null || cart.Items.Count == 0)
            {
                return NotFound(new { message = $"Cart for user with ID {userId} is empty or does not exist." });
            }

            await _cartService.ClearCartAsync(cart.CartId);
            return Ok(new { message = "Cart cleared successfully." });
        }
    }
}
