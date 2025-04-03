
namespace Operators.Moddleware.Services.Business {
    public interface IBusinessIndustryService {
        Task<Dictionary<long, string>> GetDistrictsAsync(bool includeDeleted = false, params long?[] ids);
    }

}
