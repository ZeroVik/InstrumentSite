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

        [HttpGet("{userId}")]
        public async Task<ActionResult<CartDTO>> GetCart(string userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddToCart(string userId, int productId, int quantity, decimal price)
        {
            var product = await _cartService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {productId} not found." });
            }

            await _cartService.AddToCartAsync(userId, productId, quantity, price);
            return Ok(new { message = "Item added to cart successfully." });
        }

        [HttpDelete("RemoveItem/{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            await _cartService.RemoveFromCartAsync(cartItemId);
            return Ok(new { message = "Item removed from cart successfully." });
        }

        [HttpDelete("ClearCart/{userId}")]
        public async Task<IActionResult> ClearCart(string userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart != null)
            {
                await _cartService.ClearCartAsync(cart.CartId);
            }

            return Ok(new { message = "Cart cleared successfully." });
        }
    }
}
