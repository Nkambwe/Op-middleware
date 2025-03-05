using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Data.Entities.Settings;
using Operators.Moddleware.Data.EntityConfigurations;

namespace Operators.Moddleware.Data {
    public class OpsDbContext(DbContextOptions<OpsDbContext> options) : DbContext(options) {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPassword> Passwords { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<UserTheme> UserThemes { get; set; }
        public DbSet<ConfigurationParameter> Parameters { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<BusinessContact> Contacts { get; set; }
        public DbSet<BusinessIndustry> Industries { get; set; }
        public DbSet<BusinessEmployer> Businesses { get; set; }
        public DbSet<IndividualEmployer> Individuals { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<DriverType> DriverTypes { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            BranchEntityConfiguration.Configure(modelBuilder.Entity<Branch>());
            UserEntityConfiguration.Configure(modelBuilder.Entity<User>());
            RoleEntityConfiguration.Configure(modelBuilder.Entity<Role>());
            PermissionEntityConfiguration.Configure(modelBuilder.Entity<Permission>());
            UserPasswordEntityConfiguration.Configure(modelBuilder.Entity<UserPassword>());
            ThemeEntityConfiguration.Configure(modelBuilder.Entity<Theme>());
            UserThemeEntityConfiguration.Configure(modelBuilder.Entity<UserTheme>());
            ParameterEntityConfiguration.Configure(modelBuilder.Entity<ConfigurationParameter>());
            base.OnModelCreating(modelBuilder);
        }

    }
}
