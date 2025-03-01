using Operators.Moddleware.Helpers;
using Operators.Moddleware.Data.Repositories.Settings;
using Operators.Moddleware.Data.Entities.Settings;
using System.Linq.Expressions;

namespace Operators.Moddleware.Services.Settings {
    public class ThemeService(IThemeRepository repo) : IThemeService {
        private readonly IThemeRepository _repo = repo;
        private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<Theme> FindThemeAsync(Expression<Func<Theme, bool>> where)
            => await _repo.GetAsync(where);

        public async Task<Theme> GetFirstThemeAsync(Expression<Func<Theme, bool>> where) {
            Theme theme = null;
            var themes = await _repo.GetAllAsync(where, false);
            if(themes.Any()) { 
                return themes.First();
            }

            return theme;
        }
    }

}
