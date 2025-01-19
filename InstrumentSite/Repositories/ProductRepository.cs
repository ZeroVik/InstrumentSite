using InstrumentSite.Data;
using InstrumentSite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentSite.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> AddProductAsync(Product product)
        {
            if (!await CategoryExists(product.CategoryId))
            {
                throw new ArgumentException($"Category with ID {product.CategoryId} does not exist.");
            }

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product.Id;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            if (!await CategoryExists(product.CategoryId))
            {
                throw new ArgumentException($"Category with ID {product.CategoryId} does not exist.");
            }

            var existingProduct = await _dbContext.Products.FindAsync(product.Id);
            if (existingProduct == null) return false;

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                existingProduct.ImageUrl = product.ImageUrl;
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null) return false;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ProductExists(int id)
        {
            return await _dbContext.Products.AnyAsync(p => p.Id == id);
        }

        private async Task<bool> CategoryExists(int categoryId)
        {
            return await _dbContext.Categories.AnyAsync(c => c.Id == categoryId);
        }
    }
}
