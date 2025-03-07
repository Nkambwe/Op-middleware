using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operators.Moddleware.Data.Entities.Access;

namespace Operators.Moddleware.Data.EntityConfigurations {
    public class UserPasswordEntityConfiguration {
        public static void Configure(EntityTypeBuilder<UserPassword> entityBuilder) {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.UserId).IsRequired();
            entityBuilder.Property(t => t.Password).HasColumnName("User_pwd").HasMaxLength(250).IsRequired();
            entityBuilder.Property(t => t.IsActive).HasDefaultValue();
            entityBuilder.Property(t => t.IsDeleted).HasDefaultValue();
            entityBuilder.Property(t => t.CreatedOn).IsRequired();
            entityBuilder.Property(t => t.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(t => t.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(t => t.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);
        }
    }
}
