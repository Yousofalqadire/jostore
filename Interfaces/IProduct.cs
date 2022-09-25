using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Dtos;

namespace Api.Interfaces
{
    public interface IProduct
    {
        Task<Product> AddProductAsync(ProductDto product);
        Task<Product> GetProductAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string Category);
        Task<IEnumerable<Product>> GetProductsBySubCategoryAsync(string SupCategory);
        Task<ProductResponse> DeleteProductByIdAsync(int id);
        Task<Product> ModifyProductAsync(UpdateProductDto product);
        Task<IEnumerable<Product>> GetPubularProducts();
        Task<Product> DelteFromProductPhotosAsync(DeletePhotoDto model);
        
       
    }
}