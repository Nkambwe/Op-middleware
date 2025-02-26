using System.Linq.Expressions;
using Operators.Moddleware.Data.Entities.Settings;

namespace Operators.Moddleware.Services.Settings {

    public interface IThemeService {
        Task<Theme> FindThemeAsync(Expression<Func<Theme, bool>> where);
    }

}
