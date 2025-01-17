using System.ComponentModel.DataAnnotations;

namespace InstrumentSite.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; } // Primary Key

        // Foreign Key for Order
        public int OrderId { get; set; }
        public Order Order { get; set; } // Navigation Property for Order

        // Foreign Key for Product
        public int ProductId { get; set; }
        public Product Product { get; set; } // Navigation Property for Product

        public int Quantity { get; set; } // Quantity of the product ordered

        public decimal UnitPrice { get; set; } // Price of the product at the time of order
    }
}
