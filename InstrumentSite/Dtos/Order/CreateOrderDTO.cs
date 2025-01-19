using System.ComponentModel.DataAnnotations;

namespace InstrumentSite.Dtos.Order
{
    public class CreateOrderDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public List<OrderDetailDTO> OrderDetails { get; set; }

        public decimal TotalAmount { get; set; }

        [Required]
        public object Address { get; set; }
    }


}
