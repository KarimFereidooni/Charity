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
            Models.DataModels.UserModels.User charity = await userManager.FindByNameAsync("charity");
            if (admin == null)
            {
                IdentityResult result = await userManager.CreateAsync(new Models.DataModels.UserModels.User(0, "admin@abc.com", false, "admin", null, false, null, "مدیر", "سیستم", null, null, null, null, null, null, null, null, DateTime.Now, null, null, null, Guid.NewGuid().ToString(), false, null, null, null, null, null));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors.Select(t => t.Description)));
                }

                admin = await userManager.FindByNameAsync("admin");
                await userManager.AddPasswordAsync(admin, "1234");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            if (charity == null)
            {
                IdentityResult result = await userManager.CreateAsync(new Models.DataModels.UserModels.User(0, "charity@abc.com", false, "charity", null, false, null, "حسابدار", "سیستم", null, null, null, null, null, null, null, null, DateTime.Now, null, null, null, Guid.NewGuid().ToString(), false, null, null, null, null, null));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors.Select(t => t.Description)));
                }

                charity = await userManager.FindByNameAsync("charity");
                await userManager.AddPasswordAsync(charity, "1234");
                await userManager.AddToRoleAsync(charity, "Charity");
            }

            if (context.Charities.Count() == 0)
            {
                Models.DataModels.Charity charity1 = new Models.DataModels.Charity(0, "محک", "تهران");
                Models.DataModels.Charity charity2 = new Models.DataModels.Charity(0, "ثاراله", "شیراز");
                Models.DataModels.Charity charity3 = new Models.DataModels.Charity(0, "کمیته امداد", "مشهد");
                Models.DataModels.Charity charity4 = new Models.DataModels.Charity(0, "حلال احمر", "تهران");
                context.Charities.Add(charity1);
                await context.SaveChangesAsync();
                Models.DataModels.CharityTag tag1 = new Models.DataModels.CharityTag(0, charity1.Id, "سرطان");
                context.CharityTags.Add(tag1);
                Models.DataModels.CharityTag tag2 = new Models.DataModels.CharityTag(0, charity1.Id, "کودک");
                context.CharityTags.Add(tag2);
                await context.SaveChangesAsync();

                context.Charities.Add(charity2);
                await context.SaveChangesAsync();
                Models.DataModels.CharityTag tag3 = new Models.DataModels.CharityTag(0, charity2.Id, "سرطان");
                context.CharityTags.Add(tag3);
                Models.DataModels.CharityTag tag4 = new Models.DataModels.CharityTag(0, charity2.Id, "کودک");
                context.CharityTags.Add(tag4);
                Models.DataModels.CharityTag tag5 = new Models.DataModels.CharityTag(0, charity2.Id, "نوزاد");
                context.CharityTags.Add(tag5);
                await context.SaveChangesAsync();

                context.Charities.Add(charity3);
                await context.SaveChangesAsync();
                Models.DataModels.CharityTag tag6 = new Models.DataModels.CharityTag(0, charity3.Id, "بازنشسته");
                context.CharityTags.Add(tag6);
                await context.SaveChangesAsync();

                context.Charities.Add(charity4);
                await context.SaveChangesAsync();
                Models.DataModels.CharityTag tag7 = new Models.DataModels.CharityTag(0, charity4.Id, "سیل");
                context.CharityTags.Add(tag7);
                Models.DataModels.CharityTag tag8 = new Models.DataModels.CharityTag(0, charity4.Id, "زلزله");
                context.CharityTags.Add(tag8);
                await context.SaveChangesAsync();
            }
        }
    }
}
