using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Business {

    /// <summary>
    /// Class represents a business or company employer for the driver
    /// </summary>
    public class BusinessEmployer : DomainEntity {

        [Column(Order = 1)]
        public string BusinessName { get; set; }

        [Column(Order = 2)]
        public string Address { get; set; }

        [Column(Order = 3)]
        public string City { get; set; }

        [Column(Order = 4)]
        public string BusinessNumber { get; set; }

        [Column(Order = 5)]
        public EmployerType EmployerType { get; set; }

        [Column(Order = 6)]
        public long? BusinessContactId { get; set; }

        public virtual BusinessIndustry Industry {get;set;}

        public override string ToString() => $"{BusinessName}";
        public override int GetHashCode() => ToString().GetHashCode()^3;
    }
}
