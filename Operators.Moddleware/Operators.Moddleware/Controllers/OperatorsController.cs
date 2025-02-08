using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Operators.Moddleware.Helpers;
using Operators.Moddleware.HttpHelpers;
using Operators.Moddleware.Services;
using Operators.Moddleware.Services.Access;

namespace Operators.Moddleware.Controllers {

    [ApiController]
    [Route("middleware")]
    public class OperatorsController(IMapper mapper, IBranchService branchService, IUserService userService,
        IRoleService roleService, IPermissionService permissionService,
        IPasswordService passwordService, IParameterService parameterService) : ControllerBase {
        private readonly IMapper _mapper = mapper;
        
        private readonly IServiceLogger _logger = new ServiceLogger("Operations_log");
        private DecryptionHandler decrypter;
        private readonly IBranchService _branchService = branchService;
        private readonly IUserService _userService = userService;
        private readonly IRoleService _roleService = roleService;
        private readonly IPermissionService _permissionService = permissionService;
        private readonly IPasswordService _passwordService = passwordService;
        private readonly IParameterService _parameterService = parameterService;

        [HttpPost("RetrieveUser")]
        [Produces("application/json")]
        public async Task<IActionResult> RetrieveUser([FromBody]UserRequest request) {
            try {
                var user = await _userService.FindUsernameAsync(request.Username);
        
                if (user == null) {
                    return new JsonResult(new ErrorResponse(
                        (int)ResponseCode.NOTFOUND,
                        ResponseCode.NOTFOUND.GetDescription(),
                        "Username does not exist"
                    ));
                }

                if (!user.IsActive) {
                    return new JsonResult(new ErrorResponse(
                        (int)ResponseCode.FORBIDDEN,
                        ResponseCode.FORBIDDEN.GetDescription(),
                        $"User account with username '{request.Username}' is deactivated"
                    ));
                }

                if (!user.IsVerified) {
                    return new JsonResult(new ErrorResponse(
                        (int)ResponseCode.FORBIDDEN,
                        ResponseCode.FORBIDDEN.GetDescription(),
                        $"User account with username '{request.Username}' is not verified"
                    ));
                }

                if (user.IsDeleted) {
                    return new JsonResult(new ErrorResponse(
                        (int)ResponseCode.FORBIDDEN,
                        ResponseCode.FORBIDDEN.GetDescription(),
                        $"User account with username '{request.Username}' is deleted"
                    ));
                }

                var result = _mapper.Map<UserResponse>(user);
                
                //get userpassword
                var password = await _passwordService.GetPasswordAsync(user.CurrentPassword);
                if (password != null) { 
                    result.ExpiresIn = ( await _parameterService.GetIntegerParameterAsync(ParameterName.PWD_NUMBEROFDAYSTOEXPIREY)) - (DateTime.Now - password.SetOn).Days;
                    result.PasswordId = password.Id;
                    result.Password = password.Password;
                }

                //check wether passwords are allowed to expire
                result.ExpirePasswords = await _parameterService.GetBooleanParameterAsync(ParameterName.PWD_USEEXIPRINGPASSWORDS);
                
                //..decrypt fields
                if(request.Decrypt != null && request.Decrypt.Length > 0) { 

                    try{ 
                        decrypter = new(_logger);
                        result = decrypter.DecryptProperties(result, request.Decrypt);
                    } catch(Exception ex){ 
                        string msg = $"{ex.Message}";
                        return new JsonResult(new ErrorResponse(
                            (int)ResponseCode.SERVERERROR,
                            ResponseCode.SERVERERROR.GetDescription(),
                            msg
                        ));
                    }
                    
                }

                result.StatusCode = (int)ResponseCode.SUCCESS;
                result.ResponseMessage = ResponseCode.SUCCESS.GetDescription();
                return new JsonResult(result);
            } catch (Exception ex) {
                string msg = $"System Error! {ex.Message}";
                _logger.LogToFile(msg, "ERROR");
                _logger.LogToFile($"{ex.StackTrace}", "STACKTRACE");
        
               return new JsonResult(new ErrorResponse(
                    (int)ResponseCode.SERVERERROR,
                    ResponseCode.SERVERERROR.GetDescription(),
                    msg
                ));
            }
        }

    }
}
