
using Operators.Moddleware.Data.Entities.Settings;

namespace Operators.Moddleware.Data.Repositories.Settings {
    public class ThemeRepository(OpsDbContext context) : Repository<Theme>(context), IThemeRepository {
        private readonly OpsDbContext _context = context; 
    }
}
