namespace Operators.Moddleware.Data.Entities.Access {
    public class RolePermission {
        public long RoleId { get; set; }
        public Role Role { get; set; }

        public long PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
