﻿namespace InstrumentSite.Dtos.Cart
{
    public class CartDTO
    {
        
            public int CartId { get; set; }
            public int UserId { get; set; }
            public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();
            public decimal TotalPrice { get; set; }
            public decimal DiscountAmount { get; set; }  
            public decimal GrandTotal { get; set; }      
            public string DiscountMessage { get; set; }  


    }
}
