using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

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

        public string Status { get; set; } // Status of the order (e.g., "Pending", "Shipped",
                                           // "Delivered", "Canceled")

        [Column(TypeName = "jsonb")]
        public string Address { get; set; } // Address stored as JSONB

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>(); // Navigation Property for Order Items
    }
}
