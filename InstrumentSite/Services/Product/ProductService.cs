using InstrumentSite.Dtos.Product;
using InstrumentSite.Repositories.Product;

namespace InstrumentSite.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            // Additional business logic can be added here if needed
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            return product;
        }

        public async Task<int> AddProductAsync(CreateProductDTO productDto)
        {
            // Add business rules, such as validation
            if (string.IsNullOrWhiteSpace(productDto.Name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            return await _productRepository.AddProductAsync(productDto);
        }

        public async Task<bool> UpdateProductAsync(UpdateProductDTO productDto)
        {
            // Add validation or other business logic before updating
            if (productDto.Price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.");
            }

            return await _productRepository.UpdateProductAsync(productDto);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var deleted = await _productRepository.DeleteProductAsync(id);
            if (!deleted)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            return deleted;
        }
    }
}
