
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities.Business;

namespace Operators.Moddleware.Data.EntityConfigurations {

    public class AttachmentEntityConfiguration {
        
        public static void Configure(EntityTypeBuilder<Attachment> entityBuilder) {
            entityBuilder.HasKey(a => a.Id);
            entityBuilder.Property(a => a.PersonalizedNumber).HasColumnName("Passnum").HasMaxLength(30).IsFixedLength().IsRequired(false);
            entityBuilder.Property(a => a.DocumentNumber).HasColumnName("Docnum").HasMaxLength(30).IsFixedLength().IsRequired(false);
            entityBuilder.Property(a => a.IssueDate).HasColumnName("Issued").IsRequired(false);
            entityBuilder.Property(a => a.ExpiryDate).HasColumnName("Expiry").IsFixedLength().IsRequired(false);
            entityBuilder.Property(a => a.IsVerified).HasColumnName("Verified").IsRequired();
            entityBuilder.Property(a => a.VerifiedOn).IsFixedLength().IsRequired(false);
            entityBuilder.Property(a => a.VerifiedBy).HasColumnName("Verifier").HasMaxLength(250).IsFixedLength().IsRequired(false);
            entityBuilder.Property(a => a.DocumentExtension).HasColumnName("Extension").IsRequired(false);
            entityBuilder.Property(d => d.DriverId).HasColumnName("Driver").IsRequired();
            entityBuilder.Property(d => d.AttachmentTypeId).HasColumnName("Type").IsRequired();
            entityBuilder.Property(a => a.IsActive).HasDefaultValue();
            entityBuilder.Property(a => a.IsDeleted).HasDefaultValue();
            entityBuilder.Property(a => a.CreatedOn).IsRequired();
            entityBuilder.Property(a => a.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(a => a.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(a => a.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);
            
            entityBuilder.HasOne(a => a.Driver)
                .WithMany(d => d.Attachments)
                .HasForeignKey(a => a.DriverId)
                .OnDelete(DeleteBehavior.Restrict); 
            
            entityBuilder.HasOne(a => a.Type)
                .WithMany(d => d.Attachments)
                .HasForeignKey(a => a.AttachmentTypeId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }

}
