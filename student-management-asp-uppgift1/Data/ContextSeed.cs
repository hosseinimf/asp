using Microsoft.AspNetCore.Identity;
using student_management_asp_uppgift1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using student_management_asp_uppgift1.Enums;

namespace student_management_asp_uppgift1.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Teacher.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Student.ToString()));
        }

        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var firstUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@domain.com",
                FirstName = "admin",
                LastName = "account",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != firstUser.Id))
            {
                var user = await userManager.FindByEmailAsync(firstUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(firstUser, "BytMig123!");
                    await userManager.AddToRoleAsync(firstUser, Enums.Roles.Admin.ToString());
                }
            }
        }
    }
}
