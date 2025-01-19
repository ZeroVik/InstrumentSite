using InstrumentSite.Dtos.Cart;
using InstrumentSite.Models;
using InstrumentSite.Repositories;
using System;
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

        public async Task<CartDTO> GetCartByUserIdAsync(string userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return new CartDTO { UserId = userId, Items = new List<CartItemDTO>(), TotalPrice = 0 };
            }

            var cartDto = new CartDTO
            {
                CartId = cart.Id,
                UserId = cart.UserId,
                Items = cart.CartItems.Select(item => new CartItemDTO
                {
                    CartItemId = item.Id,
                    InstrumentId = item.ProductId,
                    InstrumentName = item.Product.Name,
                    UnitPrice = item.Price,
                    Quantity = item.Quantity
                }).ToList(),
                TotalPrice = cart.CartItems.Sum(item => item.Price * item.Quantity)
            };

            return cartDto;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }
            return product;
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity, decimal price)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId, CartItems = new List<CartItem>() };
                cart = await _cartRepository.CreateCartAsync(cart);
            }

            var existingItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                await _cartRepository.UpdateCartItemAsync(existingItem);
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
                await _cartRepository.AddCartItemAsync(cartItem);
            }
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            await _cartRepository.RemoveCartItemAsync(cartItemId);
        }

        public async Task ClearCartAsync(int cartId)
        {
            await _cartRepository.ClearCartAsync(cartId);
        }
    }
}
