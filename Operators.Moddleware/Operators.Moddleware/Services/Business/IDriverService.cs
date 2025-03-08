using Operators.Moddleware.Data.Entities.Business;
using System.Linq.Expressions;

namespace Operators.Moddleware.Services.Business {

    public interface IDriverService {

        Task<IList<Driver>> GetAllAsync(bool includeDeleted, Expression<Func<Driver, bool>> expression);
    }

}
