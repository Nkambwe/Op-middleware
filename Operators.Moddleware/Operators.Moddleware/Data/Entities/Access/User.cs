using Operators.Moddleware.Data.Entities.Settings;
using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Access {

    public class User : DomainEntity {
        [Column(Order = 1)]
        public long RoleId { get; set; }

        [Column(Order = 2)]
        public long BranchId { get; set; }

        [Column(Order = 3)]
        public string EmployeeNo { get; set; }

        [Column(Order = 4)]
        public string Username { get; set; }

        [Column(Order = 5)]
        public string EmployeeName { get; set; }

        [Column(Order = 6)]
        public string EmailAddress { get; set; }

        [Column(Order = 7)]
        public long CurrentPassword { get; set; }

        [Column(Order = 8)]
        public bool IsVerified { get; set; }

        [Column(Order = 9)]
        public bool IsLoggedin { get; set; }

        [Column(Order = 10)]
        public long ThemeId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual UserTheme Theme { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
        public ICollection<UserPassword> Passwords { get; set; }

        public override string ToString() => $"{EmployeeNo}-{Username}";
        public override int GetHashCode() => ToString().GetHashCode() ^ 3;
    }
}
