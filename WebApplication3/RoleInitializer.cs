using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApplication3
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminName = "AmroSuper";
            string password = "Cronaldo789/";
            if (await roleManager.FindByNameAsync("superuser") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("superuser"));
            }
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminName) == null)
            {
                User superuser = new User {UserName = adminName };
                IdentityResult result = await userManager.CreateAsync(superuser, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superuser, "superuser");
                }

            }
            
        }
    }
}
