using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operators.Moddleware.Data.Entities.Settings;

namespace Operators.Moddleware.Data.EntityConfigurations {

    public class ThemeEntityConfiguration {
        public static void Configure(EntityTypeBuilder<Theme> entityBuilder) {
            entityBuilder.HasKey(m => m.Id);
            entityBuilder.Property(m => m.ThemeName).HasMaxLength(100).IsRequired();
            entityBuilder.Property(m => m.Skin).HasMaxLength(250).HasColumnName("PrimaryColor").IsRequired();
            entityBuilder.Property(m => m.Color).HasMaxLength(250).HasColumnName("SecondaryColor").IsRequired();
            entityBuilder.Property(m => m.FontFamily).HasMaxLength(250).IsRequired();
            entityBuilder.Property(m => m.IsActive).HasDefaultValue(true);
            entityBuilder.Property(m => m.IsDeleted).HasDefaultValue(false);
            entityBuilder.Property(m => m.CreatedOn).IsRequired();
            entityBuilder.Property(m => m.CreatedBy).HasMaxLength(250).IsFixedLength().IsRequired();
            entityBuilder.Property(m => m.LastModifiedOn).IsRequired(false);
            entityBuilder.Property(m => m.LastModifiedBy).HasMaxLength(250).IsFixedLength().IsRequired(false);
            
            //Add uniqueness constraint to ensure one-to-one relationship
            entityBuilder.HasMany(m => m.UserThemes)
                .WithOne(t => t.Theme)
                .HasForeignKey(k => k.UserId);
            
        }
    }

}
