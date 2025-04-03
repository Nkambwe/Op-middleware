using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Data.Repositories;
using System.Linq.Expressions;

namespace Operators.Moddleware.Services.Business {

    public interface IBusinessEmployerService {

            Task<PagedResult<BusinessEmployer>> GetAllPagedAsync(int page, int pageSize, bool includeDeleted);
        
            Task<PagedResult<BusinessEmployer>> PageAllAsync(int page, int size, bool includeDeleted, Expression<Func<BusinessEmployer, bool>> where = null);

            Task<PagedResult<BusinessEmployer>> PageAllAsync(CancellationToken token, int page, int size, Expression<Func<BusinessEmployer, bool>> where = null, bool includeDeleted = false);

    }
}
