using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Business {

    public class Reference : DomainEntity {
        
        [Column(Order = 1)]
        public string RefereName { get; set; }

        [Column(Order = 2)]
        public string PhoneNumber { get; set; }
        
        [Column(Order = 3)]
        public string Email { get; set; }
        
        [Column(Order = 4)]
        public string Employment { get; set; }
        
        [Column(Order = 5)]
        public long? DriverId { get; set; }
        
        public virtual Driver Driver { get; set; }

        public override string ToString() => $"{RefereName}";
        public override int GetHashCode() => ToString().GetHashCode()^3;
    }

}
