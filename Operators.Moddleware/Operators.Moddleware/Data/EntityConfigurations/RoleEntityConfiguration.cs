using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operators.Moddleware.Data.Entities.Access;

namespace Operators.Moddleware.Data.EntityConfigurations {
    public class RoleEntityConfiguration {
        public static void Configure(EntityTypeBuilder<Role> entityBuilder) {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.RoleName).HasMaxLength(250).IsRequired();
            entityBuilder.Property(t => t.Description).HasMaxLength(250).IsRequired();
            entityBuilder.Property(t => t.IsActive).HasDefaultValue(false);
            entityBuilder.Property(t => t.IsDeleted).HasDefaultValue(false);
            entityBuilder.Property(t => t.CreatedOn).IsRequired();
            entityBuilder.Property(t => t.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(t => t.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(t => t.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);
        }
    }
}
