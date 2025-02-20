using Operators.Moddleware.Data.Entities;

namespace Operators.Moddleware.Services.Access {
    public interface IParameterService {
        /// <summary>
        /// Get parameter of a boolean value
        /// </summary>
        /// <param name="param">Parameter name</param>
        /// <returns>Task containg a parameter of boolean value</returns>
        Task<bool> GetBooleanParameterAsync(string param);
         /// <summary>
        /// Get parameter of a integer value
        /// </summary>
        /// <param name="param">Parameter name</param>
        /// <returns>Task containg a parameter of integer value</returns>
        Task<int> GetIntegerParameterAsync(string param);
        /// <summary>
        /// Get a list of parameters belonging to a defined identifier
        /// </summary>
        /// <param name="identifier">Parameter identifier</param>
        /// <returns>Task containg a list of parameters</returns>
        Task<List<ConfigurationParameter>> GetParametersAsync(string identifier);
        /// <summary>
        /// Insert a list of configuration parameters to the database
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>Task containg a boolean value for the insert state</returns>
        Task<bool> InsertParametersAsync(params ConfigurationParameter[] parameters);
        /// <summary>
        /// Update a list of configuration parameters to the database
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>Task containg a boolean value for the update state</returns>
        Task<bool> UpdateParametersAsync(params ConfigurationParameter[] parameters);

    }
}