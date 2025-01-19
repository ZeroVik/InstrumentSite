using InstrumentSite.Data;
using InstrumentSite.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstrumentSite.Repositories
{
    public class CategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<InstrumentSite.Models.Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<InstrumentSite.Models.Category> GetCategoryByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<int> AddCategoryAsync(InstrumentSite.Models.Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category.Id;
        }

        public async Task<bool> UpdateCategoryAsync(InstrumentSite.Models.Category category)
        {
            var existingCategory = await _dbContext.Categories.FindAsync(category.Id);
            if (existingCategory == null) return false;

            existingCategory.Name = category.Name;
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
