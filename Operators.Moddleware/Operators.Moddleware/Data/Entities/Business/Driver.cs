using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Business {

    /// <summary>
    /// Class represents the driver instance
    /// </summary>
    public class Driver : DomainEntity {

        [Column(Order = 1)]
        public string FirstName { get; set; }

        [Column(Order = 2)]
        public string MiddleName { get; set; }

        [Column(Order = 3)]
        public string LastName { get; set; }

        [Column(Order = 4)]
        public DateTime? DateOfBirth { get; set; }

        [Column(Order = 5)]
        public long? ResidenceDistrictId { get; set; }

        [Column(Order = 6)]
        public long? DistrictId { get; set; }
        
        [Column(Order = 13)]
        public string ResidenceArea { get; set; }

        [Column(Order = 7)]
        public string Email { get; set; }

        [Column(Order = 8)]
        public string PrimaryContact { get; set; }

        [Column(Order = 9)]
        public string SecondaryContact { get; set; }
        [Column(Order = 10)]
        public int? YearsOfExperience {get;set; }

        [Column(Order = 11)]
        public string Facebook { get; set; }

        [Column(Order = 12)]
        public string WhatsApp { get; set; }

        [Column(Order = 14)]
        public string Tweeter { get; set; }
        
        [Column(Order = 15)]
        public long DriverTypeId { get; set; }
        
        [Column(Order = 16)]
        public long? MemberId { get; set; }

        public virtual DriverType DriverType {get;set;}
        public virtual District District { get; set; }
        public virtual Member Member { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        
        public virtual ICollection<Reference> References { get; set; }

        public override string ToString() {
            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(MiddleName) && !string.IsNullOrWhiteSpace(LastName)) {
                return $"{FirstName.Trim()} {MiddleName.Trim()} {LastName.Trim()}";
            }
    
            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(MiddleName)) {
                return $"{FirstName.Trim()} {MiddleName.Trim()}";
            }
    
            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName)) {
                return $"{FirstName.Trim()} {LastName.Trim()}";
            }
    
            if (!string.IsNullOrWhiteSpace(FirstName)) {
                return FirstName.Trim();
            }
    
            if (!string.IsNullOrWhiteSpace(MiddleName) && !string.IsNullOrWhiteSpace(LastName)) {
                return $"{MiddleName.Trim()} {LastName.Trim()}";
            }
    
            if (!string.IsNullOrWhiteSpace(MiddleName)) {
                return MiddleName.Trim();
            }
    
            if (!string.IsNullOrWhiteSpace(LastName)) {
                return LastName.Trim();
            }
    
            return string.Empty;
        }

        public override int GetHashCode() => ToString().GetHashCode()^3;

    }

}
