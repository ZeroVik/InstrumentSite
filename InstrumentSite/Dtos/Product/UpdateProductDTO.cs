using System.ComponentModel.DataAnnotations;

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

    public string? ImageUrl { get; set; } // Optional if no new image is provided
    public IFormFile? ImageFile { get; set; } // Optional if no new image is uploaded
}
