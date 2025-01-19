using InstrumentSite.Data;
using InstrumentSite.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentSite.Repositories
{
    public class CartRepository
    {
        private readonly AppDbContext _dbContext;

        public CartRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            await _dbContext.Carts.AddAsync(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            await _dbContext.CartItems.AddAsync(cartItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _dbContext.CartItems.Update(cartItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            var cartItem = await _dbContext.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _dbContext.CartItems.Remove(cartItem);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(int cartId)
        {
            var cartItems = await _dbContext.CartItems.Where(ci => ci.CartId == cartId).ToListAsync();
            if (cartItems.Any())
            {
                _dbContext.CartItems.RemoveRange(cartItems);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
