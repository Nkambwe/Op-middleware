
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operators.Moddleware.Data.Entities;

namespace Operators.Moddleware.Data.EntityConfigurations {

    public class DistrictEntityConfiguration {
 
        public static void Configure(EntityTypeBuilder<District> entityBuilder) { 
            entityBuilder.ToTable("District");
            entityBuilder.HasKey(d => d.Id);
            entityBuilder.Property(d => d.DistrictName).HasColumnName("name").HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(d => d.IsActive).HasDefaultValue();
            entityBuilder.Property(d => d.IsDeleted).HasDefaultValue();
            entityBuilder.Property(d => d.CreatedOn).IsRequired();
            entityBuilder.Property(d => d.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(d => d.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(d => d.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);
        }
    }

}
