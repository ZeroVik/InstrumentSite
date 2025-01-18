using InstrumentSite.Dtos.Category;

namespace InstrumentSite.Repositories.Category
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int id);
        Task<int> AddCategoryAsync(CreateCategoryDTO categoryDto);
        Task<bool> UpdateCategoryAsync(UpdateCategoryDTO categoryDto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
