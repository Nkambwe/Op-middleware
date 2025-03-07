using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Operators.Moddleware.Services;
using Operators.Moddleware.Services.Access;
using Operators.Moddleware.Services.Settings;

namespace Operators.Moddleware.Controllers {

    public class BaseController(IMapper mapper, IBranchService branches,
                                IParameterService parameters,
                                IUserService userService) : ControllerBase {
        internal IMapper Mapper => mapper;
        internal IBranchService Branches => branches;
        internal IUserService UserService => userService;
        internal IParameterService ParameterService => parameters;
        
    }

}
