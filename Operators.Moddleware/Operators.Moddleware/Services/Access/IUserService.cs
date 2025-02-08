using Operators.Moddleware.Data.Entities.Access;

namespace Operators.Moddleware.Services.Access {
    public interface IUserService {
        Task<User> FindUserByIdAsync(long userId);
        Task<User> FindEmployeeCodeAsync(string employeeNo);
        Task<User> FindUsernameAsync(string username);
        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> UserExistsAsync(string employeeNo);
        Task<bool> UserExistsByNameAsync(string username);
        Task<bool> UserExistsByEmailAsync(string email);
    }
}