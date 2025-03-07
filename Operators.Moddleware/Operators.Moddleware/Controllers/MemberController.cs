using AutoMapper;
using Operators.Moddleware.Helpers;
using Operators.Moddleware.Services;
using Operators.Moddleware.Services.Access;
using Operators.Moddleware.Services.Business;
using Operators.Moddleware.Services.Settings;

namespace Operators.Moddleware.Controllers {

    public class MemberController : BaseController {
        
        private readonly IServiceLogger _logger;
        private readonly IMemberService _members;

        public MemberController(IMapper mapper, IServiceLogger logger, 
            IBranchService branches, 
            IParameterService parameters, 
            IUserService userService,
            IMemberService members) : 
            base(mapper, branches, parameters, userService) {
            
            _logger = logger;
            _logger.Channel = $"[DRIVERS {DateTime.Now:yyyyMMMdddHHmmss}]";
            _members = members;
        }
    }

}
