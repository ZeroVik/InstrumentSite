using InstrumentSite.Dtos.Order;
using InstrumentSite.Models;
using InstrumentSite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace InstrumentSite.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return orders.Select(order => new OrderDTO
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                Status = order.Status
            });
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            return new OrderDTO
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                Status = order.Status
            };
        }

        public async Task<int> CreateOrderAsync(CreateOrderDTO createOrderDto)
        {
            // Create the Order entity
            var order = new Order
            {
                UserId = createOrderDto.UserId,
                TotalAmount = createOrderDto.TotalAmount,
                Address = JsonSerializer.Serialize(createOrderDto.Address), // Convert object to JSON string
                Status = "Pending",
                OrderDetails = new List<OrderDetail>() // Initialize OrderDetails as an empty list
            };

            // Add each OrderDetail individually
            foreach (var od in createOrderDto.OrderDetails)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice
                };
                order.OrderDetails.Add(orderDetail);
            }

            // Save the order using the repository
            var orderId = await _orderRepository.CreateOrderAsync(order);
            await _orderRepository.ClearCartAsync(createOrderDto.UserId);
            return orderId;
        }


        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            await _orderRepository.UpdateOrderStatusAsync(orderId, status);
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            return await _orderRepository.DeleteOrderAsync(orderId);
        }
    }
}
