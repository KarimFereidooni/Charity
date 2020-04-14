namespace Charity.Controllers
{
    using Charity.Data;
    using Charity.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Models.DataModels.UserModels.User> userManager;
        private readonly RoleManager<Models.DataModels.UserModels.UserRole> roleManager;
        private readonly IWebHostEnvironment hostingEnvironment;

        public HomeController(
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

        public IActionResult Error()
        {
            return this.View();
        }

        public async Task<IActionResult> Init(bool migarte = true)
        {
            await DatabaseInitializer.Initialize(this.context, this.userManager, this.roleManager, migarte);
            return this.Ok("OK");
        }

        [HttpGet]
        public ActionResult UserAvatar(int id)
        {
            var query = this.context.Users.Where(t => t.Id == id);
            if (query.Any())
            {
                var avatarText = query.Single().Avatar;
                var bytes = Convert.FromBase64String(avatarText.Substring("data:image/png;base64,".Length));
                return this.File(bytes, "image/png");
            }

            return this.Content(string.Empty);
        }

        [Extensions.NoBrowserCache]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
        [HttpGet]
        public ActionResult CaptchaImage()
        {
            return new CaptchaImageResult();
        }
    }
}
