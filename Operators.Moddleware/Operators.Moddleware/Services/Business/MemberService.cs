using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Data.Repositories;
using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Helpers;
using System.Linq.Expressions;

namespace Operators.Moddleware.Services.Business {

    public class MemberService(IUnitOfWorkFactory uowf) : IMemberService {

         private readonly IUnitOfWorkFactory _uowf = uowf;
         private readonly ServiceLogger _logger = new("Operations_log");

        
        public async Task<PagedResult<Member>> GetAllPagedAsync(int page, int pageSize, bool includeDeleted) {
             _logger.LogToFile("Retrieve all members");
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Member>();
            var members = await _repo.PageAllAsync(page, pageSize, includeDeleted);
            if (members != null) {
                _logger.LogToFile($"No records found.", "MEMBERS");
            } else {
                _logger.LogToFile($"RESULT : Page('{page}') and PageSize({pageSize})", "MEMBERS");
            }

            return members;
        }

        public async Task<PagedResult<Member>> PageAllAsync(int page, int size, bool includeDeleted, Expression<Func<Member, bool>> where = null) {
             _logger.LogToFile("Retrieve all members");
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Member>();
            var members = await _repo.PageAllAsync(page, size, includeDeleted, where);
            if (members != null) {
                _logger.LogToFile($"No records found.", "MEMBERS");
            } else {
                _logger.LogToFile($"RESULT : Page('{page}') and PageSize({size})", "MEMBERS");
            }

            return members;
        }

        public async Task<PagedResult<Member>> PageAllAsync(CancellationToken token, int page, int size, Expression<Func<Member, bool>> where = null, bool includeDeleted = false) {
             _logger.LogToFile("Retrieve all members");
            using var _uow = _uowf.Create();
            var _repo = _uow.GetRepository<Member>();
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
