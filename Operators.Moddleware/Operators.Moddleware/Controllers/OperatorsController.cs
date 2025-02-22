using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Data.Entities.Access;
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
            UserResponse userResp;
            string[] decrypt = request.Decrypt; 
            string response;
            try {
                //..get this from settings
                var includeDeleted = await _parameterService.GetBooleanParameterAsync("includeDeletedObjects");

                var user = await _userService.FindUsernameAsync(request.Username, includeDeleted);
        
               
                if (user == null) {
                    userResp = new UserResponse() {
                        ResponseCode =  (int)ResponseCode.NOTFOUND,
                        ResponseMessage = ResponseCode.NOTFOUND.GetDescription(),
                        ResponseDescription =   "Username does not exist",
                        Data = null
                    };

                    response = JsonConvert.SerializeObject(userResp);
                    _logger.LogToFile($"API RESPONSE : {response}", "MSG");
                    return new JsonResult(userResp);
                }

                if (!user.IsActive) {
                    userResp = new UserResponse() {
                        ResponseCode =  (int)ResponseCode.FORBIDDEN,
                        ResponseMessage = ResponseCode.FORBIDDEN.GetDescription(),
                        ResponseDescription =   $"User account with username '{request.Username}' is deactivated",
                        Data = null
                    };

                    response = JsonConvert.SerializeObject(userResp);
                    _logger.LogToFile($"API RESPONSE : {response}", "MSG");
                    return new JsonResult(userResp);
                }

                if (!user.IsVerified) {
                    userResp = new UserResponse() {
                        ResponseCode =  (int)ResponseCode.FORBIDDEN,
                        ResponseMessage = ResponseCode.FORBIDDEN.GetDescription(),
                        ResponseDescription =  $"User account with username '{request.Username}' is not verified",
                        Data = null
                    };

                    response = JsonConvert.SerializeObject(userResp);
                    _logger.LogToFile($"API RESPONSE : {response}", "MSG");
                    return new JsonResult(userResp);
                }

                if (user.IsDeleted) {
                    userResp = new UserResponse() {
                        ResponseCode =  (int)ResponseCode.FORBIDDEN,
                        ResponseMessage = ResponseCode.FORBIDDEN.GetDescription(),
                        ResponseDescription =  $"User account with username '{request.Username}' is deleted",
                        Data = null
                    };

                    response = JsonConvert.SerializeObject(userResp);
                    _logger.LogToFile($"API RESPONSE : {response}", "MSG");
                    return new JsonResult(userResp);
                }

                userResp = _mapper.Map<UserResponse>(user);
                
                //get userpassword
                var password = await _passwordService.GetPasswordAsync(user.CurrentPassword);
                if (password != null) { 
                    userResp.Data.ExpiresIn = ( await _parameterService.GetIntegerParameterAsync(ParameterName.PWD_NUMBEROFDAYSTOEXPIREY)) - (DateTime.Now - password.SetOn).Days;
                    userResp.Data.PasswordId = password.Id;
                    userResp.Data.Password = password.Password;
                }

                //check wether passwords are allowed to expire
                userResp.Data.ExpirePasswords = await _parameterService.GetBooleanParameterAsync(ParameterName.PWD_USEEXIPRINGPASSWORDS);
                
                //..decrypt fields
                if(decrypt != null && decrypt.Length > 0) { 

                    try{ 
                        decrypter = new(_logger);
                        userResp.Data = decrypter.DecryptProperties(userResp.Data, request.Decrypt);
                    } catch(Exception ex){ 
                        string msg = $"{ex.Message}";
                        userResp = new UserResponse() {
                            ResponseCode =  (int)ResponseCode.SERVERERROR,
                            ResponseMessage = msg,
                            ResponseDescription = ResponseCode.SERVERERROR.GetDescription(),
                            Data = null
                        };
                    }
                    
                }

                userResp.ResponseCode = (int)ResponseCode.SUCCESS;
                userResp.ResponseMessage = ResponseCode.SUCCESS.GetDescription();
            } catch (Exception ex) {
                string msg = $"System Error! {ex.Message}";
                _logger.LogToFile(msg, "ERROR");
                _logger.LogToFile($"{ex.StackTrace}", "STACKTRACE");
        
                userResp = new UserResponse() {
                    ResponseCode =  (int)ResponseCode.SERVERERROR,
                    ResponseMessage = msg,
                    ResponseDescription = ResponseCode.SERVERERROR.GetDescription(),
                    Data = null
                };

            }

            response = JsonConvert.SerializeObject(userResp);
            _logger.LogToFile($"API RESPONSE : {response}", "MSG");
            return new JsonResult(userResp);
        }


        [HttpPost("SaveConfigurations")]
        [Produces("application/json")]
        public async Task<IActionResult> SaveConfigurations([FromBody]SettingsRequest request) { 
            SystemResponse settingResponse;
            string json;

            //..make sure you have settings to save
            HttpHelpers.Attribute[] attributes = request.Attributes;
            if(attributes.Length == 0) { 
                settingResponse = new() {
                    ResponseCode =  (int)ResponseCode.BADREQUEST,
                    ResponseMessage = ResponseCode.BADREQUEST.GetDescription(),
                    ResponseDescription =   $"Settings list is empty, there's nothing to save",

                };

                json = JsonConvert.SerializeObject(settingResponse);
                _logger.LogToFile($"API RESPONSE : {json}", "MSG");
                return new JsonResult(settingResponse);
            }

            //..include deleted variables
            var includeDeleted = await _parameterService.GetBooleanParameterAsync("includeDeletedObjects");

            //..get branch settings are attached to
            long branchId = request.BranchId == 0 ? 1 : request.BranchId;
            Branch branch = await _branchService.FindBranchByIdAsync(branchId, includeDeleted);
            if(branch == null) { 
                settingResponse = new() {
                    ResponseCode =  (int)ResponseCode.NOTFOUND,
                    ResponseMessage = ResponseCode.NOTFOUND.GetDescription(),
                    ResponseDescription =   $"No branch found with Branch ID '{request.BranchId}'",

                };

                json = JsonConvert.SerializeObject(settingResponse);
                _logger.LogToFile($"API RESPONSE : {json}", "MSG");
                return new JsonResult(settingResponse);
            }

            //..get user posting the settings
            long userId = request.UserId;
            User user = await _userService.FindUserByIdAsync(userId, includeDeleted);
            if(user == null) { 
                settingResponse = new() {
                    ResponseCode =  (int)ResponseCode.NOTFOUND,
                    ResponseMessage = ResponseCode.NOTFOUND.GetDescription(),
                    ResponseDescription =   $"No User found with User ID '{request.BranchId}'",

                };

                json = JsonConvert.SerializeObject(settingResponse);
                _logger.LogToFile($"API RESPONSE : {json}", "MSG");
                return new JsonResult(settingResponse);
            }

             ConfigurationParameter[] parameters = null;
            //..get settings param names
            var paramnames = attributes?.Select(p => p?.ParameterName)
                          .Where(name => !string.IsNullOrEmpty(name))
                          .ToList() ?? [];

            //..get all parameters in the database with these names
            var configs = (await _parameterService.GetParametersAsync(p => paramnames.Contains(p.Parameter) && p.BranchId == branchId)).ToList();
            var inList = new Dictionary<string, object>();
            if(configs.Count != 0) { 
                _logger.LogToFile($"Updating the settings already in the database. A total of {configs.Count} found", "INFO");
                //..update those found in the database
                configs.ForEach(p => { 
                    var attribute = attributes.FirstOrDefault(a => (string)a.ParameterName == p.Parameter);
                    if(attribute == null){
                        inList.Add(attribute.ParameterName, attribute.ParameterValue);
                    } else { 
                        p.ParameterValue = Convert.ToString(attribute.ParameterValue);
                        p.LastModifiedBy = user.Username;
                        p.LastModifiedOn = DateTime.Now;
                    }
                });
                
                //lets update these configurations
               var isUpdated = await _parameterService.UpdateParametersAsync([.. configs]);
                int notUpdated = inList.Count;
                int toBeupdated = configs.Count - notUpdated;
                if(isUpdated){
                     _logger.LogToFile($"A total of {toBeupdated} settings of {configs.Count} updated", "INFO");
                } else {
                    var dmsg =$"Failed to update settings. A total of {toBeupdated} was supposed to be updated";
                    _logger.LogToFile(dmsg, "INFO");
                    settingResponse = new() {
                        ResponseCode =  (int)ResponseCode.NOTFOUND,
                        ResponseMessage = ResponseCode.NOTFOUND.GetDescription(),
                        ResponseDescription = dmsg,

                    };

                    //..no need to continue here, just return
                    json = JsonConvert.SerializeObject(settingResponse);
                    _logger.LogToFile($"API RESPONSE : {json}", "MSG");
                    return new JsonResult(settingResponse);
                }

                //..lets add new ones that are missing
                if(notUpdated > 0) { 
                    string[] keys = [.. inList.Keys];
                    var newAttributes = attributes.Where(a => keys.Contains(a.ParameterName)).ToList();
                    if(newAttributes != null){ 
                        //..all are already updated, just return
                        settingResponse = new() {
                            ResponseCode =  (int)ResponseCode.SUCCESS,
                            ResponseMessage = ResponseCode.SUCCESS.GetDescription(),
                            ResponseDescription =$"Settings saved and updated successfully",

                        };

                        json = JsonConvert.SerializeObject(settingResponse);
                        _logger.LogToFile($"API RESPONSE : {json}", "MSG");
                        return new JsonResult(settingResponse);
                    } 

                    //..if we get here, it means we have new ones to save
                    //..map the new objects to configuration parameters
                    parameters = _mapper.Map<ConfigurationParameter[]>(newAttributes);
                } else { 
                    //..here too all is updated, just return
                    settingResponse = new() {
                        ResponseCode =  (int)ResponseCode.SUCCESS,
                        ResponseMessage = ResponseCode.SUCCESS.GetDescription(),
                        ResponseDescription =$"Settings saved and updated successfully",

                    };

                    json = JsonConvert.SerializeObject(settingResponse);
                    _logger.LogToFile($"API RESPONSE : {json}", "MSG");
                    return new JsonResult(settingResponse);
                }

            } else { 
                //..if we are here, it means all settings are new
                //..map new objects to configuration parameters
                 parameters = _mapper.Map<ConfigurationParameter[]>(attributes);
            }

            //..update records
            _logger.LogToFile($"Adding a total of {parameters.Length} configurations");

            //..make sure the collection is null safe
            var safeParameters = parameters?.Where(p => p != null).ToList() ?? [];
            safeParameters.ForEach(p => { 
                p.BranchId = branchId;
                p.CreatedBy = HashGenerator.EncryptString(user?.EmployeeNo);
                p.LastModifiedBy = HashGenerator.EncryptString(user?.EmployeeNo);
                p.LastModifiedOn = DateTime.Now;
            });

            json = JsonConvert.SerializeObject(parameters);
            _logger.LogToFile($"List of settings : {json}", "INFO");

            var isSuccessful = await _parameterService.InsertParametersAsync(parameters);
            if(isSuccessful){
                settingResponse = new() {
                    ResponseCode =  (int)ResponseCode.SUCCESS,
                    ResponseMessage = ResponseCode.SUCCESS.GetDescription(),
                    ResponseDescription = $"Settings saved and updated successfully"
                };
            } else {
                settingResponse = new() {
                    ResponseCode =  (int)ResponseCode.FAILED,
                    ResponseMessage = ResponseCode.FAILED.GetDescription(),
                    ResponseDescription =   $"An error occurred, could not save settings"
                };
            }

             json = JsonConvert.SerializeObject(settingResponse);
            _logger.LogToFile($"API RESPONSE : {json}", "MSG");
            return new JsonResult(settingResponse);
        }
    }
}
