using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Data.Repositories;
using System.Linq.Expressions;

namespace Operators.Moddleware.Services.Business {

    public interface IMemberService {
        
        Task<PagedResult<Member>> GetAllPagedAsync(int page, int pageSize, bool includeDeleted);
        
        Task<PagedResult<Member>> PageAllAsync(int page, int size, bool includeDeleted, Expression<Func<Member, bool>> where = null);

        Task<PagedResult<Member>> PageAllAsync(CancellationToken token, int page, int size, Expression<Func<Member, bool>> where = null, bool includeDeleted = false);
    }

}
