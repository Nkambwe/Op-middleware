using Operators.Moddleware.Helpers;
using Operators.Moddleware.Data.Entities.Settings;
using System.Linq.Expressions;
using Operators.Moddleware.Data.Transactions;
using Newtonsoft.Json;

namespace Operators.Moddleware.Services.Settings {
    public class ThemeService(IUnitOfWorkFactory uowf) : IThemeService {
        //..field of UnitOfWorkFactory
        private readonly IUnitOfWorkFactory _uowf = uowf;
        private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<Theme> FindThemeAsync(Expression<Func<Theme, bool>> where) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Theme>();
            var theme= await _repo.GetAsync(where);
            if(theme != null){ 
                string json = JsonConvert.SerializeObject(theme);
                _logger.LogToFile($"RESULT : '{json}'", "THEMES");
            } else{
                _logger.LogToFile($"RESULT : Theme not found.", "THEMES");
            }
            return theme;
        }

        public async Task<Theme> GetFirstThemeAsync(Expression<Func<Theme, bool>> where) {
            Theme theme = null;
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Theme>();
            var themes = await _repo.GetAllAsync(where, false);

            if(themes.Any()) { 
                theme = themes.First();
                if(theme != null){ 
                    string json = JsonConvert.SerializeObject(theme);
                    _logger.LogToFile($"RESULT : '{json}'", "THEMES");
                } else{
                    _logger.LogToFile($"RESULT : Theme not found.", "THEMES");
                }

            }

            return theme;
        }
    }

}
