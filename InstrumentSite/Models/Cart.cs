using System.ComponentModel.DataAnnotations;

namespace InstrumentSite.Models
{
    public class Cart
    {
            [Key]
            public int Id { get; set; }
            public int UserId { get; set; } // Link the cart to a specific user
            public ICollection<CartItem> CartItems { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        

    }
}
