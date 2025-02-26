using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Settings {

    public class ConfigurationParameter : DomainEntity {

        [Column(Order = 1)]
        public long BranchId { get; set; }

        [Column(Order = 2)]
        public string Parameter { get; set; }
        [Column(Order = 3)]
        public string ParameterValue { get; set; }

        [Column(Order = 4)]
        public string Identifier { get; set; }

        public virtual Branch Branch { get; set; }

        public override string ToString() => $"{Parameter}";
        public override int GetHashCode() => ToString().GetHashCode() ^ 3;
    }

}
