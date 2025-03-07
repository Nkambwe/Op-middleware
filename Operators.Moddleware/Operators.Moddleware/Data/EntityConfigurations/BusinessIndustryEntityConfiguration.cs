
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operators.Moddleware.Data.Entities.Business;

namespace Operators.Moddleware.Data.EntityConfigurations {

    public class BusinessIndustryEntityConfiguration {
        
        public static void Configure(EntityTypeBuilder<BusinessIndustry> entityBuilder) { 
            entityBuilder.ToTable("Industry");
            entityBuilder.HasKey(d => d.Id);
            entityBuilder.Property(d => d.IndustryName).HasColumnName("name").HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(d => d.IsActive).HasDefaultValue();
            entityBuilder.Property(d => d.IsDeleted).HasDefaultValue();
            entityBuilder.Property(d => d.CreatedOn).IsRequired();
            entityBuilder.Property(d => d.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(d => d.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(d => d.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);
        }
    }

}
