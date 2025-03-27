using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services.Business {

    public class DriverTypeService(IUnitOfWorkFactory uowf) : IDriverTypeService {

         private readonly IUnitOfWorkFactory _uowf = uowf;
         private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<IList<DriverType>> GetDriverTypesAsync(bool includeDeleted = false, params long[] types) {

            _logger.LogToFile("Retrieve all drivers types");

            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<DriverType>();
            var dTypes = await _repo.GetAllAsync(t => types.Contains(t.Id), includeDeleted);
            if (dTypes != null) {
                 _logger.LogToFile($"RESULT : '{dTypes.Count}' records returned", "DIVER TYPES");
            } else {
               _logger.LogToFile($"No records found.", "DRIVER TYPES");
            }

            return dTypes;
        }

    }

}
