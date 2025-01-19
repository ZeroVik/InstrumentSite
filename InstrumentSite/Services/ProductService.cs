using InstrumentSite.Dtos.Product;
using InstrumentSite.Models;
using InstrumentSite.Repositories;
using InstrumentSite.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentSite.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return products.Select(product => new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryName = product.Category.Name,
                ImageUrl = product.ImageUrl
            });
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryName = product.Category.Name,
                ImageUrl = product.ImageUrl
            };
        }

        public async Task<int> AddProductAsync(CreateProductDTO productDto)
        {
            if (string.IsNullOrWhiteSpace(productDto.Name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            if (productDto.ImageFile == null)
            {
                throw new ArgumentException("Product image is required.");
            }

            // Save the image
            var imagePath = await SaveImageUtil.SaveImageFileAsync(productDto.ImageFile);

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                ImageUrl = imagePath
            };

            return await _productRepository.AddProductAsync(product);
        }

        public async Task<bool> UpdateProductAsync(UpdateProductDTO productDto)
        {
            if (productDto.Price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.");
            }

            // Save new image if provided
            if (productDto.ImageFile != null)
            {
                productDto.ImageUrl = await SaveImageUtil.SaveImageFileAsync(productDto.ImageFile);
            }

            var product = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                ImageUrl = productDto.ImageUrl
            };

            return await _productRepository.UpdateProductAsync(product);
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
