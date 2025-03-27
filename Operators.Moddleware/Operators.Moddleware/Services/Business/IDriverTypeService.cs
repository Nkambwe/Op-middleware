using Operators.Moddleware.Data.Entities.Business;

namespace Operators.Moddleware.Services.Business {
    public interface IDriverTypeService {
        Task<IList<DriverType>> GetDriverTypesAsync(bool v, params long[] types);
    }

}
