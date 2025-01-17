using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InstrumentSite.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; } // Primary Key

        // Foreign Key for User
        public int UserId { get; set; }
        public User User { get; set; } // Navigation Property for User

        public decimal TotalAmount { get; set; } // Total amount for the order

        public DateTime OrderDate { get; set; } = DateTime.UtcNow; // Timestamp for when the order was placed

        public string Status { get; set; } // Status of the order (e.g., "Pending", "Shipped", "Delivered", "Canceled")

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>(); // Navigation Property for Order Items
    }
}
