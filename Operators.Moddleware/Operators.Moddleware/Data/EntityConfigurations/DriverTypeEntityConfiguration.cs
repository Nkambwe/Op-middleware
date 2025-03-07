using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operators.Moddleware.Data.Entities.Business;

namespace Operators.Moddleware.Data.EntityConfigurations {

    public class DriverTypeEntityConfiguration {

        public static void Configure(EntityTypeBuilder<DriverType> entityBuilder) { 
            entityBuilder.ToTable("DriveType");
            entityBuilder.HasKey(d => d.Id);
            entityBuilder.Property(d => d.TypeName).HasColumnName("name").HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(d => d.IsActive).HasDefaultValue(false);
            entityBuilder.Property(d => d.IsDeleted).HasDefaultValue(false);
            entityBuilder.Property(d => d.CreatedOn).IsRequired();
            entityBuilder.Property(d => d.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(d => d.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(d => d.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);

            entityBuilder.HasMany(d => d.Drivers)
                .WithOne(t => t.DriverType)
                .HasForeignKey(t => t.DriverTypeId);
        }

    }

}
