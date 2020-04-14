using Charity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Charity.Extensions
{
    public static class DatabaseInitializer
    {
        public static async Task Initialize(
            ApplicationDbContext context,
            UserManager<Models.DataModels.UserModels.User> userManager,
            RoleManager<Models.DataModels.UserModels.UserRole> roleManager,
            bool migarte)
        {
            if (migarte)
            {
                await context.Database.MigrateAsync();
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new Models.DataModels.UserModels.UserRole("Admin", "مدیر سیستم"));
            }

            if (!await roleManager.RoleExistsAsync("Charity"))
            {
                await roleManager.CreateAsync(new Models.DataModels.UserModels.UserRole("Charity", "حسابدار"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new Models.DataModels.UserModels.UserRole("User", "کاربر"));
            }

            await context.SaveChangesAsync();
            Models.DataModels.UserModels.User admin = await userManager.FindByNameAsync("admin");
            if (admin == null)
            {
                IdentityResult result = await userManager.CreateAsync(new Models.DataModels.UserModels.User(0, "admin@abc.com", false, "admin", null, false, null, "مدیر", "سیستم", null, null, null, null, null, null, null, null, DateTimeOffset.Now, null, null, null, Guid.NewGuid().ToString(), false, null, null, null, null, null));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors.Select(t => t.Description)));
                }

                admin = await userManager.FindByNameAsync("admin");
                await userManager.AddPasswordAsync(admin, "1234");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
