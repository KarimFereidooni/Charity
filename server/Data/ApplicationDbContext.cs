namespace Charity.Data
{
    using Charity.Models.DataModels.UserModels;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public partial class ApplicationDbContext : IdentityDbContext<User, UserRole, int, UserClaim, UserInRole, UserLogin, UserRoleClaim, UserToken>
    {
        private readonly IConfiguration configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
             .UseSqlite(this.configuration.GetConnectionString("DefaultConnection"))
             .AddInterceptors(new Extensions.HintCommandInterceptor());
        }

        public virtual DbSet<Models.DataModels.Charity> Charities { get; set; }

        public virtual DbSet<Models.DataModels.CharityTag> CharityTags { get; set; }

        public virtual DbSet<Models.DataModels.Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("User");
            builder.Entity<UserRole>().ToTable("UserRole");
            builder.Entity<UserInRole>().ToTable("UserInRole");
            builder.Entity<UserClaim>().ToTable("UserClaim");
            builder.Entity<UserLogin>().ToTable("UserLogin");
            builder.Entity<UserRoleClaim>().ToTable("UserRoleClaim");
            builder.Entity<UserToken>().ToTable("UserToken");

            builder.Entity<User>(ModelBuilders.User.UserBuilder.Build);
            builder.Entity<UserRole>(ModelBuilders.User.UserRoleBuilder.Build);
            builder.Entity<UserClaim>(ModelBuilders.User.UserClaimBuilder.Build);
            builder.Entity<UserInRole>(ModelBuilders.User.UserInRoleBuilder.Build);
            builder.Entity<UserRoleClaim>(ModelBuilders.User.UserRoleClaimBuilder.Build);
        }
    }
}
