using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Dtos;
using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/anonymuseCart")]
    public class AnonymousShoppingCartController:ControllerBase
    {
        private readonly ApplicationDbContext db;
          private readonly IBill bill;
           private readonly IShoppingCart cart;
          
        public AnonymousShoppingCartController(IBill bill,IShoppingCart cart,ApplicationDbContext db)
        {
           this.bill = bill;
           this.cart = cart;
           this.db = db;
        }
        [HttpPost("add-to-cart")]
        public async Task<ActionResult> AddToAnonymouseCart([FromBody]List<AnonymouseCart> model)
        {
            foreach (var item in model)
            {
              var product = await db.Products.SingleOrDefaultAsync(x => x.Id == item.ProductId);
              var shopingCart = new ShoppingCart
              {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                ProductPric = item.ProductPric,
                ProductPhoto = item.ProductPhoto,
                Quantity = 1,
                TotalPrice =item.TotalPrice,
                SelectedSize = "",
                User = item.User             
              };
              var result = await db.ShoppinCarts.SingleOrDefaultAsync(x => x.ProductId == shopingCart.ProductId);
              if(result != null){
                 result.Quantity+=1;
             result.TotalPrice+= item.ProductPric;
             await db.SaveChangesAsync();
             return Ok(new AddToCartResponse{Ok = true,Massage = "تم تعديل المنتجات بنجاح"});
              }else{
                await db.ShoppinCarts.AddAsync(shopingCart);
                 await db.SaveChangesAsync();
                  var billDto = new BillDto
                { 
                   UserName = item.BillData.UserName,
                   Address=item.BillData.Address,
                   Phone = item.BillData.Phone

                };
                return Ok(await bill.SaveBillAsync(billDto));
              }
             
              
            }
              return Ok();
    }
    }
}