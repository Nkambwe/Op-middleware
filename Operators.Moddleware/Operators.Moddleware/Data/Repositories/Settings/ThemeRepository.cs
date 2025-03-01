using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities.Settings;

namespace Operators.Moddleware.Data.Repositories.Settings {
    public class ThemeRepository(IDbContextFactory<OpsDbContext> contextFactory) :
    Repository<Theme>(contextFactory), IThemeRepository {

        private readonly IDbContextFactory<OpsDbContext>  _context = contextFactory;
    }
}
