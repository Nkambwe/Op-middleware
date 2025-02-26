using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Entities.Settings;
using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities {
    public class Branch: DomainEntity {
        [Column(Order = 1)]
        public string BranchCode { get; set; }

        [Column(Order = 2)]
        public string BranchName { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ConfigurationParameter> Parameters { get; set; }

        public override string ToString() => $"{BranchCode}-{BranchName}";
        public override int GetHashCode() => ToString().GetHashCode() ^ 3 ;
    }
}
