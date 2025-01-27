using InstrumentSite.Data;
using InstrumentSite.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentSite.Repositories
{
    public class OrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order.Id;
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await _dbContext.Carts.Include(c => c.CartItems)
                                              .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                _dbContext.CartItems.RemoveRange(cart.CartItems);
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null) return false;

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
