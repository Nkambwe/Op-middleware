using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services {
    public class DistrictService(IUnitOfWorkFactory uowf) : IDistrictService {

        private readonly IUnitOfWorkFactory _uowf = uowf;
        private readonly ServiceLogger _logger = new("Operations_log");
    }
}
