using InstrumentSite.Data;
using InstrumentSite.Models;
using Microsoft.EntityFrameworkCore;

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
        await ValidateCategoryExists(product.CategoryId);
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return product.Id;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
    {
        return await _dbContext.Products
            .Include(p => p.Category)
            .Where(p => p.Category.Name == category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByTypeAsync(bool isSecondHand)
    {
        return await _dbContext.Products
            .Include(p => p.Category)
            .Where(p => p.IsSecondHand == isSecondHand)
            .ToListAsync();
    }


    public async Task<bool> UpdateProductAsync(Product product)
    {
        await ValidateCategoryExists(product.CategoryId);

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

        _dbContext.Products.Update(existingProduct);
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

    private async Task ValidateCategoryExists(int categoryId)
    {
        if (!await _dbContext.Categories.AnyAsync(c => c.Id == categoryId))
        {
            throw new ArgumentException($"Category with ID {categoryId} does not exist.");
        }
    }
}
