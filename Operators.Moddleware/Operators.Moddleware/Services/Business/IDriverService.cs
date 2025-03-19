using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Data.Repositories;
using System.Linq.Expressions;

namespace Operators.Moddleware.Services.Business {

    public interface IDriverService {

        Task<IList<Driver>> GetAllAsync(bool includeDeleted, Expression<Func<Driver, bool>> expression);

        Task<PagedResult<Driver>> GetAllPagedAsync(int page, int pageSize, bool includeDeleted);

        Task<PagedResult<Driver>> PageAllAsync(int page, int size, bool includeDeleted, Expression<Func<Driver, bool>> where = null);

        Task<PagedResult<Driver>> PageAllAsync(CancellationToken token, int page, int size, Expression<Func<Driver, bool>> where = null, bool includeDeleted = false);
    }

}
