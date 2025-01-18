using InstrumentSite.Data;
using InstrumentSite.Dtos.Category;
using InstrumentSite.Models;
using Microsoft.EntityFrameworkCore;

namespace InstrumentSite.Repositories.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories
                .Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null) return null;

            return new CategoryDTO { Id = category.Id, Name = category.Name };
        }

        public async Task<int> AddCategoryAsync(CreateCategoryDTO categoryDto)
        {
            var category = new InstrumentSite.Models.Category { Name = categoryDto.Name };
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category.Id;
        }

        public async Task<bool> UpdateCategoryAsync(UpdateCategoryDTO categoryDto)
        {
            var category = await _dbContext.Categories.FindAsync(categoryDto.Id);
            if (category == null) return false;

            category.Name = categoryDto.Name;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null) return false;

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
