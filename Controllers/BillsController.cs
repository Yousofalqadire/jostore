using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/bill")]
    public class BillsController:ControllerBase
    {
        private readonly IBill bill;

        public BillsController(IBill bill)
        {
            this.bill = bill;
        }
        [Authorize]
        [HttpPost("save-bill")]
        public async Task<ActionResult<SaveBillResponse>> SaveBill([FromBody] BillDto model)
        {
           return Ok(await bill.SaveBillAsync(model));

        }
        [HttpGet("get-bills")]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBillsAsync(){
            return Ok(await bill.GetBillsAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<BillDetails>>> GetBillDetails(int id){
            return Ok(await bill.GetBillDetails(id));
        }
        [HttpGet("get-bill/{id:int}")]
        public async Task<ActionResult<Bill>> GetBillById(int id){
            return Ok(await bill.GetBillById(id));
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<SaveBillResponse>> DeleteBill([FromRoute]int id)
        {
            return Ok(await bill.DeleteBill(id));
        }
    }
}