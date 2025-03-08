using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Helpers;
using Operators.Moddleware.HttpHelpers;
using Operators.Moddleware.Services;
using Operators.Moddleware.Services.Access;
using Operators.Moddleware.Services.Business;
using Operators.Moddleware.Services.Settings;

namespace Operators.Moddleware.Controllers {

    public class DriverController : BaseController {

        private readonly IServiceLogger _logger;
        private readonly IDriverService _drivers;
        private readonly IDistrictService _districts;
        private readonly IAttachmentService _attachment;
        private readonly DecryptionHandler decrypter;

        public DriverController(IMapper mapper, IServiceLogger logger, 
                                IBranchService branches, 
                                IParameterService parameters, 
                                IDistrictService districts,
                                IAttachmentService attachment,
                                IUserService userService,
                                IDriverService drivers) 
                                : base(mapper, branches, parameters, userService) {

            _logger = logger;
            _logger.Channel = $"[DRIVERS {DateTime.Now:yyyyMMMdddHHmmss}]";
            _drivers = drivers;
            _attachment = attachment;
            _districts = districts;
            decrypter = new(_logger);
        }


        [HttpPost("getAllMembers")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllMembers([FromBody]ApplyRequest request) { 
            IList<Driver> records;
            SystemResponse response;
            string json;
            try{ 
                //..get user posting the settings
                long userId = request.UserId;
                User user = await UserService.FindUserByIdAsync(userId, true);
                if(user == null) { 
                    response = new() {
                        ResponseCode =  (int)ResponseCode.NOTFOUND,
                        ResponseMessage = ResponseCode.NOTFOUND.GetDescription(),
                        ResponseDescription =   $"No User found with User ID '{request.UserId}'",
                    };

                    json = JsonConvert.SerializeObject(response);
                    _logger.LogToFile($"RESPONSE : {json}", "MSG");
                    return new JsonResult(response);
                }

                 //..get this from settings
                var includeDeleted = await ParameterService.GetBooleanParameterAsync("includeDeletedObjects");
                records = await _drivers.GetAllAsync(includeDeleted, d => d.IsActive);

                //..decrypt columns required
                string[] decrypt = request.Decrypt; 

                if(decrypt != null && decrypt.Length > 0) { 

                    try{ 
                        records = decrypter.DecryptProperties(records, request.Decrypt);
                    } catch(Exception ex){ 
                        string msg = $"{ex.Message}";
                        response = new() {
                            ResponseCode =  (int)ResponseCode.SERVERERROR,
                            ResponseMessage = msg,
                            ResponseDescription = "Oops! Something went wrong"
                        };
                    }
                    
                }

            } catch(Exception ex){ 
                 _logger.LogToFile($"{ex.Message}", "ERROR");
                _logger.LogToFile($"{ex.StackTrace}", "STACKTRACE");

                response = new() {
                    ResponseCode =  (int)ResponseCode.FAILED,
                    ResponseMessage = ResponseCode.FAILED.GetDescription(),
                    ResponseDescription ="Oops something went wrong"
                };
            }

            return null;
        }
    }

}
