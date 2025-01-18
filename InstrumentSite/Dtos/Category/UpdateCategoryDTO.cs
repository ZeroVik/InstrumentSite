using System.ComponentModel.DataAnnotations;

namespace InstrumentSite.Dtos.Category
{
    public class UpdateCategoryDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
