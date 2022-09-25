using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Dtos;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;


namespace Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCart
    {
        private readonly ApplicationDbContext db;

        public ShoppingCartRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        
        public async Task<AddToCartResponse> AddListOfProductsToShippingCart(List<AddProductToCartDto> products)
        {
            
               foreach (var item in products)
               {
                 var shopingCart = new ShoppingCart{
                ProductId = item.Product.Id,
                ProductName = item.Product.Brand,
                ProductPric = item.Product.Price,
                ProductPhoto = item.Product.Photos[0].Url,
                Quantity = 1,
                TotalPrice = item.Product.Price,
                User = item.User

                 };
                 await db.ShoppinCarts.AddAsync(shopingCart);
                  await db.SaveChangesAsync();
               }
             return new AddToCartResponse{Ok = true,Massage = "تم اضافة المنتجات بنجاح"};
        }

        public async Task<AddToCartResponse> AddToShoppingCart(AddProductToCartDto model)
        {
           
           var product = await db.ShoppinCarts.SingleOrDefaultAsync(x => x.ProductId == model.Product.Id );
           if(product != null){
             product.Quantity+=1;
             product.TotalPrice+= model.Product.Price;
             await db.SaveChangesAsync();
             return new AddToCartResponse{Ok = true,Massage = "تم تعديل المنتجات بنجاح"};
           }else
           {
            string selectedSiz ="";
            foreach (var item in model.Product.Sizes)
            {
              if(item.IsSelected == true)
              {
                selectedSiz = item.Value;
              }
            }

             var shopingCart = new ShoppingCart{
                ProductId = model.Product.Id,
                ProductName = model.Product.Brand,
                ProductPric = model.Product.Price,
                ProductPhoto = model.Product.Photos[0].Url,
                Quantity = 1,
                TotalPrice = model.Product.Price,
                SelectedSize = "",
                User = model.User
             };
             await db.ShoppinCarts.AddAsync(shopingCart);
             await db.SaveChangesAsync();
             return new AddToCartResponse{Ok = true,Massage = "تم اضافة المنتج بنجاح"};
           }
           
        }

        public async Task DeleteItemAsync(int id)
        {

            var item = await db.ShoppinCarts.SingleOrDefaultAsync(x => x.Id == id);
            double individualPrice = item.TotalPrice / item.Quantity;
            if(item.Quantity > 1){
              item.Quantity--;
              item.TotalPrice = item.TotalPrice - individualPrice;
              await db.SaveChangesAsync();
            }else{
              db.ShoppinCarts.Remove(item);
              await db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ShoppingCart>> GetShoppingCartsAssync(string user)
        {
           return await db.ShoppinCarts.Where(x => x.User == user).ToListAsync();
        }
    }
}