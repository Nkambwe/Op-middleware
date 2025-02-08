using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Repositories.access;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services.Access {
    public class PasswordService (IPasswordsRepository repo) : IPasswordService {
        private readonly IPasswordsRepository _repo = repo;
        private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<UserPassword> GetPasswordAsync(long currentPassword) {
            var password = await _repo.GetAsync(p => p.Id == currentPassword);
            if (password != null) {
                _logger.LogToFile($"USER PASSWORD :: {password}", "REPOSITORY");
            } else {
                _logger.LogToFile($"USER PASSWORD ::Not found.", "REPOSITORY");
            }

            return password;
        }
    }
}
