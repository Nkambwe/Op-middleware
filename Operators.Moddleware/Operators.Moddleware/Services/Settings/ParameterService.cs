using Newtonsoft.Json;
using Operators.Moddleware.Data.Entities.Settings;
using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Helpers;
using System.Linq.Expressions;

namespace Operators.Moddleware.Services.Settings {

    public class ParameterService(IUnitOfWorkFactory uowf) : IParameterService {

        private readonly IUnitOfWorkFactory _uowf = uowf;
        private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<string> GetStringParameterAsync(string param, bool includeDeleted = false) {
            _logger.LogToFile($"Retrieve string parameter: {param}", "PARAMS");

            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<ConfigurationParameter>();
            var paramater = await _repo.GetAsync(p => p.Parameter.Equals(param), includeDeleted);
            if (string.IsNullOrWhiteSpace(paramater.ParameterValue)) {
                _logger.LogToFile($"Parameter value for : {param} not set", "PARAMS");
                return string.Empty;
            }

            return paramater.ParameterValue.Trim();
        }

        public async Task<bool> GetBooleanParameterAsync(string param, bool includeDeleted = false) {
            _logger.LogToFile($"Retrieve boolean parameter: {param}", "PARAMS");

            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<ConfigurationParameter>();
            var paramater = await _repo.GetAsync(p => p.Parameter.Equals(param), includeDeleted);
            return paramater != null && paramater.ParameterValue.Equals("True", StringComparison.CurrentCultureIgnoreCase);
        }

        public async Task<int> GetIntegerParameterAsync(string param, bool includeDeleted = false) {
            _logger.LogToFile($"Retrieve integer parameter: {param}", "PARAMS");

            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<ConfigurationParameter>();
            var paramater = await _repo.GetAsync(p => p.Parameter.Equals(param), includeDeleted);
            if (paramater == null) {
                _logger.LogToFile($"Parameter value for : {param} not set", "PARAMS");
                return 0;
            }

            if (!int.TryParse(paramater.ParameterValue, out var value)) {
                _logger.LogToFile($"PESULT : Error converting parameter '{param}' value", "PARAMS");
                return 0;
            }

            return value;
        }

        public async Task<IList<ConfigurationParameter>> GetAllParametersAsync() {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<ConfigurationParameter>();
            var parameters = await _repo.GetAllAsync();
            if (!parameters.Any()) {
                _logger.LogToFile($"No configuration parameters found", "PARAMS");
            } else { 
                string json = JsonConvert.SerializeObject(parameters);
                _logger.LogToFile($"RESULT: Configuration Parematers {json} found", "PARAMS");
            }

            return parameters;
        }


        public async Task<IList<ConfigurationParameter>> GetParametersAsync(string identifier, bool includeDeleted = false) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<ConfigurationParameter>();
            var parameters  = await _repo.GetAllAsync(p => p.Identifier.Equals(identifier), includeDeleted);
            if (!parameters.Any()) {
                _logger.LogToFile($"No configuration parameters found", "PARAMS");
            } else { 
                string json = JsonConvert.SerializeObject(parameters);
                _logger.LogToFile($"RESULT: Configuration Parematers {json} found", "PARAMS");
            }

            return parameters;
        }

        public async Task<bool> InsertParametersAsync(params ConfigurationParameter[] parameters) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<ConfigurationParameter>();
            var inserted = await _repo.BulkyInsertAsync(parameters);
            if (inserted) {
                string json = JsonConvert.SerializeObject(parameters);
                _logger.LogToFile($"BULK  ACTION: Configuration Parematers {json} inserted", "PARAMS");
            } else { 
                _logger.LogToFile($"BULK  ACTION: Failed to insert configuration parameters", "PARAMS");
            }

            return inserted;
        }

        public async Task<bool> UpdateParametersAsync(params ConfigurationParameter[] parameters) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<ConfigurationParameter>();
            var updated = await _repo.BulkyUpdateAsync(parameters);
            if (updated) {
                string json = JsonConvert.SerializeObject(parameters);
                _logger.LogToFile($"BULK  ACTION: Configuration Parematers {json} updated", "PARAMS");
            } else { 
                _logger.LogToFile($"BULK  ACTION: Failed to update configuration parameters", "PARAMS");
            }

            return updated;
        }

        public async Task<IList<ConfigurationParameter>> GetParametersAsync(Expression<Func<ConfigurationParameter, bool>> where, bool includeDeleted) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<ConfigurationParameter>();
            var parameters = await _repo.GetAllAsync(where, includeDeleted);
            if (!parameters.Any()) {
                _logger.LogToFile($"RESULT: 0 configuration parameters found", "PARAMS");
            } else { 
                string json = JsonConvert.SerializeObject(parameters);
                _logger.LogToFile($"RESULT: Configuration Parematers {json} found", "PARAMS");
            }

            return parameters;
        }
    }
}
