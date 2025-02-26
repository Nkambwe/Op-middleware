using Operators.Moddleware.Data.Entities.Access;
using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Settings {
    public class UserTheme : DomainEntity {

        [Column(Order = 1)]
        public long UserId { get; set; }
        
        [Column(Order = 2)]
        public long ThemeId { get; set; }

        public virtual User User { get; set; }
        public virtual Theme Theme { get; set; }
        
    }
}
