
using Operators.Moddleware.Data.Entities.Access;

namespace Operators.Moddleware.Services.Access {
    public interface IPasswordService {
        Task<UserPassword> GetPasswordAsync(long currentPassword);
    }
}