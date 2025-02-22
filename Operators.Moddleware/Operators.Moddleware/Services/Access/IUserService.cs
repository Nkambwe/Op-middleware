using Operators.Moddleware.Data.Entities.Access;

namespace Operators.Moddleware.Services.Access {
    public interface IUserService {
        Task<User> FindUserByIdAsync(long userId, bool includeDeleted);
        Task<User> FindEmployeeCodeAsync(string employeeNo, bool includeDeleted);
        Task<User> FindUsernameAsync(string username, bool includeDeleted);
        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user, bool includeDeleted);
        Task<bool> UserExistsAsync(string employeeNo, bool includeDeleted);
        Task<bool> UserExistsByNameAsync(string username, bool includeDeleted);
        Task<bool> UserExistsByEmailAsync(string email, bool includeDeleted);
    }
}