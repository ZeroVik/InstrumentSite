using System.ComponentModel.DataAnnotations;

namespace InstrumentSite.Dtos.Order
{
    public class OrderDetailDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }

}
