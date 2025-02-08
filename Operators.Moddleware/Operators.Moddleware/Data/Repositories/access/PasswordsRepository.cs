using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Data.Repositories.access {

    public class PasswordsRepository(OpsDbContext context) : Repository<UserPassword>(context), IPasswordsRepository {
        private readonly OpsDbContext _context = context;
        private readonly ServiceLogger _logger = new("Operations_log");

    }
}
