﻿using AutoMapper;
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
            if(configs.Count != 0) { 
                _logger.LogToFile($"Updating the settings already in the database. A total of {configs.Count} found", "INFO");
                //..update those found in the database

                List<string> toUpdate = new();
                foreach(var p in configs){ 
                    var attribute = attributes.FirstOrDefault(a => a.ParameterName == p.Parameter);
                    p.ParameterValue = Convert.ToString(attribute.ParameterValue);
                    p.LastModifiedBy = user.Username;
                    p.LastModifiedOn = DateTime.Now;
                    toUpdate.Add(attribute.ParameterName);
                }

                //lets update these configurations
               var isUpdated = await _parameterService.UpdateParametersAsync([.. configs]);
                int toBeupdated = toUpdate.Count;
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
                if(toBeupdated < attributes.Length) { 
                    var newAttributes = attributes.Where(a => !toUpdate.Contains(a.ParameterName)).ToList();
                    if(newAttributes == null){ 
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
                    foreach(var p in parameters){ 
                        p.CreatedBy = user.Username;
                        p.CreatedOn = DateTime.Now;
                        p.LastModifiedBy = user.Username;
                        p.LastModifiedOn = DateTime.Now;
                    }
                    
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

        [HttpPost("getAllSettings")]
        [Produces("application/json")]
        public async Task<IActionResult> getAllSettings([FromBody]GeneralRequest request) { 
            GeneralResponse<HttpHelpers.Attribute> response = null;
            string json = JsonConvert.SerializeObject(request);
             _logger.LogToFile($"REQUEST BODY : {json}", "MSG");

            
            //..include deleted variables
            var includeDeleted = await _parameterService.GetBooleanParameterAsync("includeDeletedObjects");

             //..get branch settings are attached to
            long branchId = request.BranchId == 0 ? 1 : request.BranchId;
            Branch branch = await _branchService.FindBranchByIdAsync(branchId, includeDeleted);
            if(branch == null) { 
                response = new() {
                    ResponseCode =  (int)ResponseCode.NOTFOUND,
                    ResponseMessage = ResponseCode.NOTFOUND.GetDescription(),
                    ResponseDescription =   $"No branch found with Branch ID '{request.BranchId}'",

                };

                json = JsonConvert.SerializeObject(response);
                _logger.LogToFile($"API RESPONSE : {json}", "MSG");
                return new JsonResult(response);
            }

            //..get user posting the settings
            long userId = request.UserId;
            User user = await _userService.FindUserByIdAsync(userId, includeDeleted);
            if(user == null) { 
                response = new() {
                    ResponseCode =  (int)ResponseCode.NOTFOUND,
                    ResponseMessage = ResponseCode.NOTFOUND.GetDescription(),
                    ResponseDescription =   $"No User found with User ID '{request.BranchId}'",

                };

                json = JsonConvert.SerializeObject(response);
                _logger.LogToFile($"API RESPONSE : {json}", "MSG");
                return new JsonResult(response);
            }

            //..make sure user has access to branch records
            if(branchId != user.BranchId){ 
                 response = new() {
                    ResponseCode =  (int)ResponseCode.FORBIDDEN,
                    ResponseMessage = ResponseCode.FORBIDDEN.GetDescription(),
                    ResponseDescription = "User account is not assigned to branch and cannot access these records",

                };

                json = JsonConvert.SerializeObject(response);
                _logger.LogToFile($"API RESPONSE : {json}", "MSG");
                return new JsonResult(response);
            }

            //..get settings
            var settings = await _parameterService.GetAllParametersAsync();
            HttpHelpers.Attribute[] items = _mapper.Map<HttpHelpers.Attribute[]>(settings); 
            if(items == null || items.Length == 0){ 
                items = [];    
            }

            response = new() {
                ResponseCode =  (int)ResponseCode.SUCCESS,
                ResponseMessage = ResponseCode.SUCCESS.GetDescription(),
                ResponseDescription = $"Settings saved and updated successfully",
                Items = [.. items]
            };

            json = JsonConvert.SerializeObject(response);
            _logger.LogToFile($"API RESPONSE : {json}", "MSG");
            return new JsonResult(response);
        }
    }
}
