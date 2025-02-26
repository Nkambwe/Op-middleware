
using Operators.Moddleware.Data.Entities.Settings;

namespace Operators.Moddleware.Data.Repositories.Settings {
    public class UserThemeRepository(OpsDbContext context): 
        Repository<UserTheme>(context), IUserThemeRepository {

         private readonly OpsDbContext _context = context; 

    }

}
