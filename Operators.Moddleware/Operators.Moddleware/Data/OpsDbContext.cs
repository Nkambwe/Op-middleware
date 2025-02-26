using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Entities.Settings;
using Operators.Moddleware.Data.EntityConfigurations;

namespace Operators.Moddleware.Data {
    public class OpsDbContext : DbContext {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPassword> Passwords { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<UserTheme> UserThemes { get; set; }
        public DbSet<ConfigurationParameter> Parameters { get; set; }
        
        public OpsDbContext(DbContextOptions<OpsDbContext> options)
            : base(options) { }

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
