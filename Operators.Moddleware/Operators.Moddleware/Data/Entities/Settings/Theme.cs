using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Settings {

    public class Theme : DomainEntity {

        [Column(Order = 2)]
        public string ThemeName { get; set; }

        [Column(Order = 3)]
        public string Skin { get; set; }

        [Column(Order = 4)]
        public string Color { get; set; }

        [Column(Order = 5)]
        public string FontFamily { get; set; }

        public virtual ICollection<UserTheme> UserThemes { get; set; }
    }
}
