using Operators.Moddleware.Data.Transactions;
using Operators.Moddleware.Helpers;

namespace Operators.Moddleware.Services.Business {
    public class AttachmentTypeService(IUnitOfWorkFactory uowf) : IAttachmentTypeService {

         private readonly IUnitOfWorkFactory _uowf = uowf;
         private readonly ServiceLogger _logger = new("Operations_log");
     }
}
