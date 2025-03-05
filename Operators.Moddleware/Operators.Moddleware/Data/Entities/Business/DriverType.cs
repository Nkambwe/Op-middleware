using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Business {

    /// <summary>
    /// Class represents type of drivers
    /// </summary>
    public class DriverType : DomainEntity {
        
        [Column(Order = 1)]
        public string TypeName { get; set; }

        public virtual ICollection<Driver> Drivers { get; set; }

        public override string ToString() => $"{TypeName}";
        public override int GetHashCode() => ToString().GetHashCode()^3;

    }
}
