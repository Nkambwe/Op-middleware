
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities.Business;

namespace Operators.Moddleware.Data.EntityConfigurations {

    public class BusinessContactEntityConfiguration {
        
         public static void Configure(EntityTypeBuilder<BusinessContact> entityBuilder) {
            entityBuilder.HasKey(b => b.Id);
            entityBuilder.Property(b => b.FirstName).HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(b => b.MiddleName).HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(b => b.LastName).HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(b => b.Email).HasMaxLength(200).HasColumnName("EmailAddress").IsRequired(false);
            entityBuilder.Property(b => b.PrimaryContact).HasColumnName("Phone1").HasMaxLength(20).IsFixedLength().IsRequired(false);
            entityBuilder.Property(b => b.SecondaryContact).HasColumnName("Phone2").HasMaxLength(20).IsFixedLength().IsRequired(false);
            entityBuilder.Property(b => b.EmployerId).HasColumnName("EmployerId").IsRequired();
            entityBuilder.Property(b => b.IsActive).HasDefaultValue(false);
            entityBuilder.Property(b => b.IsDeleted).HasDefaultValue(false);
            entityBuilder.Property(b => b.CreatedOn).IsRequired();
            entityBuilder.Property(b => b.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(b => b.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(b => b.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);

            entityBuilder.HasOne(b => b.Employer)
                .WithOne(t => t.Contact)
                .HasForeignKey<BusinessEmployer>(t => t.ContactId);
            
         }
    }

}
