
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities.Business;

namespace Operators.Moddleware.Data.EntityConfigurations {

    public class DriverEntityConfiguration {

         public static void Configure(EntityTypeBuilder<Driver> entityBuilder) {
            entityBuilder.HasKey(d => d.Id);
            entityBuilder.Property(d => d.FirstName).HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(d => d.MiddleName).HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(d => d.LastName).HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(d => d.DateOfBirth).HasColumnName("Dob").IsRequired(false);
            entityBuilder.Property(d => d.Email).HasMaxLength(200).HasColumnName("EmailAddress").IsRequired(false);
            entityBuilder.Property(d => d.PrimaryContact).HasColumnName("Phone1").HasMaxLength(20).IsFixedLength().IsRequired(false);
            entityBuilder.Property(d => d.SecondaryContact).HasColumnName("Phone2").HasMaxLength(20).IsFixedLength().IsRequired(false);
            entityBuilder.Property(d => d.ResidenceArea).HasColumnName("PhysicalAddress").IsRequired(false);
            entityBuilder.Property(d => d.Facebook).HasColumnName("Fbk").HasMaxLength(400).IsRequired(false);
            entityBuilder.Property(d => d.WhatsApp).HasColumnName("Wap").HasMaxLength(400).IsRequired(false);
            entityBuilder.Property(d => d.Tweeter).HasColumnName("Twr").HasMaxLength(400).IsRequired(false);
            entityBuilder.Property(d => d.YearsOfExperience).HasColumnName("Experience").IsRequired(false);
            entityBuilder.Property(d => d.ResidenceDistrictId).HasColumnName("HomeAreaId").IsRequired(false);
            entityBuilder.Property(d => d.DistrictId).HasColumnName("WorkAreaId").IsRequired(false);
            entityBuilder.Property(d => d.MemberId).HasColumnName("MemberId").IsRequired(false);
            entityBuilder.Property(d => d.DriverTypeId).HasColumnName("TypeId").IsRequired();
            entityBuilder.Property(d => d.IsActive).HasDefaultValue();
            entityBuilder.Property(d => d.IsDeleted).HasDefaultValue();
            entityBuilder.Property(d => d.CreatedOn).IsRequired();
            entityBuilder.Property(d => d.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(d => d.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(d => d.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);

            entityBuilder.HasMany(d => d.Attachments)
                .WithOne(a => a.Driver)
                .HasForeignKey(a => a.DriverId)
                .OnDelete(DeleteBehavior.Restrict); 

            entityBuilder.HasOne(d => d.DriverType)
                .WithMany(t => t.Drivers)
                .HasForeignKey(d => d.DriverTypeId)
                .OnDelete(DeleteBehavior.Restrict); 
            
            entityBuilder.HasOne(d => d.Member)
                .WithMany(t => t.Drivers)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(d => d.District)
                .WithMany(t => t.Drivers)
                .HasForeignKey(d => d.DistrictId);

         }
    }

}
