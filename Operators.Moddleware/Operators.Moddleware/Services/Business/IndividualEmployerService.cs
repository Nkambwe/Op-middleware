using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services.Business {
    public class IndividualEmployerService(IUnitOfWorkFactory uowf) : IIndividualEmployerService {

         private readonly IUnitOfWorkFactory _uowf = uowf;
         private readonly ServiceLogger _logger = new("Operations_log");
    }

}
