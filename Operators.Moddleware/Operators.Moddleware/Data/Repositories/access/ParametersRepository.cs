using Operators.Moddleware.Data.Entities.Settings;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Data.Repositories.access {

    public class ParametersRepository(OpsDbContext context) : Repository<ConfigurationParameter>(context), IParametersRepository {
        private readonly OpsDbContext _context = context;
        private readonly ServiceLogger _logger = new("Operations_log");

    }
}
