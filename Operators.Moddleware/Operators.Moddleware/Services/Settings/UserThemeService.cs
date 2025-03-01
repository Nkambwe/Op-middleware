using Operators.Moddleware.Data.Entities.Settings;
using Operators.Moddleware.Data.Repositories.Settings;
using System.Linq.Expressions;

namespace Operators.Moddleware.Services.Settings {

    public class UserThemeService(IUserThemeRepository repo) : IUserThemeService {
        private readonly IUserThemeRepository _repo = repo;

        public async Task<UserTheme> FindUserThemeAsync(Expression<Func<UserTheme, bool>> where)
            => await _repo.GetAsync(where);

        public async Task<bool> InsertThemeAsync(UserTheme userTheme)
            => await _repo.InsertAsync(userTheme);

        public async Task<bool> UpdateUserThemeAsync(UserTheme theme)
            => await _repo.UpdateAsync(theme);
    }

}
