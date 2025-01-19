namespace InstrumentSite.Dtos.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsSecondHand { get; set; }
    }

}
