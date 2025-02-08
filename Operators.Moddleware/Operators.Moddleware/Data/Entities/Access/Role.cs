using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Access {

    public class Role : DomainEntity {
        [Column(Order = 1)]
        public string RoleName { get; set; }
        [Column(Order = 2)]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public override string ToString() => $"{RoleName}";
        public override int GetHashCode() => ToString().GetHashCode() ^ 3;
    }
}
