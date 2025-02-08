using Operators.Moddleware.Data.Entities.Access;

namespace Operators.Moddleware.Data.Repositories.access {
    public interface IPermissionRepository : IRepository<Permission> {
        Task<bool> AssignPermissionsToUserAsync(long userId, List<long> permissionIds);
        Task<bool> RemovePermissionsFromUserAsync(long userId, List<long> permissionIds);
        Task<bool> UpdateUserPermissionsAsync(long userId, List<long> newPermissionIds);
        Task<bool> HasPermissionAsync(long userId, long permissionId);
        Task<List<Permission>> GetUserPermissionsAsync(long userId);
    }

}
