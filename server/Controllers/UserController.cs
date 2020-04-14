namespace Charity.Controllers
{
    using Charity.Data;
    using Charity.Extensions;
    using Charity.Models.ViewModels.User;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Models.DataModels.UserModels.User> userManager;
        private readonly SignInManager<Models.DataModels.UserModels.User> signInManager;
        private readonly IAntiforgery antiforgery;
        private readonly IConfiguration config;

        public UserController(
            ApplicationDbContext context,
            UserManager<Models.DataModels.UserModels.User> userManager,
            SignInManager<Models.DataModels.UserModels.User> signInManager,
            IAntiforgery antiforgery,
            IConfiguration config)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.antiforgery = antiforgery;
            this.config = config;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return this.Unauthorized(new { Error = "شما برای مدتی از نرم افزار استفاده نکرده اید و از سیستم خارج شده اید", ErrorCode = 401 });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                string errors = string.Join("\n", this.ModelState.Values.Select(t => string.Join("\n", t.Errors.Select(e => e.ErrorMessage).ToArray())).ToArray());
                throw new Exception("لطفا خطاهای زیر را برطرف کنید:\n" + errors);
            }

            Models.DataModels.UserModels.User user = null;
            if (model.UserName.Contains("@", StringComparison.CurrentCulture))
            {
                user = await this.userManager.FindByEmailAsync(model.UserName);
                if (user == null)
                {
                    throw new Exception($"هیچ کاربری با ایمیل {model.UserName} پیدا نشد");
                }
            }
            else if (model.UserName.StartsWith("09"))
            {
                user = this.context.Users.Where(t => t.PhoneNumber == model.UserName || t.NormalizedUserName == model.UserName.ToUpper(System.Globalization.CultureInfo.CurrentCulture)).FirstOrDefault();
                if (user == null)
                {
                    throw new Exception($"هیچ کاربری با شماره موبایل {model.UserName} پیدا نشد");
                }
            }
            else
            {
                user = await this.userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    throw new Exception($"هیچ کاربری با نام کاربری {model.UserName} پیدا نشد");
                }
            }

            if (user.Disabled)
            {
                throw new Exception("حساب کاربری غیر فعال شده است");
            }

            if (!await this.userManager.HasPasswordAsync(user))
            {
                throw new Exception($"برای حساب کاربری شما رمز عبور تعیین نشده است. لطفا روی لینک زیر کلیک کنید و برای حساب کاربری خود رمز عبور تعیین کنید<br/><a href=\"/forgetPassword\">تعیین رمز عبور (لطفا اینجا کلیک کنید)</a>");
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await this.signInManager.PasswordSignInAsync(user, model.Password, true, true);
            if (result.Succeeded)
            {
                this.antiforgery.GetAndStoreTokens(this.HttpContext);
                user.LastLoginDateTime = user.LoginDateTime;
                user.LoginDateTime = DateTimeOffset.Now;
                await this.context.SaveChangesAsync();
                return this.Json(new ProfileViewModel(user, true, this.GetUserRoles(user), string.Empty));
            }

            if (result.RequiresTwoFactor)
            {
                throw new Exception("نیاز به تایید دو مرحله ای وجود دارد.");
            }

            if (result.IsLockedOut)
            {
                throw new Exception("بدلیل اینکه 5 بار رمز عبور اشتباه وارد شده است حساب کاربری حداکثر بمدت 10 دقیقه غیر فعال است");
            }
            else
            {
                throw new Exception("رمزعبور نادرست است.");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return this.Unauthorized(new { Error = "دسترسی غیرمجاز. شما مجوز لازم را ندارید", ErrorCode = 403 });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return this.Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (model == null)
            {
                throw new Exception("اطلاعات به درستی ارسال نشده است");
            }

            if (!this.ModelState.IsValid)
            {
                string errors = string.Join("\n", this.ModelState.Values.Select(t => string.Join("\n", t.Errors.Select(e => e.ErrorMessage).ToArray())).ToArray());
                throw new Exception("لطفا خطاهای زیر را برطرف کنید:\n" + errors);
            }

            if (!this.User.Identity.IsAuthenticated)
            {
                try
                {
                    ValidateCaptchaHelper.Instance.Execute(this.HttpContext, model.Captcha);
                }
                catch (Exception ex)
                {
                    throw new Exception("لطفا خطاهای زیر را برطرف کنید:\n" + ex.Message);
                }
            }

            if (model.SelectedRole == "Admin" && (!this.User.Identity.IsAuthenticated || !(await this.userManager.IsInRoleAsync(await this.userManager.GetUserAsync(this.User), "Admin"))))
            {
                throw new Exception("فقط کاربر مدیر می تواند یک مدیر ثبت نام کند");
            }

            if (!Regex.IsMatch(model.UserName, @"^[a-zA-Z0-9-._]+$"))
            {
                throw new Exception($"برای نام کاربری فقط از حروف انگلیسی، اعداد و کاراکترهای . - _ استفاده کنید");
            }

            if (this.context.Users.Where(t => t.NormalizedUserName == model.UserName.ToUpper()).Any())
            {
                throw new Exception($"نام کاربری {model.UserName} قبلا ثبت شده است");
            }

            if (!string.IsNullOrEmpty(model.Email) && this.context.Users.Where(t => t.NormalizedEmail == model.Email.ToUpper()).Any())
            {
                throw new Exception($"ایمیل {model.Email} قبلا ثبت شده است");
            }

            if (!string.IsNullOrEmpty(model.PhoneNumber) && this.context.Users.Where(t => t.PhoneNumber == model.PhoneNumber).Any())
            {
                throw new Exception($"شماره موبایل {model.PhoneNumber} قبلا ثبت شده است");
            }

            if (!string.IsNullOrEmpty(model.PhoneNumber) && !model.PhoneNumber.StartsWith("09"))
            {
                throw new Exception($"شماره موبایل باید با 09 شروع شود");
            }

            if (!string.IsNullOrEmpty(model.PhoneNumber) && model.PhoneNumber.Length != 11)
            {
                throw new Exception($"شماره موبایل باید 11 رقم باشد");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                model.Name = model.UserName;
            }

            Models.DataModels.UserModels.User user;
            using (var transaction = this.context.Database.BeginTransaction())
            {
                var newUser = new Models.DataModels.UserModels.User(0, model.Email, false, model.UserName, model.PhoneNumber, false, model.Title, model.Name, model.Surname, null, null, null, null, null, null, null, null, DateTimeOffset.Now, null, DateTimeOffset.Now, null, Guid.NewGuid().ToString(), false, null, null, null, null, null);
                IdentityResult createResult = await this.userManager.CreateAsync(newUser);
                if (!createResult.Succeeded)
                {
                    throw new Exception(string.Join("\n", createResult.Errors.Select(t => t.Description)));
                }

                user = await this.userManager.FindByNameAsync(model.UserName);
                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    await this.userManager.AddPasswordAsync(user, model.Password);
                }

                if (model.SelectedRole == "Charity")
                {
                    await this.userManager.AddToRoleAsync(user, "Charity");
                }
                else if (model.SelectedRole == "User")
                {
                    await this.userManager.AddToRoleAsync(user, "User");
                }
                else if (model.SelectedRole == "Admin")
                {
                    await this.userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    throw new Exception("نقش نامعتبر");
                }

                transaction.Commit();
            }

            if (!this.User.Identity.IsAuthenticated)
            {
                await this.signInManager.SignInAsync(user, true);
                this.antiforgery.GetAndStoreTokens(this.HttpContext);
                return this.Json(new ProfileViewModel(user, true, this.GetUserRoles(user), string.Empty));
            }
            else
            {
                var currentUser = await this.userManager.GetUserAsync(this.User);
                return this.Json(new ProfileViewModel(currentUser, true, this.GetUserRoles(currentUser), string.Empty));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAvatar([FromBody] Models.ViewModels.User.ProfilePictureViewModel model)
        {
            byte[] imageBytes = Convert.FromBase64String(model.Avatar.Substring(model.Avatar.IndexOf("base64,") + "base64,".Length));
            string base64String;
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                using Image image = Image.FromStream(ms, true);
                using Image newImage = GraphicsHelper.ResizeImage(image, 120, 120);
                using MemoryStream m = new MemoryStream();
                newImage.Save(m, ImageFormat.Png);
                m.Flush();
                byte[] newImageBytes = m.ToArray();
                base64String = Convert.ToBase64String(newImageBytes);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            user.Avatar = "data:image/png;base64," + base64String;
            user.LastUpdateDateTime = DateTimeOffset.Now;
            await this.context.SaveChangesAsync();
            return this.Json(new ProfileViewModel(user, true, this.GetUserRoles(user), string.Empty));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile([FromBody] Models.ViewModels.User.ProfileViewModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (string.IsNullOrEmpty(model.Name))
            {
                model.Name = model.UserName;
            }

            Models.DataModels.UserModels.User newUser = new Models.DataModels.UserModels.User(user.Id, model.Email, user.EmailConfirmed, model.UserName, model.PhoneNumber, user.PhoneNumberConfirmed, model.Title, model.Name, model.Surname, model.Avatar, model.NationCode, model.IdNumber, model.HomePhoneNumber, model.WorkPhoneNumber, model.Fax, model.Address, model.PostalCode, user.RegisterDateTime, user.LastLoginDateTime, user.LoginDateTime, DateTimeOffset.Now, user.ChatId, user.Disabled, model.IDPayLink, model.BankName, model.BankAccountNumber, model.BankCard, model.BankIBAN);
            Models.DataModels.UserModels.User.Copy(newUser, user);
            await this.context.SaveChangesAsync();
            return this.Json(new ProfileViewModel(user, true, this.GetUserRoles(user), string.Empty));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] Models.ViewModels.User.ChangePasswordViewModel model)
        {
            if (model == null)
            {
                throw new Exception("اطلاعات نادرست ارسال شده است");
            }

            if (!this.ModelState.IsValid)
            {
                string errors = string.Join("\n", this.ModelState.Values.Select(t => string.Join("\n", t.Errors.Select(e => e.ErrorMessage).ToArray())).ToArray());
                throw new Exception("لطفا خطاهای زیر را برطرف کنید:\n" + errors);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            if (!await this.userManager.HasPasswordAsync(user))
            {
                await this.userManager.AddPasswordAsync(user, model.NewPassword);
            }
            else
            {
                if (!await this.userManager.CheckPasswordAsync(user, model.CurrentPassword))
                {
                    throw new Exception("رمز عبور فعلی نادرست وارد شده است");
                }

                await this.userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            }

            return this.Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserInfo()
        {
            Models.DataModels.UserModels.User user = null;
            if (this.User.Identity.IsAuthenticated)
            {
                user = await this.userManager.GetUserAsync(this.User);
            }

            return this.Json(new ProfileViewModel(user, this.User.Identity.IsAuthenticated, this.User.Identity.IsAuthenticated ? this.GetUserRoles(user) : null, string.Empty));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProfile(int id)
        {
            var query = from x in this.context.Users
                        where x.Id == id
                        let roles = this.context.UserRoles.Where(t => t.UserId == x.Id).Select(t => new Models.ViewModels.User.UserRoleViewModel
                        {
                            Id = t.RoleId,
                            Name = t.UserRole.Name,
                            Title = t.UserRole.Title,
                        }).ToArray()
                        select new
                        {
                            x.Id,
                            x.FullName,
                            x.UserName,
                            x.Avatar,
                            Roles = roles,
                        };
            var user = await query.SingleOrDefaultAsync();
            if (user == null)
            {
                return this.NotFound();
            }

            return this.Json(user);
        }

        public Models.ViewModels.User.UserRoleViewModel[] GetUserRoles(Models.DataModels.UserModels.User user)
        {
            return this.context.UserRoles.Where(t => t.UserId == user.Id).Select(t => new Models.ViewModels.User.UserRoleViewModel(t.RoleId, t.UserRole.Name, t.UserRole.Title)).ToArray();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers(string role, string fullName, [FromBody]Models.ViewModels.TablePagination model)
        {
            IQueryable<Models.DataModels.UserModels.User> query = this.context.Users;
            if (!string.IsNullOrEmpty(role) && role != "All")
            {
                query = query.Where(x => x.Roles.Select(x => x.UserRole.NormalizedName).Contains(role.ToUpper()));
            }

            if (!string.IsNullOrEmpty(fullName))
            {
                query = query.Where(x => x.FullName.Contains(fullName));
            }

            if (model.SortDesc[0])
            {
                query = query.DynamicOrderByDescending(model.SortBy[0]);
            }
            else
            {
                query = query.DynamicOrderBy(model.SortBy[0]);
            }

            IQueryable<Models.DataModels.UserModels.User> dataQuery;
            if (model.ItemsPerPage > 0)
            {
                dataQuery = query.Skip((model.Page - 1) * model.ItemsPerPage).Take(model.ItemsPerPage);
            }
            else
            {
                dataQuery = query;
            }

            return this.Json(new
            {
                Data = await dataQuery.Select(x => new
                {
                    x.Id,
                    x.FullName,
                    x.UserName,
                    x.Email,
                    x.PhoneNumber,
                    x.LoginDateTime,
                }).ToArrayAsync(),
                TotalCount = await query.CountAsync(),
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> LoginUser([FromQuery]string userName)
        {
            var user = await this.userManager.FindByNameAsync(userName);
            if (user != null)
            {
                await this.signInManager.SignOutAsync();
                await this.signInManager.SignInAsync(user, false);
                return this.Json(new ProfileViewModel(user, true, this.GetUserRoles(user), string.Empty));
            }
            else
            {
                throw new Exception("کاربر پیدا نشد");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateResetPasswordCode([FromBody]PasswordResetViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                string errors = string.Join("\n", this.ModelState.Values.Select(t => string.Join("\n", t.Errors.Select(e => e.ErrorMessage).ToArray())).ToArray());
                throw new Exception("لطفا خطاهای زیر را برطرف کنید:\n" + errors);
            }

            var user = await this.context.Users.SingleOrDefaultAsync(x => x.NormalizedEmail == model.Email.ToUpper());
            if (user == null)
            {
                throw new Exception("هیچ کاربری با این ایمیل پیدا نشد");
            }

            // string code = await this.userManager.GeneratePasswordResetTokenAsync(user);
            // await this.emailService.SendAsync("کد اهراز هویت", $"خیریه\nکد اهراز هویت شما برای تغییر رمزعبور:\n{code}", model.Email);
            return this.Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody]PasswordResetLevel2ViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                string errors = string.Join("\n", this.ModelState.Values.Select(t => string.Join("\n", t.Errors.Select(e => e.ErrorMessage).ToArray())).ToArray());
                throw new Exception("لطفا خطاهای زیر را برطرف کنید:\n" + errors);
            }

            var user = await this.context.Users.SingleOrDefaultAsync(x => x.NormalizedEmail == model.Email.ToUpper());
            if (user == null)
            {
                throw new Exception("هیچ کاربری با این ایمیل پیدا نشد");
            }

            var result = await this.userManager.ResetPasswordAsync(user, model.Code, model.NewPassword);
            if (!result.Succeeded)
            {
                string code = result.Errors.First().Code;
                if (code == "InvalidToken")
                {
                    throw new Exception("کد اهراز هویت ارسال شده صحیح نیست");
                }

                throw new Exception(string.Join("\n", result.Errors.Select(x => x.Description).ToArray()));
            }

            user.EmailConfirmed = true;
            await this.context.SaveChangesAsync();
            return this.Ok();
        }
    }
}
