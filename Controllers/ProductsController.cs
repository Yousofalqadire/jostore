using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Interfaces;
using Api.Models;
using Api.Dtos;
using Microsoft.AspNetCore.Authorization;
namespace Api.Controllers
{
    [ApiController]
     [Route("api/products")]
    public class ProductsController :ControllerBase
    {
        private readonly IProduct product;

        public ProductsController(IProduct product)
        {
            this.product = product;
        }

        [Authorize(Roles="admin")]
        [HttpPost("add-product")]
        public async Task<ActionResult<Product>> AddProduct([FromForm]ProductDto model){
          
          return Ok(await product.AddProductAsync(model));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> getProductById(int id){

            return Ok(await product.GetProductAsync(id));
        }

        [HttpGet("get-products")]
        public async Task<ActionResult<IEnumerable<Product>>> getProducts(){
          return Ok(await product.GetProductsAsync());
        }
       [HttpGet("get-products-by-subCategory/{categoryName}")]
       public async Task<ActionResult<IEnumerable<Product>>> GetProductsBySubCategory([FromRoute]string categoryName)
       {
           return Ok(await product.GetProductsBySubCategoryAsync(categoryName));
       }
       [HttpGet("get-products-by-category/{category}")]
       public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory([FromRoute]string category)
       {
           return Ok(await product.GetProductsByCategoryAsync(category));
       }
        [Authorize(Roles="admin")]
       [HttpDelete("{id:int}")]
       public async Task<ActionResult<ProductResponse>> DeleteProduct([FromRoute]int id)
       {
        return Ok(await product.DeleteProductByIdAsync(id));
       }
        [Authorize(Roles="admin")]
       [HttpPut("modify-product")]
       public async Task<ActionResult<Product>> ModifyProduct([FromForm]UpdateProductDto model){
        return Ok(await product.ModifyProductAsync(model));
       }
    }
}