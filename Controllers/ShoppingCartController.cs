using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Api.Models;

namespace Api.Controllers
{
    [ApiController]
     [Route("api/cart")]
    public class ShoppingCartController:ControllerBase
    {
        private readonly IShoppingCart cart;

        public ShoppingCartController(IShoppingCart cart)
        {
            this.cart = cart;
        }
        [Authorize]
        [HttpPost("add-to-cart")]
        public async Task<ActionResult<AddToCartResponse>> AddToCart(AddProductToCartDto model)
        {
           var result = await cart.AddToShoppingCart(model);
           if(result.Ok){
            return Ok(result);
           }else {
            return BadRequest(new AddToCartResponse{Ok = false,Massage = "لم يتم اضافة المنتج هناك بعض الأخطاء"});
           }
        }
                
        [Authorize]
        [HttpGet("get-cart-items/{user}")]
        public async Task<ActionResult<IEnumerable<ShoppingCart>>> GetShoppingCartItems(string user){
           return Ok(await cart.GetShoppingCartsAssync(user));
        }
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteItemAsync(int id){
            await cart.DeleteItemAsync(id);
            return Ok();
        }
    }
}