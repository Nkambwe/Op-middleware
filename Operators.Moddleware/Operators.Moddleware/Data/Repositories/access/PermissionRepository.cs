using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Exceptions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Data.Repositories.access {

    public class PermissionRepository(OpsDbContext context) : Repository<Permission>(context), IPermissionRepository {
        private readonly OpsDbContext _context = context;
        private readonly ServiceLogger _logger = new("Operations_log");


        /// <summary>
        /// Assign multiple permissions to a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionIds"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException">Throws exception if user record not found</exception>
        public async Task<bool> AssignPermissionsToUserAsync(long userId, List<long> permissionIds) {
            try {
                var user = await _context.Users
                    .Include(u => u.UserPermissions)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null) {
                    _logger.LogToFile("User record not found", "ERROR");
                    throw new NotFoundException("User not found");
                }

                // Get permissions that don't already exist for the user
                var existingPermissionIds = user.UserPermissions
                    .Select(up => up.PermissionId)
                    .ToList();

                var newPermissionIds = permissionIds
                    .Except(existingPermissionIds)
                    .ToList();

                // Add new permissions
                foreach (var permissionId in newPermissionIds) {
                    user.UserPermissions.Add(new UserPermission {
                        UserId = userId,
                        PermissionId = permissionId
                    });
                }

                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex) {
                _logger.LogToFile($"Error occurred while assigning permission to user : {ex.Message}", "ERROR");
                _logger.LogToFile($"{ex.StackTrace}", "STACKTRACE");
                return false;
            }
        }

        /// <summary>
        /// Remove specific permissions from a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionIds"></param>
        /// <returns></returns>
        public async Task<bool> RemovePermissionsFromUserAsync(long userId, List<long> permissionIds) {
            try {
                var userPermissions = await _context.Set<UserPermission>()
                    .Where(up => up.UserId == userId && permissionIds.Contains(up.PermissionId))
                    .ToListAsync();

                if (userPermissions.Any()) {
                    _context.Set<UserPermission>().RemoveRange(userPermissions);
                    await _context.SaveChangesAsync();
                }

                return true;
            } catch (Exception ex) {
                _logger.LogToFile($"Error occurred while removing permission from user : {ex.Message}", "ERROR");
                _logger.LogToFile($"{ex.StackTrace}", "STACKTRACE");
                return false;
            }
        }

        /// <summary>
        /// Replace all user permissions with new ones
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPermissionIds"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserPermissionsAsync(long userId, List<long> newPermissionIds) {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try {
                // Remove all existing permissions
                var existingPermissions = await _context.Set<UserPermission>()
                    .Where(up => up.UserId == userId)
                    .ToListAsync();

                _context.Set<UserPermission>().RemoveRange(existingPermissions);

                // Add new permissions
                var newPermissions = newPermissionIds.Select(permissionId => new UserPermission {
                    UserId = userId,
                    PermissionId = permissionId
                });

                await _context.Set<UserPermission>().AddRangeAsync(newPermissions);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            } catch (Exception ex) {
                await transaction.RollbackAsync();
                _logger.LogToFile($"Error occurred while updating permissions : {ex.Message}", "ERROR");
                _logger.LogToFile($"{ex.StackTrace}", "STACKTRACE");
                return false;
            }
        }

        /// <summary>
        /// Check if user has specific permission
        /// </summary>
        /// <param name="userId">User ID to look for</param>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public async Task<bool> HasPermissionAsync(long userId, long permissionId) {
            return await _context.Set<UserPermission>()
                .AnyAsync(up => up.UserId == userId && up.PermissionId == permissionId);
        }

        /// <summary>
        /// Get all permissions for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Permission>> GetUserPermissionsAsync(long userId) {
            return await _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Permissions)
                .ToListAsync();
        }
    }

}
