using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities.Settings;

namespace Operators.Moddleware.Data.Repositories.Settings {
    public class ConfigurationRepository(IDbContextFactory<OpsDbContext> contextFactory) :
    Repository<ConfigurationParameter>(contextFactory), IConfigurationRepository {

        private readonly IDbContextFactory<OpsDbContext>  _context = contextFactory;
    }
}
