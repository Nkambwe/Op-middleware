using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services.Business {
    public class BusinessIndustryService(IUnitOfWorkFactory uowf) : IBusinessIndustryService {

         private readonly IUnitOfWorkFactory _uowf = uowf;
         private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<Dictionary<long, string>> GetDistrictsAsync(bool includeDeleted = false,params long?[] ids) {
            var result = new Dictionary<long, string>();
            _logger.LogToFile("Retrieve district areas");

            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<BusinessIndustry>();
            var areas = await _repo.GetAllAsync(t => ids.Contains(t.Id), includeDeleted);
            if (areas != null) {
                if(result.Count != 0) { 
                    result = areas.ToDictionary(
                        c => c.Id, 
                        c => c.IndustryName
                    );
                }
                _logger.LogToFile($"RESULT : '{areas.Count}' records returned", "DISTRICTS");
                
            } else {
                _logger.LogToFile($"No records found.", "DISTRICTS");
            }

            return result;
        }
    }

}
