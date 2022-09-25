using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Interfaces;
using Api.Models;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly ApplicationDbContext db;
        private readonly ICloudStorage storage;
        public ProductRepository(ApplicationDbContext _db, ICloudStorage _storage)
        {
           db = _db;
           storage = _storage; 
        }
        public async Task<Product> AddProductAsync(ProductDto product)
        {
           var p = new Product();
           
           var Photos = new List<Photo>();
           var sizes = new List<Size>();
           p.Brand = product.Brand;
           p.Category = product.Category;
           p.IsPupular = product.IsPupular;
           p.SupCategory = product.SupCategory;
           p.AddedDate = DateTime.Now.ToString();
           p.Price = product.Price;
           p.Details = product.Description;
           if(product.SalePrice != 0){
              product.SalePrice = product.SalePrice;
           }else {
            p.SalePrice = 0;
           }
           foreach(var size in product.Sizes){
             var _size = new Size{
                Value = size,
                Quantity = 1,

             };
             sizes.Add(_size);
           }
           p.Sizes = sizes;
           foreach(var ph in product.Photos){
             var result = await storage.addPhotoAsync(ph);
             var photo = new Photo{
                PublicId = result.PublicId,
                Url = result.SecureUrl.AbsoluteUri,
             };
             Photos.Add(photo);
           }
           p.Photos = Photos;
           await db.Products.AddAsync(p);
           await db.SaveChangesAsync();
           return p;
        }

        public async Task<ProductResponse> DeleteProductByIdAsync(int id)
        {
            var product = await db.Products
            .Include(x => x.Photos).
            Include(x => x.Sizes).
            SingleOrDefaultAsync(x => x.Id == id);
            foreach (var item in product.Photos)
            {
                await storage.deletePhotoAsync(item.PublicId);
                
            }
             db.Products.Remove(product);
            await db.SaveChangesAsync();
            return new ProductResponse{Ok=true,Massage = "تم حذف المنتج بنجاح"};
        }

        public async Task<Product> DelteFromProductPhotosAsync(DeletePhotoDto model)
        {
            var prod = await db.Products.Include(x => x.Photos)
            .SingleOrDefaultAsync(x => x.Id == model.Id);
            var photo = prod.Photos.FirstOrDefault(x => x.PublicId == model.PublicId);
            await storage.deletePhotoAsync(photo.PublicId);
            prod.Photos.Remove(photo);
            await db.SaveChangesAsync();
            return prod;
             
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await db.Products.Include(x => x.Photos)
            .Include(x => x.Sizes)
            .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
           return await db.Products.Include(x => x.Photos)
           .Include(x => x.Sizes).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string Category)
        {
           return await db.Products.Include(x=> x.Photos)
           .Include(x => x.Sizes)
           .Where(x => x.Category == Category).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBySubCategoryAsync(string SupCategory)
        {
            return  await db.Products.Include(x => x.Photos)
            .Include(x => x.Sizes)
            .Where(x => x.SupCategory == SupCategory).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetPubularProducts()
        {
            return await db.Products.
            Include(x => x.Photos).
            Include(x => x.Sizes).
            Where(x => x.IsPupular == true).ToListAsync();
        }

        public async Task<Product> ModifyProductAsync(UpdateProductDto model)
        {

         var p = await db.Products.
             Include(x => x.Photos).
             Include(x => x.Sizes).
         SingleOrDefaultAsync(x => x.Id == model.Id);
             p.Brand = model.Brand;
             p.Category = model.Category;
             p.Price = model.Price;
             p.SalePrice = model.SalePrice;
             p.SupCategory = model.SupCategory;
             p.Details = model.Description;
             var photos = new List<Photo>();
             var sizes = new List<Size>();
             if(model.Photos != null){
              foreach (var item in model.Photos)
              {
                var result = await storage.addPhotoAsync(item);
                var photo = new Photo{
                    PublicId = result.PublicId,
                    Url = result.SecureUrl.AbsoluteUri
                };
                photos.Add(photo);
              }
               if(p.Photos != null)
              {
                p.Photos.AddRange(photos);
              }else{
                p.Photos  = photos;
              }
             }
           
             
            // p.Sizes = model.Sizes;
             await db.SaveChangesAsync();
           return p;
            
        }
    }
}