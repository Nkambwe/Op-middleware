using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operators.Moddleware.Data.Entities;

namespace Operators.Moddleware.Data.EntityConfigurations {
    public class ParameterEntityConfiguration {
        public static void Configure(EntityTypeBuilder<ConfigurationParameter> entityBuilder) {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Parameter).HasColumnName("Paramname").HasMaxLength(250).IsRequired();
            entityBuilder.Property(t => t.ParameterValue).HasColumnName("Paramvalue").HasMaxLength(250).IsRequired();
            entityBuilder.Property(t => t.Identifier).HasColumnName("Identifier").HasMaxLength(100).IsRequired();
            entityBuilder.Property(t => t.IsActive).HasDefaultValue(false);
            entityBuilder.Property(t => t.IsDeleted).HasDefaultValue(false);
            entityBuilder.Property(t => t.CreatedOn).IsRequired();
            entityBuilder.Property(t => t.CreatedBy).IsRequired();
            entityBuilder.Property(t => t.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(t => t.LastModifiedBy).IsRequired(false);
        }
    }
}
