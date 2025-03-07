using Operators.Moddleware.Data.Entities.Business;
using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities {

    public class District : DomainEntity {

        [Column(Order = 1)]
        public string DistrictName { get; set; }

        public virtual ICollection<IndividualEmployer> Individuals { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }

        public override string ToString() => $"{DistrictName}";
        public override int GetHashCode() => ToString().GetHashCode()^3;

    }
}
