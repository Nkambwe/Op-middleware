using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operators.Moddleware.Data.Entities.Business;

namespace Operators.Moddleware.Data.EntityConfigurations {

    public class MemberEntityConfiguration {

        public static void Configure(EntityTypeBuilder<Member> entityBuilder) {
            entityBuilder.HasKey(m => m.Id);
            entityBuilder.Property(m => m.FirstName).HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(m => m.MiddleName).HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(m => m.LastName).HasMaxLength(200).IsRequired(false);
            entityBuilder.Property(m => m.Email).HasMaxLength(200).HasColumnName("EmailAddress").IsRequired(false);
            entityBuilder.Property(m => m.PrimaryContact).HasColumnName("Phone1").HasMaxLength(20).IsFixedLength().IsRequired(false);
            entityBuilder.Property(m => m.SecondaryContact).HasColumnName("Phone2").HasMaxLength(20).IsFixedLength().IsRequired(false);
            entityBuilder.Property(m => m.Address).HasColumnName("PhysicalAddress").IsRequired(false);
            entityBuilder.Property(m => m.IsActive).HasDefaultValue(true);
            entityBuilder.Property(m => m.IsDeleted).HasDefaultValue(false);
            entityBuilder.Property(m => m.CreatedOn).IsRequired();
            entityBuilder.Property(m => m.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(m => m.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(m => m.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);
            
            entityBuilder.HasMany(m => m.Drivers)
                .WithOne(d => d.Member)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }

}
