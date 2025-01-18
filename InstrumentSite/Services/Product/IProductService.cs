using InstrumentSite.Dtos.Product;

namespace InstrumentSite.Services.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<int> AddProductAsync(CreateProductDTO productDto);
        Task<bool> UpdateProductAsync(UpdateProductDTO productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
