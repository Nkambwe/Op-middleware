using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Data.Repositories.access {

    public class PasswordsRepository(IDbContextFactory<OpsDbContext> contextFactory) :
        Repository<UserPassword>(contextFactory), IPasswordsRepository {

        private readonly IDbContextFactory<OpsDbContext>  _context = contextFactory;
        private readonly ServiceLogger _logger = new("Operations_log");

    }
}
