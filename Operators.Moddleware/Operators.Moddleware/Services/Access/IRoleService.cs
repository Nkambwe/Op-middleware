using Operators.Moddleware.Data.Entities.Access;

namespace Operators.Moddleware.Services.Access {
    public interface IRoleService {
        Task<Role> FindRoleByIdAsync(long roleId);
        Task<bool> CreateRoleAsync(Role role);
        Task<bool> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(long roleId);
        Task<bool> RoleExistsAsync(string roleName);
    }
}