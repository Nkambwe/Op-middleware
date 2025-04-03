using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services.Business {
    public class BusinessContactService(IUnitOfWorkFactory uowf) : IBusinessContactService {

         private readonly IUnitOfWorkFactory _uowf = uowf;
         private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<IList<BusinessContact>> GetContactsAsync(bool includeDeleted, params long[] ids) {
            _logger.LogToFile("Retrieve district areas");

            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<BusinessContact>();
            var areas = await _repo.GetAllAsync(t => ids.Contains(t.Id), includeDeleted);
            if (areas != null) {
                _logger.LogToFile($"RESULT : '{areas.Count}' records returned", "DISTRICTS");
            } else {
                _logger.LogToFile($"No records found.", "DISTRICTS");
            }

            return [.. areas];
        }
    }
}
