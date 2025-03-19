using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Entities.Settings;

namespace Operators.Moddleware.Data.EntityConfigurations {

    public class UserEntityConfiguration {

        public static void Configure(EntityTypeBuilder<User> entityBuilder) {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.RoleId);
            entityBuilder.Property(t => t.BranchId);
            entityBuilder.Property(t => t.ThemeId);
            entityBuilder.Property(t => t.EmployeeNo).HasMaxLength(200).IsFixedLength().IsRequired();
            entityBuilder.Property(t => t.Username).HasMaxLength(250).IsRequired();
            entityBuilder.Property(t => t.EmailAddress).HasMaxLength(250).IsRequired();
            entityBuilder.Property(t => t.CurrentPassword).HasColumnName("pwd").IsRequired();
            entityBuilder.Property(t => t.IsActive).HasDefaultValue(true);
            entityBuilder.Property(t => t.IsVerified).HasDefaultValue(false);
            entityBuilder.Property(t => t.IsLoggedin).HasDefaultValue(false);
            entityBuilder.Property(t => t.IsDeleted).HasDefaultValue(false);
            entityBuilder.Property(t => t.CreatedOn).IsRequired();
            entityBuilder.Property(t => t.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(t => t.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(t => t.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);
        
            //One-to-One: User to Branch relationship
            entityBuilder.HasOne(u => u.Theme)
                .WithOne(t => t.User)
                .HasForeignKey<UserTheme>(t => t.UserId);

            // One-to-Many: User to Branch relationship
            entityBuilder
                .HasOne(u => u.Branch)
                .WithMany(b => b.Users)
                .HasForeignKey(u => u.BranchId)
                .OnDelete(DeleteBehavior.Restrict); 

            // One-to-Many: User to Role relationship
            entityBuilder
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);  

            // Many-to-Many: User to Permission relationship
            entityBuilder
                .HasMany(u => u.Permissions)
                .WithMany(p => p.Users)
                .UsingEntity<UserPermission>(
                    j => j
                        .HasOne(up => up.Permission)
                        .WithMany(p => p.UserPermissions)
                        .HasForeignKey(up => up.PermissionId),
                    j => j
                        .HasOne(up => up.User)
                        .WithMany(u => u.UserPermissions)
                        .HasForeignKey(up => up.UserId),
                    j =>
                    {
                        j.HasKey(t => new { t.UserId, t.PermissionId });
                        j.ToTable("UserPermissions");
                    });
        }

    }

}
