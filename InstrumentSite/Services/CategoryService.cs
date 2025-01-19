using InstrumentSite.Dtos.Category;
using InstrumentSite.Models;
using InstrumentSite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentSite.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return categories.Select(category => new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            });
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<int> AddCategoryAsync(CreateCategoryDTO createCategoryDto)
        {
            var category = new InstrumentSite.Models.Category
            {
                Name = createCategoryDto.Name
            };

            return await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task<bool> UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDto)
        {
            var category = new InstrumentSite.Models.Category
            {
                Id = updateCategoryDto.Id,
                Name = updateCategoryDto.Name
            };

            return await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
