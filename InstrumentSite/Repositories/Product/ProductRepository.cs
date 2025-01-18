using InstrumentSite.Data;
using InstrumentSite.Dtos.Product;
using InstrumentSite.Models;
using InstrumentSite.Repositories.Product;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddProductAsync(CreateProductDTO productDto)
    {
        var categoryExists = await _dbContext.Categories.AnyAsync(c => c.Id == productDto.CategoryId);
        if (!categoryExists)
        {
            throw new ArgumentException($"Category with ID {productDto.CategoryId} does not exist.");
        }

        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            CategoryId = productDto.CategoryId,
            ImageUrl = productDto.ImageUrl
        };

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();

        return product.Id;
    }

    public async Task<bool> UpdateProductAsync(UpdateProductDTO productDto)
    {
        var categoryExists = await _dbContext.Categories.AnyAsync(c => c.Id == productDto.CategoryId);
        if (!categoryExists)
        {
            throw new ArgumentException($"Category with ID {productDto.CategoryId} does not exist.");
        }

        var product = await _dbContext.Products.FindAsync(productDto.Id);
        if (product == null) return false;

        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;
        product.CategoryId = productDto.CategoryId;
        product.ImageUrl = productDto.ImageUrl;

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

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        return await _dbContext.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category.Name,
                ImageUrl = p.ImageUrl
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
    {
        return await _dbContext.Products
            .Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category.Name,
                ImageUrl = p.ImageUrl
            })
            .ToListAsync();
    }
}
