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
    [Route("api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly ISale sale;
        public SalesController(ISale sale)
        {
            this.sale = sale;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSalesAsync()
        {
            return Ok(await sale.GetSalesAsync());
        }
        [HttpGet("get-by-date/{date}")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSalesByDate(DateTime date)
        {
            return Ok(await sale.GetSaleByDate(date));
        }
        [HttpPost("add-to-sales")]
        public async Task<ActionResult<SalesResponse>> AddToSaleAsync([FromBody]Sale model)
        {
           return Ok(await sale.AddToSaleAsync(model));
        }
        [HttpGet("get-amount")]
        public async Task<ActionResult<SalesAmount>> GetSalesAmount()
        {
            return Ok(await sale.GetSalesAmount());
        }
    }
}