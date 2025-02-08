using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Repositories.access;
using Operators.Moddleware.Exceptions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services.Access {
    public class UserService(IUserRepository repo) : IUserService {
        private readonly IUserRepository _repo = repo;
        private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<User> FindUsernameAsync(string username) {
            var user = await _repo.GetAsync(u => u.Username == username, u => u.Branch, u => u.Role, u => u.Permissions);
            if (user != null) {
                _logger.LogToFile($"USER :: User '{user}' found.", "REPOSITORY");
            } else {
                _logger.LogToFile($"USER :: User with Username no. '{username}' not found.", "REPOSITORY");
            }

            return user;
        }

        public async Task<User> FindEmployeeCodeAsync(string employeeNo) {
            var user = await _repo.GetAsync(u => u.EmployeeNo == employeeNo);
            if (user != null) {
                _logger.LogToFile($"USER :: User '{user}' found.", "REPOSITORY");
            } else {
                _logger.LogToFile($"USER :: User with Employee no. '{employeeNo}' not found.", "REPOSITORY");
            }

            return user;
        }

        public async Task<User> FindUserByIdAsync(long userId) {
            var user = await _repo.GetAsync(userId);
            if (user != null) {
                _logger.LogToFile($"USER :: User '{user}' found.", "REPOSITORY");
            } else {
                _logger.LogToFile($"USER :: User with ID '{userId}' not found.", "REPOSITORY");
            }

            return user;
        }

        public async Task<bool> CreateUserAsync(User user) {
            _logger.LogToFile($"Attepting to create new user", "REPOSITORY");
            var result = await _repo.InsertAsync(user);
            if (result) {
                _logger.LogToFile($"USER :: {user} created successfully", "REPOSITORY");
            } else {
                _logger.LogToFile($"USER :: {user} was not created. An error occurred", "REPOSITORY");
            }
            return result;
        }

        public async Task<bool> UpdateUserAsync(User user) {
            _logger.LogToFile($"Attepting to update user", "REPOSITORY");
            if (!await _repo.ExistsAsync(u => u.Username == user.Username)) {
                _logger.LogToFile($"NOTFOUND :: User '{user}' already exists", "REPOSITORY");
                throw new NotFoundException($"No user with Username '{user}' found");
            }

            var result = await _repo.UpdateAsync(user);
            if (result) {
                _logger.LogToFile($"USER :: {user} updated successfully", "REPOSITORY");
            } else {
                _logger.LogToFile($"USER :: {user} was not updated. An error occurred", "REPOSITORY");
            }
            return result;
        }

        public async Task<bool> UserExistsAsync(string employeeNo) {
            var result = await _repo.ExistsAsync(u => u.EmployeeNo == employeeNo);
            if (result) {
                _logger.LogToFile($"USER :: User with employee number '{employeeNo}' found.", "REPOSITORY");
            } else {
                _logger.LogToFile($"USER :: User with employee number '{employeeNo}' not found.", "REPOSITORY");
            }
            return result;
        }

        public async Task<bool> UserExistsByNameAsync(string username) {
            var result = await _repo.ExistsAsync(u => u.Username == username);
            if (result) {
                _logger.LogToFile($"USER :: User with username '{username}' found.", "REPOSITORY");
            } else {
                _logger.LogToFile($"USER :: User with username '{username}' not found.", "REPOSITORY");
            }
            return result;
        }

        public async Task<bool> UserExistsByEmailAsync(string email) {
            var result = await _repo.ExistsAsync(u => u.EmployeeNo == email);
            if (result) {
                _logger.LogToFile($"USER :: User with email address '{email}' found.", "REPOSITORY");
            } else {
                _logger.LogToFile($"USER :: User with email address '{email}' not found.", "REPOSITORY");
            }
            return result;
        }
    }
}
