using InstrumentSite.Dtos.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstrumentSite.Repositories.Product
{
    public interface IProductRepository
    {
        Task<int> AddProductAsync(CreateProductDTO productDto);
        Task<bool> UpdateProductAsync(UpdateProductDTO productDto);
        Task<bool> DeleteProductAsync(int id);
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
    }
}
