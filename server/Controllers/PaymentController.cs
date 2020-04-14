namespace Charity.Controllers
{
    using Charity.Data;
    using Charity.Models.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Models.DataModels.UserModels.User> userManager;
        private readonly RoleManager<Models.DataModels.UserModels.UserRole> roleManager;
        private readonly IWebHostEnvironment hostingEnvironment;

        public PaymentController(
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

        [Authorize]
        public async Task<IActionResult> AddPayment([FromBody]PaymentViewModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            long amount = model.Amount / model.Charities.Length;
            foreach (var charityId in model.Charities)
            {
                Models.DataModels.Payment payment = new Models.DataModels.Payment(0, user.Id, charityId, amount, DateTime.Now, model.Desceiption);
                this.context.Payments.Add(payment);
                await this.context.SaveChangesAsync();
            }

            return this.Ok();
        }

        [Authorize(Roles = "Admin, Charity")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var query = this.context.Payments.Where(x => x.Id == id).Select(x => new
            {
                x.Id,
                x.Amount,
                x.Desceiption,
                x.PaymentDateTime,
                x.CharityId,
                CharityName = x.Charity.Name,
                x.UserId,
                x.User.UserName,
                UserFullName = x.User.Name + " " + x.User.Surname
            });
            if (!(await query.AnyAsync()))
            {
                return this.NotFound();
            }

            return this.Json(await query.FirstOrDefaultAsync());
        }

        [Authorize(Roles = "Admin, Charity")]
        public async Task<IActionResult> GetPayments([FromQuery]string name, [FromBody]TablePagination model)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = null;
            }

            var query = this.context.Payments.Where(x => name == null || x.Charity.Name.Contains(name)).Select(x => new
            {
                x.Id,
                x.Amount,
                x.Desceiption,
                x.PaymentDateTime,
                x.CharityId,
                CharityName = x.Charity.Name,
                x.UserId,
                x.User.UserName,
                UserFullName = x.User.Name + " " + x.User.Surname
            });

            var count = await query.CountAsync();
            var totalAmount = await query.SumAsync(x => x.Amount);
            var dataQuery = query.Skip(model.ItemsPerPage * (model.Page - 1)).Take(model.ItemsPerPage);

            return this.Json(new { TotalCount = count, Data = await dataQuery.ToArrayAsync(), TotalAmount = totalAmount });
        }
    }
}
