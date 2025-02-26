using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Entities.Settings;

namespace Operators.Moddleware.Data.EntityConfigurations {

    public class UserThemeEntityConfiguration {

        public static void Configure(EntityTypeBuilder<UserTheme> entityBuilder) {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.UserId);
            entityBuilder.Property(t => t.ThemeId);

            entityBuilder.HasOne(t => t.User)
                .WithOne(u => u.Theme).HasForeignKey<User>(t => t.ThemeId);

            entityBuilder.HasOne(u => u.Theme)
                .WithMany(t => t.UserThemes);
        }

    }
}
