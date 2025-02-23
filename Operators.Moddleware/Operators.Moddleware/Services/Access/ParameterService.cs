using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Data.Repositories.access;
using Operators.Moddleware.Helpers;
using System.Linq.Expressions;

namespace Operators.Moddleware.Services.Access {

    public class ParameterService(IParametersRepository repo) : IParameterService {

        private readonly IParametersRepository _repo = repo;
        private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<string> GetStringParameterAsync(string param, bool includeDeleted = false) {
            _logger.LogToFile($"Retrieve string parameter: {param}", "INFO");
            var paramater = await _repo.GetAsync(p => p.Parameter.Equals(param), includeDeleted);
            if(string.IsNullOrWhiteSpace(paramater.ParameterValue)){ 
                _logger.LogToFile($"Parameter value for : {param} not set", "INFO");
                return string.Empty;
            }
            
            return paramater.ParameterValue.Trim();
        }

        public async Task<bool> GetBooleanParameterAsync(string param, bool includeDeleted = false) {
            _logger.LogToFile($"Retrieve boolean parameter: {param}", "INFO");
            var paramater = await _repo.GetAsync(p => p.Parameter.Equals(param), includeDeleted);
            return paramater != null && paramater.ParameterValue.Equals("True", StringComparison.CurrentCultureIgnoreCase);
        }

        public async Task<int> GetIntegerParameterAsync(string param, bool includeDeleted = false) {
            _logger.LogToFile($"Retrieve integer parameter: {param}", "INFO");
            var paramater = await _repo.GetAsync(p => p.Parameter.Equals(param), includeDeleted);
            if(paramater == null){
                _logger.LogToFile($"Parameter value for : {param} not set", "INFO");
                return 0;    
            }
            
            if(!int.TryParse(paramater.ParameterValue, out var value)){ 
                return 0;
            }

            return value;
        }

        public async  Task<IList<ConfigurationParameter>> GetAllParametersAsync()
            => await _repo.GetAllAsync();

        public async Task<IList<ConfigurationParameter>> GetParametersAsync(string identifier, bool includeDeleted = false) 
            => await _repo.GetAllAsync(p => p.Identifier.Equals(identifier), includeDeleted);

        public async Task<bool> InsertParametersAsync(params ConfigurationParameter[] parameters) 
            => await _repo.BulkyInsertAsync(parameters);

        public async Task<bool> UpdateParametersAsync(params ConfigurationParameter[] parameters)
            => await _repo.BulkyUpdateAsync( parameters);

        public async Task<IList<ConfigurationParameter>> GetParametersAsync(Expression<Func<ConfigurationParameter, bool>> where, bool includeDeleted)
            => await _repo.GetAllAsync(where, includeDeleted);
    }
}
