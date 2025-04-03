using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Data.Repositories;
using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Helpers;
using System.Linq.Expressions;

namespace Operators.Moddleware.Services.Business {
    public class IndividualEmployerService(IUnitOfWorkFactory uowf) : IIndividualEmployerService {

        private readonly IUnitOfWorkFactory _uowf = uowf;
        private readonly ServiceLogger _logger = new("Operations_log");

        public async Task<PagedResult<IndividualEmployer>> GetAllPagedAsync(int page, int pageSize, bool includeDeleted) {
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<IndividualEmployer>();
            var individuals = await _repo.PageAllAsync(page, pageSize, includeDeleted);
            if (individuals != null) {
                _logger.LogToFile($"No records found.", "MEMBERS");
            } else {
                _logger.LogToFile($"RESULT : Page('{page}') and PageSize({pageSize})", "INDIVIDUALS");
            }

            return individuals;
        }

        public async Task<PagedResult<IndividualEmployer>> PageAllAsync(int page, int size, bool includeDeleted, Expression<Func<IndividualEmployer, bool>> where = null) {
             _logger.LogToFile("Retrieve all individual Employers");
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<IndividualEmployer>();
            var individuls = await _repo.PageAllAsync(page, size, includeDeleted, where);
            if (individuls != null) {
                _logger.LogToFile($"No records found.", "MEMBERS");
            } else {
                _logger.LogToFile($"RESULT : Page('{page}') and PageSize({size})", "INDIVIDUALS");
            }

            return individuls;
        }

        public async Task<PagedResult<IndividualEmployer>> PageAllAsync(CancellationToken token, int page, int size, Expression<Func<IndividualEmployer, bool>> where = null, bool includeDeleted = false) {
             _logger.LogToFile("Retrieve all members");
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<IndividualEmployer>();
            var members = await _repo.PageAllAsync(token, page, size, where, includeDeleted);
            if (members != null) {
                _logger.LogToFile($"No records found.", "MEMBERS");
            } else {
                _logger.LogToFile($"RESULT : Page('{page}') and PageSize({size})", "MEMBERS");
            }

            return members;
        }
    }

}
