using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Api.Data.SeedingData
{
    public class Seed
    {
        public static  async Task SeedingRoles(RoleManager<IdentityRole> roleManager){
           if(!roleManager.Roles.Any()){
             List<IdentityRole> roles = new List<IdentityRole>();
            roles.Add(new IdentityRole{Name= "member"});
            roles.Add(new IdentityRole{Name= "manager"});
            foreach(var role in roles){
               await roleManager.CreateAsync(role);
            }
           }
        }
        public static async Task SeedingCategories(ApplicationDbContext db){
            if(!db.Categories.Any()){
                List<Category> categories = new List<Category>();
                categories.Add(new Category{Name = "Bags"});
                categories.Add(new Category{Name = "Slim-Fit-Jeans"});
                categories.Add(new Category{Name = "Parphans"});
                categories.Add(new Category{Name = "Accessorise"});
                await db.Categories.AddRangeAsync(categories);
                await db.SaveChangesAsync();
            }
        }
        public static async Task SeedingAdmin(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager){
            if(!userManager.Users.Any()){
                ApplicationUser user = new ApplicationUser{
                    Email = "admin@jostor.com",
                    FullName = "ranadah rawashdeh",
                    EmailConfirmed = true,
                    PhoneNumber = "0781000987",
                    UserName = "admin@jostor.com"
                };
                await userManager.CreateAsync(user,"Randah@ra#1");
                IdentityRole role = new IdentityRole{Name ="admin"};
                await roleManager.CreateAsync(role);
                await userManager.AddToRoleAsync(user,role.Name);
            }
        }
    }
}