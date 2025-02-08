using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Access {
    public class UserPassword : DomainEntity {
        [Column(Order = 1)]
        public long UserId {get;set; }
        [Column(Order = 2)]
        public string Password {get;set; }
        [Column(Order = 3)]
        public DateTime SetOn {get;set; }

        public virtual User User {get;set;}
        
        public override string ToString() => $"{UserId}-{Password}";
        public override int GetHashCode() => ToString().GetHashCode() ^ 3 ;
    }
}
