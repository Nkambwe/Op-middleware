using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Data.Repositories.access {

    public class RoleRepository(OpsDbContext context) : Repository<Role>(context), IRoleRepository {
        private readonly OpsDbContext _context = context;
        private readonly ServiceLogger _logger = new("Operations_log");

    }
}
