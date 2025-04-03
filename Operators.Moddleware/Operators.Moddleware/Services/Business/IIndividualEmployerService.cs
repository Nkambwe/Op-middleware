using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Data.Repositories;
using System.Linq.Expressions;

namespace Operators.Moddleware.Services.Business {

    public interface IIndividualEmployerService {

            Task<PagedResult<IndividualEmployer>> GetAllPagedAsync(int page, int pageSize, bool includeDeleted);
        
            Task<PagedResult<IndividualEmployer>> PageAllAsync(int page, int size, bool includeDeleted, Expression<Func<IndividualEmployer, bool>> where = null);

            Task<PagedResult<IndividualEmployer>> PageAllAsync(CancellationToken token, int page, int size, Expression<Func<IndividualEmployer, bool>> where = null, bool includeDeleted = false);

    }

}
