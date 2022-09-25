using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MembersController:ControllerBase
    {
        
        private readonly IMember member;

        public MembersController(IMember member)
        {
            this.member = member;
        }

        [HttpGet("get-users")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers()
        {
            return Ok(await member.GetMembers());

        }
        [HttpGet("get-roles")]
        public async Task<ActionResult<IEnumerable<string>>> GetUserRoles(string id)
        {
            return Ok(await member.GetUserRoles(id));
        }
    }
}