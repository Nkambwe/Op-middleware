
using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Business {

    public class BusinessIndustry: DomainEntity {

        [Column(Order = 1)]
        public string IndustryName { get; set; }

        public virtual ICollection<IndividualEmployer> Individuals { get; set; }
        public virtual ICollection<BusinessEmployer> Businesses { get; set; }

        public override string ToString() => $"{IndustryName}";
        public override int GetHashCode() => ToString().GetHashCode()^3;

    }
}
