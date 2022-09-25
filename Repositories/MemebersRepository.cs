using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Api.Data;

namespace Api.Repositories
{
    public class MemebersRepository:IMember
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        public MemebersRepository(UserManager<ApplicationUser> _userManager, 
        RoleManager<IdentityRole> _roleManager,
        ApplicationDbContext _db, IMapper _mapper)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            db = _db;
            mapper = _mapper;

        }

        public async Task<IEnumerable<MemberDto>> GetMembers()
        {
            var users = await userManager.Users.ToListAsync();
            var userToReturn = new List<MemberDto>();
            foreach(var u in users){               
                var user = mapper.Map<MemberDto>(u);               
                userToReturn.Add(user);
            }
            return userToReturn;
        }

        public async Task<IEnumerable<string>> GetUserRoles(string id)
        {
           var user = await userManager.FindByIdAsync(id);
           var result =  await userManager.GetRolesAsync(user);
           return result.AsEnumerable<string>();
        }
    }
}