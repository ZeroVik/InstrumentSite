namespace InstrumentSite.Dtos.Cart
{
    public class CartItemDTO
    {
        public int CartItemId { get; set; }
        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal => UnitPrice * Quantity;
    }

}
