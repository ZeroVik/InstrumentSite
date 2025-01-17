using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace InstrumentSite.Models
{
    public class CartItem
    {
            [Key]
            public int Id { get; set; }
            public int CartId { get; set; }
            public Cart Cart { get; set; }
            public int ProductId { get; set; } // Foreign key to the Instrument
            public Product Product { get; set; }
            public int Quantity { get; set; } = 1;
            public decimal Price { get; set; } // Price at the time of adding to cart
        

    }
}
