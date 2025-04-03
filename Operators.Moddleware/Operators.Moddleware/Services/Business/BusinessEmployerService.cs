using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Data.Repositories;
using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Helpers;
using System.Linq.Expressions;

namespace Operators.Moddleware.Services.Business {

    public class BusinessEmployerService(IUnitOfWorkFactory uowf) : IBusinessEmployerService {

         private readonly IUnitOfWorkFactory _uowf = uowf;
         private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<PagedResult<BusinessEmployer>> GetAllPagedAsync(int page, int pageSize, bool includeDeleted) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<BusinessEmployer>();
            var businesses = await _repo.PageAllAsync(page, pageSize, includeDeleted);
            if (businesses != null) {
                _logger.LogToFile($"No records found.", "BUSINESSES");
            } else {
                _logger.LogToFile($"RESULT : Page('{page}') and PageSize({pageSize})", "BUSINESSES");
            }

            return businesses;
        }

        public async Task<PagedResult<BusinessEmployer>> PageAllAsync(int page, int size, bool includeDeleted, Expression<Func<BusinessEmployer, bool>> where = null) {
             _logger.LogToFile("Retrieve all individual Employers");
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<BusinessEmployer>();
            var businesses = await _repo.PageAllAsync(page, size, includeDeleted, where);
            if (businesses != null) {
                _logger.LogToFile($"No records found.", "BUSINESSES");
            } else {
                _logger.LogToFile($"RESULT : Page('{page}') and PageSize({size})", "BUSINESSES");
            }

            return businesses;
        }

        public async Task<PagedResult<BusinessEmployer>> PageAllAsync(CancellationToken token, int page, int size, Expression<Func<BusinessEmployer, bool>> where = null, bool includeDeleted = false) {
            _logger.LogToFile("Retrieve all Employers");
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<BusinessEmployer>();
            var members = await _repo.PageAllAsync(token, page, size, where, includeDeleted);
            if (members != null) {
                _logger.LogToFile($"No records found.", "BUSINESSES");
            } else {
                _logger.LogToFile($"RESULT : Page('{page}') and PageSize({size})", "BUSINESSES");
            }

            return members;
        }
    }
}
