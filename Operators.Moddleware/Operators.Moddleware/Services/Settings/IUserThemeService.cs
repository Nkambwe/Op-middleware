
using System.Linq.Expressions;
using Operators.Moddleware.Data.Entities.Settings;

namespace Operators.Moddleware.Services.Settings {

    public interface IUserThemeService {
        Task<UserTheme> FindUserThemeAsync(Expression<Func<UserTheme, bool>> where);
        Task<bool> InsertThemeAsync(UserTheme userTheme);
        Task<bool> UpdateUserThemeAsync(UserTheme theme);
    }
}
