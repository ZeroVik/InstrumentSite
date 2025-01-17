using System.ComponentModel.DataAnnotations;

namespace InstrumentSite.Dtos.Category
{
    public class CreateCategoryDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }

}
