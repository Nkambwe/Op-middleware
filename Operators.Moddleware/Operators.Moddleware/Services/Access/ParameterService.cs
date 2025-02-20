using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Data.Repositories.access;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services.Access {
    public class ParameterService(IParametersRepository repo) : IParameterService {
        private readonly IParametersRepository _repo = repo;
        private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<string> GetStringParameterAsync(string param) {
            _logger.LogToFile($"Retrieve string parameter: {param}", "INFO");
            var paramater = await _repo.GetAsync(p => p.Parameter.Equals(param));
            if(string.IsNullOrWhiteSpace(paramater.ParameterValue)){ 
                _logger.LogToFile($"Parameter value for : {param} not set", "INFO");
                return string.Empty;
            }
            
            return paramater.ParameterValue.Trim();
        }

        public async Task<bool> GetBooleanParameterAsync(string param) {
            _logger.LogToFile($"Retrieve boolean parameter: {param}", "INFO");
            var paramater = await _repo.GetAsync(p => p.Parameter.Equals(param));
            return paramater != null && paramater.ParameterValue.Equals("1");
        }

        public async Task<int> GetIntegerParameterAsync(string param) {
            _logger.LogToFile($"Retrieve integer parameter: {param}", "INFO");
            var paramater = await _repo.GetAsync(p => p.Parameter.Equals(param));
            if(paramater == null){
                _logger.LogToFile($"Parameter value for : {param} not set", "INFO");
                return 0;    
            }
            
            if(!int.TryParse(paramater.ParameterValue, out var value)){ 
                return 0;
            }

            return value;
        }

        public Task<List<ConfigurationParameter>> GetParametersAsync(string identifier) {
            throw new NotImplementedException();
        }

        public Task<bool> InsertParametersAsync(params ConfigurationParameter[] parameters) {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateParametersAsync(params ConfigurationParameter[] parameters) {
            throw new NotImplementedException();
        }
    }
}
