
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities.Business;

namespace Operators.Moddleware.Data.EntityConfigurations {

    public class BusinessEmployerEntityConfiguration {

         public static void Configure(EntityTypeBuilder<BusinessEmployer> entityBuilder) {
            entityBuilder.HasKey(i => i.Id);
            entityBuilder.Property(i => i.BusinessName).HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(i => i.Address).HasMaxLength(200).HasColumnName("PhysicalAddress").IsRequired(false);
            entityBuilder.Property(i => i.City).HasColumnName("City").HasMaxLength(100).IsRequired(false);
            entityBuilder.Property(i => i.BusinessNumber).HasColumnName("Phone2").HasMaxLength(20).IsFixedLength().IsRequired(false);
            entityBuilder.Property(i => i.EmployerType).HasColumnName("Type").IsRequired();
            entityBuilder.Property(i => i.ContactId).HasColumnName("ContactId").IsRequired(false);
            entityBuilder.Property(i => i.IsActive).HasDefaultValue(true);
            entityBuilder.Property(i => i.IsDeleted).HasDefaultValue(false);
            entityBuilder.Property(i => i.CreatedOn).IsRequired();
            entityBuilder.Property(i => i.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(i => i.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(i => i.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);

            entityBuilder.HasOne(d => d.Industry)
                .WithMany(t => t.Businesses)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Restrict); 

            entityBuilder.HasOne(b => b.Contact)
                .WithOne(t => t.Employer)
                .HasForeignKey<BusinessContact>(t => t.EmployerId);
            
         }
    }

}
