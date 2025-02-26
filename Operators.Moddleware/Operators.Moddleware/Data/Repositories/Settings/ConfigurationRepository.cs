using Operators.Moddleware.Data.Entities.Settings;

namespace Operators.Moddleware.Data.Repositories.Settings {
    public class ConfigurationRepository(OpsDbContext context)
        : Repository<ConfigurationParameter>(context), IConfigurationRepository {

        private readonly OpsDbContext _context = context;
    }
}
