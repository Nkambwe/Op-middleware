using AutoMapper;
using Operators.Moddleware.Helpers;
using Operators.Moddleware.Services;
using Operators.Moddleware.Services.Access;
using Operators.Moddleware.Services.Business;
using Operators.Moddleware.Services.Settings;

namespace Operators.Moddleware.Controllers {
    public class EmployerController : BaseController {

        private readonly IServiceLogger _logger;
        private readonly IEmployerTypeService _employers;
        private readonly IIndividualEmployerService _individuals;
        private readonly IBusinessEmployerService _businesses;
        private readonly IBusinessIndustryService _industries;
        private readonly IDistrictService _districts;
       

        public EmployerController(IMapper mapper, IServiceLogger logger,  
                                IBranchService branches, 
                                IDistrictService districts,
                                IBusinessEmployerService businesses,
                                IIndividualEmployerService individuals,
                                IBusinessIndustryService industries,
                                IDistrictService _districts,
                                IParameterService parameters, 
                                IUserService userService, IEmployerTypeService employers) :
                                base(mapper, branches, parameters, userService) {

            _logger = logger;
            _logger.Channel = $"[EMPLOYERS {DateTime.Now:yyyyMMMdddHHmmss}]";
            _employers = employers;
            _individuals = individuals;
            _businesses = businesses;
            _industries = industries;
            _districts = districts;
        }
    }

}
