using InstrumentSite.Dtos.Product;
using InstrumentSite.Models;
using InstrumentSite.Repositories;
using InstrumentSite.Utilities;
using Microsoft.EntityFrameworkCore;
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
                ImageUrl = product.ImageUrl,
                IsSecondHand = product.IsSecondHand,
                CategoryId = product.Category.Id
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
                ImageUrl = product.ImageUrl,
                IsSecondHand = product.IsSecondHand
            };
        }

        public async Task<int> AddProductAsync(CreateProductDTO productDto, bool isSecondHand, HttpRequest request)
        {
            if (string.IsNullOrWhiteSpace(productDto.Name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            if (productDto.ImageFile == null)
            {
                throw new ArgumentException("Product image is required.");
            }

            // Save the image and generate full URL
            var imageUrl = await SaveImageUtil.SaveImageFileAsync(productDto.ImageFile, request);

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                IsSecondHand = isSecondHand,
                ImageUrl = imageUrl
            };

            return await _productRepository.AddProductAsync(product);
        }


        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryAsync(string categoryName)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(categoryName); // Use repository here
            return products.Select(product => new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                IsSecondHand = product.IsSecondHand,
                CategoryName = product.Category.Name // Map the Category.Name
            });
        }




        public async Task<IEnumerable<ProductDTO>> GetProductsByTypeAsync(bool isSecondHand)
        {
            var products = await _productRepository.GetProductsByTypeAsync(isSecondHand);
            return products.Select(product => new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryName = product.Category.Name,
                ImageUrl = product.ImageUrl,
                IsSecondHand = product.IsSecondHand
            });
        }


        public async Task<bool> UpdateProductAsync(UpdateProductDTO productDto, HttpRequest request)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(productDto.Id);
            if (existingProduct == null)
            {
                return false; // Product not found
            }

            // Handle new image upload
            if (productDto.ImageFile != null)
            {
                var newImageUrl = await SaveImageUtil.SaveImageFileAsync(productDto.ImageFile, request);
                productDto.ImageUrl = newImageUrl;

                // Optionally delete old image from storage
                var oldImagePath = Path.Combine("wwwroot", "uploads", "products", Path.GetFileName(existingProduct.ImageUrl));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            else
            {
                // Retain existing ImageUrl
                productDto.ImageUrl = existingProduct.ImageUrl;
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
