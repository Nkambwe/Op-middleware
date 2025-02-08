using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operators.Moddleware.Data.Entities.Access;

namespace Operators.Moddleware.Data.EntityConfigurations {
    public class PermissionEntityConfiguration {
        public static void Configure(EntityTypeBuilder<Permission> entityBuilder) {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.PermissionName).HasMaxLength(250).IsRequired();
            entityBuilder.Property(t => t.Description).HasMaxLength(250).IsRequired();
            entityBuilder.Property(t => t.IsActive).HasDefaultValue(false);
            entityBuilder.Property(t => t.IsDeleted).HasDefaultValue(false);
            entityBuilder.Property(t => t.CreatedOn).IsRequired();
            entityBuilder.Property(t => t.CreatedBy).HasMaxLength(10).IsFixedLength().IsRequired();
            entityBuilder.Property(t => t.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(t => t.LastModifiedBy).HasMaxLength(10).IsFixedLength().IsRequired(false);
        }
    }
}
