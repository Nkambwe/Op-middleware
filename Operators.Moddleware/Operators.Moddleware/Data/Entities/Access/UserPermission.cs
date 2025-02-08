namespace Operators.Moddleware.Data.Entities.Access {
    public class UserPermission {
        public long UserId { get; set; }
        public User User { get; set; }

        public long PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
