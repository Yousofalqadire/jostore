using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Api.Dtos;
using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountContraller : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        public AccountContraller( UserManager<ApplicationUser> _userManager,IConfiguration config, RoleManager<IdentityRole> _roleMnager)
        {
            userManager =_userManager;
            configuration = config;
            roleManager = _roleMnager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserManagerDto>> Register([FromBody]RegisterDto model){
            
           if(ModelState.IsValid){
             var user = await userManager.FindByEmailAsync(model.Email);
            if(user != null){
             return BadRequest("this user is have an account try another !");
            }
            var User = new ApplicationUser{
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                Country = model.Country,
                PhoneNumber = model.PhoneNumber,
                City = model.City
            };
            if(model.ConfirmPassword == model.Password){
                var result = await userManager.CreateAsync(User,model.Password);
                 IdentityRole role = await roleManager.FindByNameAsync("member");
                 if(role != null){
                   await userManager.AddToRoleAsync(User,role.Name);
                 }
                
                if (result.Succeeded){
                    var confirmation = await userManager.GenerateEmailConfirmationTokenAsync(User);
                    return Ok(new UserManagerDto{IsCreated = true,
                     Confirmation = confirmation,UserId = User.Id,
                     Massage = "تم التسجيل بنجاح"});
                }
            }
           }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<Token>> Login([FromBody]LoginDto model){
            var user = await userManager.FindByEmailAsync(model.UserName);
            if(user == null) {
              return BadRequest("this user not found");
            }
            var result = await userManager.CheckPasswordAsync(user,model.Password);
            if(!result) return BadRequest("thw e password not maching");
             var claims = new List<Claim>(){
                new Claim("email", user.Email),
                new Claim("username",user.UserName),
                new Claim("name",user.FullName),
                new Claim("id",user.Id)
             };
             var roles = await userManager.GetRolesAsync(user);
             claims.AddRange(roles.Select(role => new Claim("roles",role)));
             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSetting:TokenKey"]));
             var token = new JwtSecurityToken(
                issuer: configuration["AuthSetting:Issuer"],
                audience: configuration["AuthSetting:Audience"],
                claims:claims,
                expires: System.DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)

             );
            var tokenAsString = new  JwtSecurityTokenHandler().WriteToken(token);
            
            return Ok(new Token{text = tokenAsString});
        }

        [HttpPost("confirm-registeration")]
        public async Task<ActionResult<UserManagerDto>> ConfirmRegisteration([FromBody]ConfirmReDto model)
        {
            var response = new UserManagerDto();
            if(!ModelState.IsValid) return BadRequest();
            var user = await userManager.FindByIdAsync(model.UserId);
            if(user != null){
                var result = await userManager.ConfirmEmailAsync(user,HttpUtility.UrlDecode(model.Confirmation));
                if(result.Succeeded){
                    user.EmailConfirmed = true;
                    response.IsCreated = true;
                    response.Massage = "تم تأكيد التسجيل بنجاح";
                }
            }
            return Ok(response);
        }
    }
}