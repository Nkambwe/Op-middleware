using AutoMapper;
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

    [ApiController]
    [Route("middleware")]
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

        [HttpPost("drivers/getAllDrivers")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllDrivers([FromBody] ApplyRequest request) {
            PagedResponse<List<DriverDto>> response;
             _logger.LogToFile($"Retrieving drivers list", "INFO");
            string json;
            try {
                // Get user posting the settings
                long userId = request.UserId;
                User user = await UserService.FindUserByIdAsync(userId, true);
                if (user == null)
                {
                    response = new()
                    {
                        ResponseCode = (int)ResponseCode.NOTFOUND,
                        ResponseMessage = ResponseCode.NOTFOUND.GetDescription(),
                        ResponseDescription = $"No User found with User ID '{request.UserId}'",
                    };
                    json = JsonConvert.SerializeObject(response);
                    _logger.LogToFile($"RESPONSE : {json}", "MSG");
                    return new JsonResult(response);
                }

                // Get paginated results with total count
                var result = await _drivers.PageAllAsync(request.Page, request.PageSize, request.IncludeDeleted, m => m.IsActive); 
                var records = result.Entities.ToList();
                int totalCount = result.Count;
                 _logger.LogToFile($"Records found {totalCount}", "INFO");

                // Decrypt columns required
                List<DriverDto> drivers = new();
                string[] decrypt = request.Decrypt;

                // First decrypt any fields if needed
                if (decrypt is { Length: > 0 }) {
                    try {
                        records = decrypter.DecryptProperties(records, decrypt);
                    } catch (Exception ex) {
                        string msg = $"{ex.Message}";

                        response = new() {
                            ResponseCode = (int)ResponseCode.SERVERERROR,
                            ResponseMessage = msg,
                            ResponseDescription = "Oops! Something went wrong"
                        };

                        return new JsonResult(response);
                    }
                }

                // Ensure records are not null before processing
                if (records?.Count > 0) {
                    drivers = records.Select(Mapper.Map<DriverDto>).ToList();
                }
        
                // Return paginated response
                response = new() {
                    ResponseCode = (int)ResponseCode.SUCCESS,
                    ResponseMessage = ResponseCode.SUCCESS.GetDescription(),
                    ResponseDescription = "Drivers retrieved successfully",
                    Data = drivers,
                    Meta = new Meta {
                        TotalCount = totalCount,
                        PageSize = request.PageSize,
                        CurrentPage = request.Page,
                        TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
                    }
                };
        
                return new JsonResult(response);
            } catch (Exception ex) {
                _logger.LogToFile($"{ex.Message}", "ERROR");
                _logger.LogToFile($"{ex.StackTrace}", "STACKTRACE");
                response = new() {
                    ResponseCode = (int)ResponseCode.FAILED,
                    ResponseMessage = ResponseCode.FAILED.GetDescription(),
                    ResponseDescription = "Oops something went wrong"
                };
                return new JsonResult(response);
            }
        }
        
        [HttpPost("drivers/getDriver")]
        [Produces("application/json")]
        public async Task<IActionResult> GetDrivers([FromBody] DriverRequest request) {
            SystemResponse<DriverDto> response;
             _logger.LogToFile($"Retrieving driver record", "INFO");
            string json;
            try {
                // Get user posting the settings
                long userId = request.UserId;
                User user = await UserService.FindUserByIdAsync(userId, true);
                if (user == null)
                {
                    response = new()
                    {
                        ResponseCode = (int)ResponseCode.NOTFOUND,
                        ResponseMessage = ResponseCode.NOTFOUND.GetDescription(),
                        ResponseDescription = $"No User found with User ID '{request.UserId}'",
                    };
                    json = JsonConvert.SerializeObject(response);
                    _logger.LogToFile($"RESPONSE : {json}", "MSG");
                    return new JsonResult(response);
                }

                // Get paginated results with total count
                Driver result = await _drivers.GetDriverAsync(request.DriverId); 
                if(result == null) { 
                    response = new() {
                        ResponseCode = (int)ResponseCode.NOTFOUND,
                        ResponseMessage = ResponseCode.NOTFOUND.GetDescription(),
                        ResponseDescription = $"No driver found with User ID '{request.UserId}'",
                    };
                    
                    _logger.LogToFile($"RESPONSE : {response}", "MSG");
                    return new JsonResult(response);    
                }
                json = JsonConvert.SerializeObject(result);
                 _logger.LogToFile($"Record :: {json}", "INFO");

                // Decrypt columns required
                string[] decrypt = request.Decrypt;

                // First decrypt any fields if needed
                if (decrypt is { Length: > 0 }) {
                    try {
                        result = decrypter.DecryptProperties(result, decrypt);
                    } catch (Exception ex) {
                        string msg = $"{ex.Message}";

                        response = new() {
                            ResponseCode = (int)ResponseCode.SERVERERROR,
                            ResponseMessage = msg,
                            ResponseDescription = "Oops! Something went wrong"
                        };

                        return new JsonResult(response);
                    }
                }

                // Map record to dto
                var driverDto = Mapper.Map<DriverDto>(result);
        
                // Return paginated response
                response = new() {
                    ResponseCode = (int)ResponseCode.SUCCESS,
                    ResponseMessage = ResponseCode.SUCCESS.GetDescription(),
                    ResponseDescription = "Driver retrieve successfully",
                    Data = driverDto
                };
        
                return new JsonResult(response);
            } catch (Exception ex) {
                _logger.LogToFile($"{ex.Message}", "ERROR");
                _logger.LogToFile($"{ex.StackTrace}", "STACKTRACE");
                response = new() {
                    ResponseCode = (int)ResponseCode.FAILED,
                    ResponseMessage = ResponseCode.FAILED.GetDescription(),
                    ResponseDescription = "Oops something went wrong"
                };
                return new JsonResult(response);
            }
        }

    }

}
