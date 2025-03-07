using AutoMapper;
using Operators.Moddleware.Helpers;
using Operators.Moddleware.Services;
using Operators.Moddleware.Services.Access;
using Operators.Moddleware.Services.Settings;

namespace Operators.Moddleware.Controllers {

    public class SystemController : BaseController {

        private readonly IServiceLogger _logger;
        private readonly IRoleService _roles;
        private readonly IPermissionService _permissions;
        private readonly IPasswordService _passwords;

        public SystemController(IMapper mapper, IServiceLogger logger, 
            IRoleService roles,
            IPermissionService permissions,
            IPasswordService passwords,
            IBranchService branches, 
            IParameterService parameters, 
            IUserService userService) : 
            base(mapper, branches, parameters, userService) {

            _logger = logger;
            _logger.Channel = $"[SYSTEM {DateTime.Now:yyyyMMMdddHHmmss}]";

            _roles = roles;
            _permissions = permissions;
            _passwords = passwords;

        }

    }
}
