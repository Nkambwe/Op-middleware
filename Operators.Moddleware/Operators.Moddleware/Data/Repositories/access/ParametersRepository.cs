using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities.Settings;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Data.Repositories.access {

    public class ParametersRepository(IDbContextFactory<OpsDbContext> contextFactory) :
        Repository<ConfigurationParameter>(contextFactory), IParametersRepository {

        private readonly IDbContextFactory<OpsDbContext>  _context = contextFactory;
        private readonly ServiceLogger _logger = new("Operations_log");

    }
}
