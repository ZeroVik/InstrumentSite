using InstrumentSite.Dtos.Cart;
using InstrumentSite.Models;
using InstrumentSite.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstrumentSite.Enums;

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

            var cartDto = new CartDTO
            {
                UserId = userId,
                Items = new List<CartItemDTO>(),
                TotalPrice = 0,
                DiscountAmount = 0,
                GrandTotal = 0,
                DiscountMessage = null
            };

            if (cart == null) return cartDto;

            cartDto.CartId = cart.Id;
            cartDto.Items = cart.CartItems.Select(item => new CartItemDTO
            {
                CartItemId = item.Id,
                ProductId = item.ProductId,
                ProductName = item.Product.Name,
                UnitPrice = item.Price,
                Quantity = item.Quantity,
                // Subtotal is calculated automatically by the DTO
                UserId = item.Cart.UserId
            }).ToList();

            // Calculate total price using the DTO's Subtotal property
            cartDto.TotalPrice = cartDto.Items.Sum(item => item.Subtotal);

            // Apply discount rules
            if (cartDto.TotalPrice >= 500)
            {
                cartDto.DiscountAmount = cartDto.TotalPrice * 0.10m;
                cartDto.DiscountMessage = "10% discount on orders over $500!";
            }
            else if (cartDto.TotalPrice >= 200)
            {
                cartDto.DiscountAmount = cartDto.TotalPrice * 0.05m;
                cartDto.DiscountMessage = "5% discount on orders over $200!";
            }

            cartDto.GrandTotal = cartDto.TotalPrice - cartDto.DiscountAmount;

            return cartDto;
        }

        public async Task<CartOperationResultEnum> UpdateCartItemQuantityAsync(
        int cartItemId,
        int quantity,
        int userId)
        {
            // Validation
            if (quantity < 1 || quantity > 100)
                return CartOperationResultEnum.InvalidQuantity;

            // Get cart item with ownership check
            var cartItem = await _cartRepository.GetCartItemWithDetailsAsync(cartItemId);
            if (cartItem?.Cart?.UserId != userId)
                return CartOperationResultEnum.Unauthorized;

            // Check product stock
            var product = await _productRepository.GetProductByIdAsync(cartItem.ProductId);
            

            // Update and save
            cartItem.Quantity = quantity;
            await _cartRepository.UpdateCartItemAsync(cartItem);

            return CartOperationResultEnum.Success;
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
