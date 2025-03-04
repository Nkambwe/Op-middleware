using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services.Access {

    public class PasswordService (IUnitOfWorkFactory uowf) : IPasswordService {

        private readonly IUnitOfWorkFactory _uowf = uowf;
        private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<UserPassword> GetPasswordAsync(long currentPassword) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<UserPassword>();

            var password = await _repo.GetAsync(p => p.Id == currentPassword);
            if (password == null) {
                _logger.LogToFile("RESULT ::User password not found.", "PASSWORDS");
            }

            return password;
        }
    }
}
