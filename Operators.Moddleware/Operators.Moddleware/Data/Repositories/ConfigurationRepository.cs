using Operators.Moddleware.Data.Entities;

namespace Operators.Moddleware.Data.Repositories {
    public class ConfigurationRepository(OpsDbContext context) 
        : Repository<ConfigurationParameter>(context), IConfigurationRepository {

        private readonly OpsDbContext _context = context;
    }
}
