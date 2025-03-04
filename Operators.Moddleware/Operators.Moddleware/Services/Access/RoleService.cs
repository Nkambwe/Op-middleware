using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Exceptions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services.Access {

    public class RoleService(IUnitOfWorkFactory uowf) : IRoleService {

        private readonly IUnitOfWorkFactory _uowf = uowf;
        private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<Role> FindRoleByIdAsync(long roleId) {

            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Role>();
            var role = await _repo.GetAsync(roleId);
            if (role != null) {
                _logger.LogToFile($"RESULT : Role '{role}' found.", "ROLES");
            } else {
                _logger.LogToFile($"RESULT : Role with ID '{roleId}' not found.", "ROLES");
            }

            return role;
        }

        public async Task<bool> CreateRoleAsync(Role role) {
            _logger.LogToFile($"Attepting to create new role", "ROLES");

            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Role>();
            if (!await _repo.ExistsAsync(r => r.RoleName == role.RoleName)) {
                var result = await _repo.InsertAsync(role);
                if (result) {
                    _logger.LogToFile($"RESULT : {role} created successfully", "ROLES");
                } else {
                    _logger.LogToFile($"RESULT : {role} was not created. An error occurred", "ROLES");
                }
                return result;
            } else {
                _logger.LogToFile($"DUPLICATION :: Role '{role}' already exists", "ROLES");
                throw new DuplicateException($"Another role with role name '{role}' exists");
            }

        }

        public async Task<bool> UpdateRoleAsync(Role role) {
            _logger.LogToFile($"Attepting to update role", "ROLES");

            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Role>();
            if (!await _repo.ExistsAsync(r => r.RoleName == role.RoleName)) {
                _logger.LogToFile($"NOTFOUND :: Role '{role}' already exists", "ROLES");
                throw new NotFoundException($"No role with role name '{role}' found");
            }

            var result = await _repo.UpdateAsync(role);
            if (result) {
                _logger.LogToFile($"RESULT : {role} updated successfully", "ROLES");
            } else {
                _logger.LogToFile($"RESULT : {role} was not updated. An error occurred", "ROLES");
            }
            return result;
        }

        public async Task<bool> DeleteRoleAsync(long roleId) {

            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Role>();
            var role = await _repo.GetAsync(roleId);
            if (role != null) {
                return await _repo.DeleteAsync(role);
            } else {
                throw new NotFoundException($"No role with role ID '{roleId}' found");
            }
        }

        public async Task<bool> RoleExistsAsync(string roleName) {

            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Role>();
            var result = await _repo.ExistsAsync(r => r.RoleName == roleName);
            if (result) {
                _logger.LogToFile($"RESULT : Role with name '{roleName}' found.", "ROLES");
            } else {
                _logger.LogToFile($"RESULT : Role with name '{roleName}' not found.", "ROLES");
            }
            return result;
        }
    }
}
