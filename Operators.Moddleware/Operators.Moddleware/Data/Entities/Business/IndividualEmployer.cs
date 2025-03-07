using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Business {

    public class IndividualEmployer : DomainEntity {
        
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
        public EmployerType EmployerType { get; set; }
        
        [Column(Order = 8)]
        public string ResidenceArea { get; set; }

        [Column(Order = 9)]
        public long? ResidenceDistrictId { get; set; }

        [Column(Order = 10)]
        public long? DistrictId { get; set; }
        
        [Column(Order = 11)]
        public string Facebook { get; set; }

        [Column(Order = 12)]
        public string WhatsApp { get; set; }

        [Column(Order = 13)]
        public string Tweeter { get; set; }
        
        [Column(Order = 14)]
        public long? BusinessContactId { get; set; }

        public virtual District District { get; set; }
        public virtual BusinessIndustry Industry {get;set;}

         public override string ToString() 
            => $"{FirstName} {(!string.IsNullOrWhiteSpace(MiddleName) ? MiddleName : "")}{LastName}";
        public override int GetHashCode() => ToString().GetHashCode()^3;
        
    }
}
