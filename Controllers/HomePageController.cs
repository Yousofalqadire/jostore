using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/homePage")]
    public class HomePageController:ControllerBase
    {
        private readonly IProduct product;

        public HomePageController(IProduct product)
        {
            this.product = product;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetPubularItems()
        {
            return Ok(await product.GetPubularProducts());
        }
    }
}