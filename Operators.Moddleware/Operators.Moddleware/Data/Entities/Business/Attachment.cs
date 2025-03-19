using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Business {

    /// <summary>
    /// Class holds document and other file attachments to the driver's record like National ID,
    /// Drivering Liciense, Reference letters and others. The type of document is referenced
    /// by rge document type. <see cref="AttarchmentType"/>
    /// </summary>
    public class Attachment : DomainEntity {

        [Column(Order = 1)]
        public long DriverId { get; set; }      
        
        /// <summary>
        /// Document Personalized number. Must be unique Eg.NIN or Employee number
        /// </summary>
        [Column(Order = 2)]
        public string PersonalizedNumber { get; set; }

        /// <summary>
        /// Document identification number. Eg.Card Number
        /// </summary>
        [Column(Order = 3)]
        public string DocumentNumber { get; set; }

        [Column(Order = 4)]
        public DateTime? IssueDate { get; set; }

        [Column(Order = 5)]
        public DateTime? ExpiryDate { get; set; }

        [Column(Order = 6)]
        public bool IsVerified { get; set; }

        [Column(Order = 7)]
        public DateTime? VerifiedOn { get; set; }

        [Column(Order = 8)]
        public string VerifiedBy { get; set; }

        [Column(Order = 9)]
        public long AttachmentTypeId { get; set; }

        [Column(Order = 10)]
        public string DocumentExtension { get; set; }


        public virtual Driver Driver { get; set; }
        public virtual AttarchmentType Type { get; set; }

        public override string ToString() 
            => $"{(!string.IsNullOrWhiteSpace(DocumentNumber) ? DocumentNumber : "00000")}-{AttachmentTypeId}-{PersonalizedNumber}";
        public override int GetHashCode() => ToString().GetHashCode()^3;
    }
}
