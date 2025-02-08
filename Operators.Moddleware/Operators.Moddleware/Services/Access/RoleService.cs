using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Repositories.access;
using Operators.Moddleware.Exceptions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services.Access {

    public class RoleService(IRoleRepository repo) : IRoleService {
        private readonly IRoleRepository _repo = repo;
        private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<Role> FindRoleByIdAsync(long roleId) {
            var role = await _repo.GetAsync(roleId);
            if (role != null) {
                _logger.LogToFile($"ROLE :: Role '{role}' found.", "REPOSITORY");
            } else {
                _logger.LogToFile($"ROLE :: Role with ID '{roleId}' not found.", "REPOSITORY");
            }

            return role;
        }

        public async Task<bool> CreateRoleAsync(Role role) {
            _logger.LogToFile($"Attepting to create new role", "REPOSITORY");
            if (!await _repo.ExistsAsync(r => r.RoleName == role.RoleName)) {
                var result = await _repo.InsertAsync(role);
                if (result) {
                    _logger.LogToFile($"ROLE :: {role} created successfully", "REPOSITORY");
                } else {
                    _logger.LogToFile($"ROLE :: {role} was not created. An error occurred", "REPOSITORY");
                }
                return result;
            } else {
                _logger.LogToFile($"DUPLICATION :: Role '{role}' already exists", "REPOSITORY");
                throw new DuplicateException($"Another role with role name '{role}' exists");
            }

        }

        public async Task<bool> UpdateRoleAsync(Role role) {
            _logger.LogToFile($"Attepting to update role", "REPOSITORY");
            if (!await _repo.ExistsAsync(r => r.RoleName == role.RoleName)) {
                _logger.LogToFile($"NOTFOUND :: Role '{role}' already exists", "REPOSITORY");
                throw new NotFoundException($"No role with role name '{role}' found");
            }

            var result = await _repo.UpdateAsync(role);
            if (result) {
                _logger.LogToFile($"ROLE :: {role} updated successfully", "REPOSITORY");
            } else {
                _logger.LogToFile($"ROLE :: {role} was not updated. An error occurred", "REPOSITORY");
            }
            return result;
        }

        public async Task<bool> DeleteRoleAsync(long roleId) {
            var role = await _repo.GetAsync(roleId);
            if (role != null) {
                return await _repo.DeleteAsync(role);
            } else {
                throw new NotFoundException($"No role with role ID '{roleId}' found");
            }
        }

        public async Task<bool> RoleExistsAsync(string roleName) {
            var result = await _repo.ExistsAsync(r => r.RoleName == roleName);
            if (result) {
                _logger.LogToFile($"ROLE :: Role with name '{roleName}' found.", "REPOSITORY");
            } else {
                _logger.LogToFile($"ROLE :: Role with name '{roleName}' not found.", "REPOSITORY");
            }
            return result;
        }
    }
}
