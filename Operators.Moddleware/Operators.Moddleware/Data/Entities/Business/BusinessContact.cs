
using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Business {

    /// <summary>
    /// Class represents contact person details for Business employer
    /// </summary>
    public class BusinessContact : DomainEntity {

        [Column(Order = 1)]
        public string FirstName { get; set; }

        [Column(Order = 2)]
        public string MiddleName { get; set; }

        [Column(Order = 3)]
        public string LastName { get; set; }

        [Column(Order = 4)]
        public string Email { get; set; }

        [Column(Order = 5)]
        public string PrimaryContact { get; set; }

        [Column(Order = 6)]
        public string SecondaryContact { get; set; }
        
        [Column(Order = 7)]
        public long? EmployerId { get; set; }

        public virtual BusinessEmployer Employer { get; set; }

        public override string ToString() => $"{FirstName} {(!string.IsNullOrWhiteSpace(MiddleName) ? MiddleName : "")} {LastName}";
        public override int GetHashCode() => ToString().GetHashCode()^3;

    }
}
