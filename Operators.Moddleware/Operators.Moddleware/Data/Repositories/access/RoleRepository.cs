using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Data.Repositories.access {

    public class RoleRepository(IDbContextFactory<OpsDbContext> contextFactory) :
        Repository<Role>(contextFactory), IRoleRepository {
        private readonly IDbContextFactory<OpsDbContext>  _context = contextFactory;
        private readonly ServiceLogger _logger = new("Operations_log");

    }
}
