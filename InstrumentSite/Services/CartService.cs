using InstrumentSite.Dtos.Cart;
using InstrumentSite.Models;
using InstrumentSite.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentSite.Services
{
    public class CartService
    {
        private readonly CartRepository _cartRepository;
        private readonly ProductRepository _productRepository;

        public CartService(CartRepository cartRepository, ProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<CartDTO> GetCartByUserIdAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return new CartDTO
                {
                    UserId = userId,
                    Items = new List<CartItemDTO>(),
                    TotalPrice = 0
                };
            }

            var cartDto = new CartDTO
            {
                CartId = cart.Id,
                UserId = cart.UserId,
                Items = cart.CartItems.Select(item => new CartItemDTO
                {
                    CartItemId = item.Id,
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    UnitPrice = item.Price,
                    Quantity = item.Quantity
                }).ToList(),
                TotalPrice = cart.CartItems.Sum(item => item.Price * item.Quantity)
            };

            return cartDto;
        }

        public async Task AddToCartAsync(int userId, int productId, int quantity, decimal price)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                // Assign the created cart since CreateCartAsync returns a cart
                cart = await _cartRepository.CreateCartAsync(cart);
            }

            var existingItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                await _cartRepository.UpdateCartItemAsync(existingItem); // Call without assignment
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = price
                };
                await _cartRepository.AddCartItemAsync(cartItem); // Call without assignment
            }
        }

        public async Task<bool> CheckProductExistsAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            return product != null;
        }

        public async Task<bool> RemoveFromCartAsync(int cartItemId)
        {
            await _cartRepository.RemoveCartItemAsync(cartItemId);
            return true;
        }

        public async Task ClearCartAsync(int cartId)
        {
            await _cartRepository.ClearCartAsync(cartId);
        }
    }
}
