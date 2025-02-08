using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Repositories.access;
using Operators.Moddleware.Exceptions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services.Access {
    public class PermissionService(IPermissionRepository repo) : IPermissionService {

        private readonly IPermissionRepository _repo = repo;
        private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<bool> AssignPermissionsToUserAsync(long userId, List<long> permissionIds) {

            try {
                _logger.LogToFile($"Attepting to assign permissions to user", "REPOSITORY");
                var result = await _repo.AssignPermissionsToUserAsync(userId, permissionIds);
                if (result) {
                    _logger.LogToFile($"PERMISSIONS :: {permissionIds.Count} permissions granted to user", "REPOSITORY");
                } else {
                    _logger.LogToFile($"PERMISSIONS :: An error occurred while attempting to assign permissions to user", "REPOSITORY");
                }
                return result;
            } catch (NotFoundException) {
                throw;
            }

        }

        public async Task<List<Permission>> GetUserPermissionsAsync(long userId) {

            try {
                _logger.LogToFile($"Retrieving user permissions", "REPOSITORY");
                return await _repo.GetUserPermissionsAsync(userId);
            } catch (Exception) {
                _logger.LogToFile($"PERMISSIONS :: An error occurred while retrieving user permissions", "REPOSITORY");
                throw;
            }
        }

        public async Task<bool> HasPermissionAsync(long userId, long permissionId) {

            try {
                _logger.LogToFile($"Checking if user '{userId}' has permission with ID '{permissionId}'", "REPOSITORY");
                var result = await _repo.HasPermissionAsync(userId, permissionId);
                if (result) {
                    _logger.LogToFile($"PERMISSIONS :: User has permission ID '{permissionId}'", "REPOSITORY");
                } else {
                    _logger.LogToFile($"PERMISSIONS :: USer has no permission with ID '{permissionId}'", "REPOSITORY");
                }
                return result;
            } catch (Exception) {
                _logger.LogToFile($"PERMISSIONS :: An error occurred while retrieving user permissions", "REPOSITORY");
                throw;
            }
        }

        public async Task<bool> RemovePermissionsFromUserAsync(long userId, List<long> permissionIds) {
            _logger.LogToFile($"Attepting to remove permissions from user", "REPOSITORY");
            var result = await _repo.RemovePermissionsFromUserAsync(userId, permissionIds);
            if (result) {
                _logger.LogToFile($"PERMISSIONS :: {permissionIds.Count} permissions granted to user", "REPOSITORY");
            } else {
                _logger.LogToFile($"PERMISSIONS :: An error occurred while attempting to assign permissions to user", "REPOSITORY");
            }
            return result;
        }

        public async Task<bool> UpdateUserPermissionsAsync(long userId, List<long> newPermissionIds) {

            try {
                _logger.LogToFile($"Updating user '{userId}' permissions", "REPOSITORY");
                var result = await _repo.UpdateUserPermissionsAsync(userId, newPermissionIds);
                if (result) {
                    _logger.LogToFile($"PERMISSIONS :: User '{userId}' permissions have been updated", "REPOSITORY");
                } else {
                    _logger.LogToFile($"PERMISSIONS :: Attempt to update user '{userId}' permissions failed", "REPOSITORY");
                }
                return result;
            } catch (Exception) {
                _logger.LogToFile($"PERMISSIONS :: An error occurred while attempting to update user permissions", "REPOSITORY");
                throw;
            }
        }
    }
}
