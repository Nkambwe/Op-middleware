
namespace Operators.Moddleware.Services {

    public interface IDistrictService {
        Task<Dictionary<long, string>> GetDistrictsAsync(bool includeDeleted = false, params long[] ids);
    }
}
