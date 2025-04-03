
using Operators.Moddleware.Data.Entities.Business;

namespace Operators.Moddleware.Services.Business {
    public interface IBusinessContactService {

        Task<IList<BusinessContact>> GetContactsAsync(bool includeDeleted, params long[] types);

    }
}
