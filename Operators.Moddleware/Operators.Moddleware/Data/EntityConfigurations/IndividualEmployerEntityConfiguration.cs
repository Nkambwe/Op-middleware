
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities.Business;

namespace Operators.Moddleware.Data.EntityConfigurations {

    public class IndividualEmployerEntityConfiguration {
        
         public static void Configure(EntityTypeBuilder<IndividualEmployer> entityBuilder) {
            entityBuilder.HasKey(i => i.Id);
            entityBuilder.Property(i => i.FirstName).HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(i => i.MiddleName).HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(i => i.LastName).HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(i => i.Email).HasMaxLength(200).HasColumnName("EmailAddress").IsRequired(false);
            entityBuilder.Property(i => i.PrimaryContact).HasColumnName("Phone1").HasMaxLength(20).IsFixedLength().IsRequired(false);
            entityBuilder.Property(i => i.SecondaryContact).HasColumnName("Phone2").HasMaxLength(20).IsFixedLength().IsRequired(false);
            entityBuilder.Property(i => i.ResidenceArea).HasColumnName("PhysicalAddress").IsRequired(false);
            entityBuilder.Property(i => i.EmployerType).HasColumnName("Type").IsRequired(false);
            entityBuilder.Property(i => i.Facebook).HasColumnName("Fbk").HasMaxLength(400).IsRequired(false);
            entityBuilder.Property(i => i.WhatsApp).HasColumnName("Wap").HasMaxLength(400).IsRequired(false);
            entityBuilder.Property(i => i.Tweeter).HasColumnName("Twr").HasMaxLength(400).IsRequired(false);
            entityBuilder.Property(i => i.ResidenceDistrictId).HasColumnName("HomeAreaId").IsRequired(false);
            entityBuilder.Property(i => i.DistrictId).HasColumnName("WorkAreaId").IsRequired(false);
            entityBuilder.Property(i => i.BusinessContactId).HasColumnName("ContactId").IsRequired(false);
            entityBuilder.Property(i => i.IsActive).HasDefaultValue();
            entityBuilder.Property(i => i.IsDeleted).HasDefaultValue();
            entityBuilder.Property(i => i.CreatedOn).IsRequired();
            entityBuilder.Property(i => i.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(i => i.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(i => i.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);

            entityBuilder.HasOne(d => d.District)
                .WithMany(a => a.Individuals)
                .HasForeignKey(a => a.DistrictId)
                .OnDelete(DeleteBehavior.Restrict); 

            entityBuilder.HasOne(d => d.Industry)
                .WithMany(t => t.Individuals)
                .HasForeignKey(d => d.BusinessContactId)
                .OnDelete(DeleteBehavior.Restrict); 
            
         }
    }

}
