using System.ComponentModel.DataAnnotations.Schema;

namespace Operators.Moddleware.Data.Entities.Access {
    public class Permission : DomainEntity {
        [Column(Order = 1)]
        public string PermissionName { get; set; }

        [Column(Order = 2)]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }

        public override string ToString() => $"{PermissionName}";
        public override int GetHashCode() => ToString().GetHashCode() ^ 3;
    }
}
