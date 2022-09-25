using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/photos")]
    public class PhototsController:ControllerBase
    {
        private readonly IProduct product;

        

        public PhototsController([FromBody]IProduct product)
        {
            this.product = product;
        }

        [HttpPut("modify-product")]
        public async Task<ActionResult<Product>> DeletePhotoFromProduct(DeletePhotoDto model)
        {
            return Ok(await product.DelteFromProductPhotosAsync(model));
            
        }
    }
}