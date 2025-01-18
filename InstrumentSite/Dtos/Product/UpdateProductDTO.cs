﻿using System.ComponentModel.DataAnnotations;

namespace InstrumentSite.Dtos.Product
{
    public class UpdateProductDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(500)] // Optional: Adjust length as needed
        public string ImageUrl { get; set; }
    }

}
