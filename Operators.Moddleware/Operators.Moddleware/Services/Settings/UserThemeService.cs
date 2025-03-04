using Newtonsoft.Json;
using Operators.Moddleware.Data.Entities.Settings;
using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Helpers;
using System.Linq.Expressions;

namespace Operators.Moddleware.Services.Settings {

    public class UserThemeService(IUnitOfWorkFactory uowf) : IUserThemeService {
         //..field of UnitOfWorkFactory
        private readonly IUnitOfWorkFactory _uowf = uowf;
        private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<UserTheme> FindUserThemeAsync(Expression<Func<UserTheme, bool>> where) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<UserTheme>();
            var theme = await _repo.GetAsync(where);
            if (theme == null) {
                string json = JsonConvert.SerializeObject(theme);
                _logger.LogToFile($"RESULT ::'{json}'", "USERTHEMES");
            } else { 
                _logger.LogToFile($"RESULT :: No themes found for user", "USERTHEMES");
            }

            return theme;
        }

        public async Task<bool> InsertThemeAsync(UserTheme userTheme) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<UserTheme>();
            var saved = await _repo.InsertAsync(userTheme);
            if (saved) {
                string json = JsonConvert.SerializeObject(userTheme);
                _logger.LogToFile($"RESULT ::'{json}'", "USERTHEMES");
            } else { 
                _logger.LogToFile($"RESULT :: User theme not saved. An error occurred", "USERTHEMES");
            }

            return saved;
        }

        public async Task<bool> UpdateUserThemeAsync(UserTheme theme) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<UserTheme>();
            var updated = await _repo.UpdateAsync(theme);
            if (updated) {
                string json = JsonConvert.SerializeObject(theme);
                _logger.LogToFile($"RESULT ::'{json}'", "USERTHEMES");
            } else { 
                _logger.LogToFile($"RESULT :: User theme not updated. An error occurred", "USERTHEMES");
            }

            return updated;
        }
    }

}
