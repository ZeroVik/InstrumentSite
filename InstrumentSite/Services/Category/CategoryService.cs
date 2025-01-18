using InstrumentSite.Dtos.Category;
using InstrumentSite.Repositories.Category;

namespace InstrumentSite.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<int> AddCategoryAsync(CreateCategoryDTO categoryDto)
        {
            // Add business rules here (e.g., check if category already exists)
            return await _categoryRepository.AddCategoryAsync(categoryDto);
        }

        public async Task<bool> UpdateCategoryAsync(UpdateCategoryDTO categoryDto)
        {
            // Add business logic for updates here
            return await _categoryRepository.UpdateCategoryAsync(categoryDto);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            // Add additional checks before deletion
            return await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
