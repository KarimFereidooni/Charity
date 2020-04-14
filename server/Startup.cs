using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using Charity.Extensions;

namespace Charity
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            this.Configuration = configuration;
            this.HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
                options.AllowSynchronousIO = true;
            });
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddDbContext<Data.ApplicationDbContext>(options =>
                options.UseSqlite(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<Models.DataModels.UserModels.User, Models.DataModels.UserModels.UserRole>()
            .AddEntityFrameworkStores<Data.ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
#if DEBUG
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
#else
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
#endif
            });
            services.AddAuthentication(x =>
            {
                // x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // .AddJwtBearer(options =>
            // {
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = this.Configuration["Jwt:Issuer"],
            //        ValidAudience = this.Configuration["Jwt:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:Key"]))
            //    };
            // })
            .AddCookie(options =>
            {
                // Cookie settings
                // options.Cookie.HttpOnly = true;
                // options.Cookie.Expiration = System.TimeSpan.FromMinutes(30);
#if DEBUG
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
#endif
                options.ExpireTimeSpan = System.TimeSpan.FromMinutes(30);
                options.LoginPath = "/User/Login";
                options.AccessDeniedPath = "/User/AccessDenied";

                // options.SlidingExpiration = true;
            });
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";

                // SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                // options.Cookie.HttpOnly = true;
                // options.Cookie.Expiration = System.TimeSpan.FromMinutes(30);
#if DEBUG
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
#endif
                options.ExpireTimeSpan = System.TimeSpan.FromMinutes(30);
                options.LoginPath = "/User/Login";
                options.AccessDeniedPath = "/User/AccessDenied";

                // options.SlidingExpiration = true;
            });

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
               options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            // services.AddRazorPages().AddNewtonsoftJson(options =>
            //   options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            // Need to register ISpaStaticFileProvider for UseSpaStaticFiles middleware to work
            // services.AddSpaStaticFiles(configuration =>
            // {
            //    configuration.RootPath = "wwwroot";
            // });
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
                options.Cookie.Name = "XSRF-TOKEN";
                options.Cookie.HttpOnly = false;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.Path = "/";
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                // options.HttpsPort = 5001;
            });

#if DEBUG
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:8080")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            }));
#endif
            // services.AddSignalR(x =>
            // {
            //    x.EnableDetailedErrors = true;
            // }).AddNewtonsoftJsonProtocol();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            Data.ApplicationDbContext context,
            UserManager<Models.DataModels.UserModels.User> userManager,
            RoleManager<Models.DataModels.UserModels.UserRole> roleManager)
        {
            DatabaseInitializer.Initialize(context, userManager, roleManager, true).Wait();
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseExceptionMiddleware();
            }
            else
            {
                // app.UseDeveloperExceptionPage();
                app.UseExceptionMiddleware();

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseSpaStaticFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCookiePolicy();

            app.UseRewriter(new RewriteOptions().Add(ctx =>
            {
                // checking if the hostName has www. at the beginning
                var req = ctx.HttpContext.Request;
                var hostName = req.Host;
                if (hostName.ToString().ToLower().StartsWith("www."))
                {
                    // Strip off www.
                    var newHostName = hostName.ToString().Substring(4);

                    // Creating new url
                    var newUrl = $"{req.Scheme}://{newHostName}{req.PathBase}{req.Path}{req.QueryString}";

                    // Modify Http Response
                    var response = ctx.HttpContext.Response;
                    response.Headers[HeaderNames.Location] = newUrl;
                    response.StatusCode = 301;
                    ctx.Result = RuleResult.EndResponse;
                }
            }));
            app.UseRouting();
#if DEBUG
            app.UseCors("CorsPolicy");
#endif
            app.UseAuthentication();
            app.UseAuthorization();
            // app.UseWebSockets();
            // app.UseMiddleware<ChatWebSocketMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

                // endpoints.MapRazorPages();
                // endpoints.MapHub<Hub.ChatHub>("/chatHub");
                endpoints.MapFallbackToFile("index.html", new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(System.IO.Path.Combine(env.ContentRootPath, "wwwroot\\dist")),
                });
            });
        }
    }
}
