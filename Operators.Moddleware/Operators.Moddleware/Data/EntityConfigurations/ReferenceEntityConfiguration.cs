using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operators.Moddleware.Data.Entities.Business;

namespace Operators.Moddleware.Data.EntityConfigurations {

    public class ReferenceEntityConfiguration {

        public static void Configure(EntityTypeBuilder<Reference> entityBuilder) { 
            entityBuilder.ToTable("DriverReferees");
            entityBuilder.HasKey(r => r.Id);
            entityBuilder.Property(r => r.RefereName);
            entityBuilder.Property(r => r.PhoneNumber).HasColumnName("Phone").HasMaxLength(20).IsRequired(false);
            entityBuilder.Property(r => r.Email).HasMaxLength(200).HasColumnName("EmailAddress").IsRequired(false);
            entityBuilder.Property(r => r.Employment).HasMaxLength(200).IsFixedLength().IsRequired();
            entityBuilder.Property(r => r.DriverId).IsRequired();
            entityBuilder.Property(r => r.IsActive).HasDefaultValue();
            entityBuilder.Property(r => r.IsDeleted).HasDefaultValue();
            entityBuilder.Property(r => r.CreatedOn).IsRequired();
            entityBuilder.Property(r => r.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(r => r.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(r => r.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);

            entityBuilder.HasOne(d => d.Driver)
                .WithMany(m => m.References)
                .HasForeignKey(m => m.DriverId)
                .OnDelete(DeleteBehavior.Restrict); 
        }

    }

}
