using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operators.Moddleware.Data.Entities;

namespace Operators.Moddleware.Data.EntityConfigurations {
    public class BranchEntityConfiguration {
        public static void Configure(EntityTypeBuilder<Branch> entityBuilder) {
            entityBuilder.HasKey(b => b.Id);
            entityBuilder.Property(b => b.BranchCode).HasMaxLength(10).IsFixedLength().IsRequired();
            entityBuilder.Property(b => b.BranchName).HasMaxLength(250).IsRequired();
            entityBuilder.Property(b => b.IsActive).HasDefaultValue(false);
            entityBuilder.Property(b => b.IsDeleted).HasDefaultValue(false);
            entityBuilder.Property(b => b.CreatedOn).IsRequired();
            entityBuilder.Property(b => b.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(b => b.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(b => b.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);
        
        }
    }
}
