
using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities.Settings;

namespace Operators.Moddleware.Data.Repositories.Settings {

    public class UserThemeRepository(IDbContextFactory<OpsDbContext> contextFactory) :
        Repository<UserTheme>(contextFactory), IUserThemeRepository {

         private readonly IDbContextFactory<OpsDbContext>  _context = contextFactory;

    }

}
