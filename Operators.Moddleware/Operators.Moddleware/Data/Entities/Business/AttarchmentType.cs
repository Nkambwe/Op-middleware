using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Business {

    public class AttarchmentType : DomainEntity {

        [Column(Order = 1)]
        public string TypeName { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
