namespace InstrumentSite.Dtos.Cart
{
    public class AddToCartDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
