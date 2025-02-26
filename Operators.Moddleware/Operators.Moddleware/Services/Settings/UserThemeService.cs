using Operators.Moddleware.Data.Repositories.Settings;

namespace Operators.Moddleware.Services.Settings {
    public class UserThemeService(IUserThemeRepository repo)  : IUserThemeService {

        IUserThemeRepository _repo = repo;
    }
}
