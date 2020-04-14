namespace Charity.Controllers
{
    using Charity.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using Transer.Models.ViewModels;

    [AllowAnonymous]
    public class CharityController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Models.DataModels.UserModels.User> userManager;
        private readonly RoleManager<Models.DataModels.UserModels.UserRole> roleManager;
        private readonly IWebHostEnvironment hostingEnvironment;

        public CharityController(
            ApplicationDbContext context,
            UserManager<Models.DataModels.UserModels.User> userManager,
            RoleManager<Models.DataModels.UserModels.UserRole> roleManager,
            IWebHostEnvironment hostingEnvironment)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> GetCharities([FromBody]SearchCharitiesViewModel model)
        {
            var query = this.context.Charities.Select(x => new
            {
                x.Id,
                x.Name,
                x.Location,
                Tags = x.Tags.Select(xx => xx.Tag),
                Selected = false
            });
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                model.Name = model.Name.Trim();
                query = query.Where(t => t.Name.Contains(model.Name));
            }

            if (!string.IsNullOrWhiteSpace(model.Tag))
            {
                model.Tag = model.Tag.Trim();
                query = query.Where(t => t.Tags.Contains(model.Tag));
            }

            if (!string.IsNullOrWhiteSpace(model.Location))
            {
                model.Location = model.Location.Trim();
                query = query.Where(t => t.Location.Contains(model.Location));
            }

            var count = await query.CountAsync();
            var dataQuery = query.Skip(model.RowsPerPage * (model.Page - 1)).Take(model.RowsPerPage);

            return this.Json(new { TotalCount = count, Data = await dataQuery.ToArrayAsync() });
        }
    }
}
