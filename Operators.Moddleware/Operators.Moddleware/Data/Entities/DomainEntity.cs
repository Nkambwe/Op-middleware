using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities {
    public abstract class DomainEntity {
        [Column(Order = 0)]
        public long Id { get; set; }

        [Column(Order = 50)]
        public bool IsActive { get; set; }

        [Column(Order = 51)]
        public bool IsDeleted { get; set; }
        [Column(Order = 52)]
        public DateTime CreatedOn { get; set; }

        [Column(Order = 53)]
        public string CreatedBy { get; set; }

        [Column(Order = 54)]
        public DateTime? LastModifiedOn { get; set; }

        [Column(Order = 55)]
        public string LastModifiedBy { get; set; }
    }
}
