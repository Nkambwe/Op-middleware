using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Exceptions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services.Access {

    public class UserService(IUnitOfWorkFactory uowf) : IUserService {
        private readonly IUnitOfWorkFactory _uowf = uowf;
        private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<User> FindUsernameAsync(string username, bool includeDeleted) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<User>();
            var user = await _repo.GetAsync(u => u.Username == username,includeDeleted , u => u.Branch, 
                u => u.Role, u => u.Permissions, u => u.Passwords, u=> u.Theme);
            if (user != null) {
                _logger.LogToFile($"RESULT : User '{user}' found.", "USERS");
            } else {
                _logger.LogToFile($"RESULT : User with Username no. '{username}' not found.", "USERS");
            }

            return user;
        }

        public async Task<User> FindEmployeeCodeAsync(string employeeNo, bool includeDeleted) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<User>();
            var user = await _repo.GetAsync(u => u.EmployeeNo == employeeNo, includeDeleted);
            if (user != null) {
                _logger.LogToFile($"RESULT : User '{user}' found.", "USERS");
            } else {
                _logger.LogToFile($"RESULT : User with Employee no. '{employeeNo}' not found.", "USERS");
            }

            return user;
        }

        public async Task<User> FindUserByIdAsync(long userId, bool includeDeleted) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<User>();
            var user = await _repo.GetAsync(userId, includeDeleted);
            if (user != null) {
                _logger.LogToFile($"RESULT : User '{user}' found.", "USERS");
            } else {
                _logger.LogToFile($"RESULT : User with ID '{userId}' not found.", "USERS");
            }

            return user;
        }

        public async Task<bool> CreateUserAsync(User user) {
            _logger.LogToFile($"Attepting to create new user", "USERS");
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<User>();
            var result = await _repo.InsertAsync(user);
            if (result) {
                _logger.LogToFile($"RESULT : {user} created successfully", "USERS");
            } else {
                _logger.LogToFile($"RESULT : {user} was not created. An error occurred", "USERS");
            }
            return result;
        }

        public async Task<bool> UpdateUserAsync(User user, bool includeDeleted) {
            _logger.LogToFile($"Attepting to update user", "USERS");

            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<User>();
            if (!await _repo.ExistsAsync(u => u.Username == user.Username, includeDeleted)) {
                _logger.LogToFile($"NOTFOUND : User with username '{user.Username}' does not exists", "USERS");
                throw new NotFoundException($"No user with Username '{user}' found");
            }

            var result = await _repo.UpdateAsync(user, includeDeleted);
            if (result) {
                _logger.LogToFile($"RESULT : {user} updated successfully", "USERS");
            } else {
                _logger.LogToFile($"RESULT : {user} was not updated. An error occurred", "USERS");
            }
            return result;
        }

        public async Task<bool> UserExistsAsync(string employeeNo, bool includeDeleted) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<User>();
            var result = await _repo.ExistsAsync(u => u.EmployeeNo == employeeNo, includeDeleted);
            if (result) {
                _logger.LogToFile($"RESULT : User with employee number '{employeeNo}' found.", "USERS");
            } else {
                _logger.LogToFile($"RESULT : User with employee number '{employeeNo}' not found.", "USERS");
            }
            return result;
        }

        public async Task<bool> UserExistsByNameAsync(string username, bool includeDeleted) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<User>();
            var result = await _repo.ExistsAsync(u => u.Username == username, includeDeleted);
            if (result) {
                _logger.LogToFile($"RESULT : User with username '{username}' found.", "USERS");
            } else {
                _logger.LogToFile($"RESULT : User with username '{username}' not found.", "USERS");
            }
            return result;
        }

        public async Task<bool> UserExistsByEmailAsync(string email, bool includeDeleted) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<User>();
            var result = await _repo.ExistsAsync(u => u.EmployeeNo == email, includeDeleted);
            if (result) {
                _logger.LogToFile($"RESULT : User with email address '{email}' found.", "USERS");
            } else {
                _logger.LogToFile($"RESULT : User with email address '{email}' not found.", "USERS");
            }
            return result;
        }
    }
}
