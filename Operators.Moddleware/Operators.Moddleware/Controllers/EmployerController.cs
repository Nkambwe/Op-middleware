using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Helpers;
using Operators.Moddleware.HttpHelpers;
using Operators.Moddleware.Services;
using Operators.Moddleware.Services.Access;
using Operators.Moddleware.Services.Business;
using Operators.Moddleware.Services.Settings;
using System.Collections.Generic;

namespace Operators.Moddleware.Controllers {

    
    [ApiController]
    [Route("employers")]
    public class EmployerController : BaseController {

        private readonly IServiceLogger _logger;
        private readonly IIndividualEmployerService _individuals;
        private readonly IBusinessEmployerService _businesses;
        private readonly IBusinessContactService _contacts;
        private readonly IBusinessIndustryService _industries;
        private readonly IDistrictService _districts;
        private readonly DecryptionHandler decrypter;

        public EmployerController(IMapper mapper, IServiceLogger logger,  
                                IBranchService branches, 
                                IDistrictService districts,
                                IBusinessEmployerService businesses,
                                IIndividualEmployerService individuals,
                                IBusinessIndustryService industries,
                                IParameterService parameters, 
                                IBusinessContactService contacts,
                                IUserService userService) :
                                base(mapper, branches, parameters, userService) {

            _logger = logger;
            _logger.Channel = $"[EMPLOYERS {DateTime.Now:yyyyMMMdddHHmmss}]";
            _individuals = individuals;
            _businesses = businesses;
            _industries = industries;
            _contacts = contacts;
            _districts = districts;
            decrypter = new(_logger);
        }

        [HttpPost("employers/getAllEmployers")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllEmployers([FromBody] ApplyRequest request) {

            try {

                long userId = request.UserId;
                User user = await UserService.FindUserByIdAsync(userId, true);
                if (user == null) {
                    return NotFound(new SystemResponse<IndividualEmployerDto> {
                        ResponseCode = (int)ResponseCode.NOTFOUND,
                        ResponseMessage = ResponseCode.NOTFOUND.GetDescription(),
                        ResponseDescription = $"No User found with User ID '{request.UserId}'",
                    });
                }

                user = decrypter.DecryptProperties(user, ["Email", "EmployeeNo", "EmployeeName"]);
                 _logger.LogToFile($"Retrieve all employers by '{user.EmployeeNo}'", "MSG");


                int page = request.Page > 0 ? request.Page : 1;
                int pageSize = request.PageSize > 0 ? request.PageSize : 10;

                var individuals = await GetAllIndividualsAsync(request);

                var businesses = await GetAllBusinessesAsync(request);

                //    if (request.Decrypt != null && request.Decrypt.Length > 0)
                //    {
                //        try
                //        {
                //            pagedResult.Entities = decrypter.DecryptProperties(pagedResult.Entities, request.Decrypt);
                //        }
                //        catch (Exception ex)
                //        {
                //            return StatusCode(500, new SystemResponse<Member>
                //            {
                //                ResponseCode = (int)ResponseCode.SERVERERROR,
                //                ResponseMessage = ex.Message,
                //                ResponseDescription = "Oops! Something went wrong"
                //            });
                //        }
                //    }

                //    return Ok(new {
                //        Data = pagedResult.Entities,
                //        TotalCount = pagedResult.Count,
                //        Page = pagedResult.Page,
                //        PageSize = pagedResult.Size
                //    });
            } catch (Exception ex) {
                _logger.LogToFile($"{ex.Message}", "ERROR");
                _logger.LogToFile($"{ex.StackTrace}", "STACKTRACE");

                return StatusCode(500, new SystemResponse<EmployerDto> {
                    ResponseCode = (int)ResponseCode.FAILED,
                    ResponseMessage = ResponseCode.FAILED.GetDescription(),
                    ResponseDescription = "Oops something went wrong"
                });
            }

            return null;
        }

        [HttpPost("employers/getAllBusinessEmployers")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllBusinessEmployers([FromBody] ApplyRequest request) {
            string json;
            PagedResponse<List<BusinessEmployerDto>> response;
            try {
                long userId = request.UserId;
                User user = await UserService.FindUserByIdAsync(userId, true);
                if (user == null) {
                    response = new PagedResponse<List<BusinessEmployerDto>> {
                        ResponseCode = (int)ResponseCode.NOTFOUND,
                        ResponseMessage = ResponseCode.NOTFOUND.GetDescription(),
                        ResponseDescription = $"No User found with User ID '{request.UserId}'",
                        Data = [],
                        Meta = new Meta {
                            TotalCount = 0,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageSize,
                            TotalPages = 0
                        }
                    };

                    json = JsonConvert.SerializeObject(response);
                    _logger.LogToFile($"RESPONSE : {json}", "MSG");
                    return NotFound(response);
                }

                user = decrypter.DecryptProperties(user, ["Email", "EmployeeNo", "EmployeeName"]);
                 _logger.LogToFile($"Retrieve all business employers by '{user.EmployeeNo}'", "MSG");

                response = await GetAllBusinessesAsync(request);
                json = JsonConvert.SerializeObject(response);
                _logger.LogToFile($"RESPONSE : {json}", "MSG");
                 return Ok(response);
            } catch (Exception ex) {
                _logger.LogToFile($"{ex.Message}", "ERROR");
                _logger.LogToFile($"{ex.StackTrace}", "STACKTRACE");

                response =  new PagedResponse<List<BusinessEmployerDto>> {
                    ResponseCode = (int)ResponseCode.FAILED,
                    ResponseMessage = ResponseCode.FAILED.GetDescription(),
                    ResponseDescription = "Oops something went wrong",
                    Data = [],
                    Meta = new Meta {
                        TotalCount = 0,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageSize,
                        TotalPages = 0
                    }
                };

                json = JsonConvert.SerializeObject(response);
                _logger.LogToFile($"RESPONSE : {json}", "MSG");
                return StatusCode(500,response);
            }
        }
        
        [HttpPost("employers/getAllIndividualEmployers")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllIndividualEmployers([FromBody] ApplyRequest request) {
            string json;
            PagedResponse<List<IndividualEmployerDto>> response;
            try {
                long userId = request.UserId;
                User user = await UserService.FindUserByIdAsync(userId, true);
                if (user == null) {
                    response = new PagedResponse<List<IndividualEmployerDto>> {
                        ResponseCode = (int)ResponseCode.NOTFOUND,
                        ResponseMessage = ResponseCode.NOTFOUND.GetDescription(),
                        ResponseDescription = $"No User found with User ID '{request.UserId}'",
                        Data = [],
                        Meta = new Meta {
                            TotalCount = 0,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageSize,
                            TotalPages = 0
                        }
                    };

                    json = JsonConvert.SerializeObject(response);
                    _logger.LogToFile($"RESPONSE : {json}", "MSG");
                    return NotFound(response);
                }

                user = decrypter.DecryptProperties(user, ["Email", "EmployeeNo", "EmployeeName"]);
                 _logger.LogToFile($"Retrieve all individual employers by '{user.EmployeeNo}'", "MSG");

                response = await GetAllIndividualsAsync(request);
                json = JsonConvert.SerializeObject(response);
                _logger.LogToFile($"RESPONSE : {json}", "MSG");
                 return Ok(response);
            } catch (Exception ex) {
                _logger.LogToFile($"{ex.Message}", "ERROR");
                _logger.LogToFile($"{ex.StackTrace}", "STACKTRACE");

                response =  new PagedResponse<List<IndividualEmployerDto>> {
                    ResponseCode = (int)ResponseCode.FAILED,
                    ResponseMessage = ResponseCode.FAILED.GetDescription(),
                    ResponseDescription = "Oops something went wrong",
                    Data = [],
                    Meta = new Meta {
                        TotalCount = 0,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageSize,
                        TotalPages = 0
                    }
                };

                json = JsonConvert.SerializeObject(response);
                _logger.LogToFile($"RESPONSE : {json}", "MSG");
                return StatusCode(500,response);
            }
        }
        
        [HttpPost("employers/createemployer")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateMember([FromBody] ApplyRequest request){ 
            var response = await Task.FromResult(new ApplyRequest());
            return Ok(response);
        }
        
        [HttpPost("employers/updateemployer")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateDriver([FromBody] ApplyRequest request){ 
            var response = await Task.FromResult(new ApplyRequest());
            return Ok(response);
        }
        
        [HttpPost("employers/deleteemployer")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteDriver([FromBody] ApplyRequest request){ 
            var response = await Task.FromResult(new ApplyRequest());
            return Ok(response);
        }
        protected async Task<PagedResponse<List<BusinessEmployerDto>>> GetAllBusinessesAsync(ApplyRequest request) {
            try {
                
                int page = request.Page > 0 ? request.Page : 1;
                int pageSize = request.PageSize > 0 ? request.PageSize : 10;
                var pagedResult = await _businesses.PageAllAsync(page, pageSize, request.IncludeDeleted);

                List<BusinessEmployerDto> result = [];
                //stry decrypt records
                var employers = pagedResult.Entities;
                if(employers.Count > 0) { 
                    if (request.Decrypt != null && request.Decrypt.Length > 0) {
                        try {
                            pagedResult.Entities = decrypter.DecryptProperties(employers, request.Decrypt);
                        }  catch (Exception ex) {
                            return new PagedResponse<List<BusinessEmployerDto>> {
                                ResponseCode = (int)ResponseCode.SERVERERROR,
                                ResponseMessage = ex.Message,
                                ResponseDescription = "Oops! Something went wrong",
                                Data = result,
                                Meta = new Meta {
                                    TotalCount = 0,
                                    PageSize = request.PageSize,
                                    CurrentPage = request.PageSize,
                                    TotalPages = 0
                                }

                            };
                        }
                    }
                }

                //everything is okay, just no data found
                if(employers.Count > 0) { 
                   return new PagedResponse<List<BusinessEmployerDto>> {
                        ResponseCode = (int)ResponseCode.SUCCESS,
                        ResponseMessage = ResponseCode.SUCCESS.GetDescription(),
                        ResponseDescription = "No data found",
                        Data = result,
                        Meta = new Meta {
                            TotalCount = 0,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageSize,
                            TotalPages = 0
                        }
                   };
                }
                
                //map records to Dtos
                result = employers.Select(Mapper.Map<BusinessEmployerDto>).ToList();
                int totalCount = result.Count;
                 _logger.LogToFile($"Records found {totalCount}", "INFO");

                //..get business contacsts
                var contactIds = employers
                    .Select(e => (long)e.ContactId)
                    .Distinct()
                    .ToArray();

                if (contactIds.Length != 0) {
                    // Fetch contacts in a single async call
                    var cntactsList = await _contacts.GetContactsAsync(false, contactIds);
                    if(cntactsList.Count > 0) { 
                        // Bulk update category names
                        foreach (var employer in result) {
                            var con = cntactsList.FirstOrDefault(c => c.Id == employer.ContactId);
                            if(con != null) { 
                               employer.Contact = Mapper.Map<ContactDto>(con);
                            }
                        }
                    }          
                }

                return new PagedResponse<List<BusinessEmployerDto>> {
                    ResponseCode = (int)ResponseCode.SUCCESS,
                    ResponseMessage = ResponseCode.SUCCESS.GetDescription(),
                    ResponseDescription = "Drivers retrieved successfully",
                    Data = result,
                    Meta = new Meta {
                        TotalCount = totalCount,
                        PageSize = request.PageSize,
                        CurrentPage = request.Page,
                        TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
                    }
                    
                };

            } catch (Exception ex) {
                _logger.LogToFile($"{ex.Message}", "ERROR");
                _logger.LogToFile($"{ex.StackTrace}", "STACKTRACE");

                return new PagedResponse<List<BusinessEmployerDto>> {
                    ResponseCode = (int)ResponseCode.SERVERERROR,
                    ResponseMessage = ex.Message,
                    ResponseDescription = "Oops! Something went wrong",
                    Data = [],
                    Meta = new Meta {
                        TotalCount = 0,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageSize,
                        TotalPages = 0
                    }

                };
            }
        }

        protected async Task<PagedResponse<List<IndividualEmployerDto>>> GetAllIndividualsAsync(ApplyRequest request) {
            try {
                
                int page = request.Page > 0 ? request.Page : 1;
                int pageSize = request.PageSize > 0 ? request.PageSize : 10;
                var pagedResult = await _individuals.PageAllAsync(page, pageSize, request.IncludeDeleted);

                List<IndividualEmployerDto> result = [];
                //stry decrypt records
                var employers = pagedResult.Entities;
                if(employers.Count > 0) { 
                    if (request.Decrypt != null && request.Decrypt.Length > 0) {
                        try {
                            pagedResult.Entities = decrypter.DecryptProperties(employers, request.Decrypt);
                        }  catch (Exception ex) {
                            return new PagedResponse<List<IndividualEmployerDto>> {
                                ResponseCode = (int)ResponseCode.SERVERERROR,
                                ResponseMessage = ex.Message,
                                ResponseDescription = "Oops! Something went wrong",
                                Data = result,
                                Meta = new Meta {
                                    TotalCount = 0,
                                    PageSize = request.PageSize,
                                    CurrentPage = request.PageSize,
                                    TotalPages = 0
                                }

                            };
                        }
                    }
                }

                //everything is okay, just no data found
                if(employers.Count > 0) { 
                   return new PagedResponse<List<IndividualEmployerDto>> {
                        ResponseCode = (int)ResponseCode.SUCCESS,
                        ResponseMessage = ResponseCode.SUCCESS.GetDescription(),
                        ResponseDescription = "No data found",
                        Data = result,
                        Meta = new Meta {
                            TotalCount = 0,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageSize,
                            TotalPages = 0
                        }
                   };
                }
                
                //map records to Dtos
                result = employers.Select(Mapper.Map<IndividualEmployerDto>).ToList();
                int totalCount = result.Count;
                 _logger.LogToFile($"Records found {totalCount}", "INFO");

                 // Optimize category retrieval with a single query and dictionary lookup
                 var districtIds = employers
                    .Select(d => (long)d.DistrictId)
                    .Distinct()
                    .ToArray();

                 if (districtIds.Length != 0) {
                    // Fetch area in a single async call
                    var districts = await _districts.GetDistrictsAsync(false, districtIds);

                    // Bulk update category names
                    foreach (var driver in result) {
                        if (districts.TryGetValue((long)driver.DistrictId, out var districtName)) {
                            driver.DistrictName = districtName;
                        }
                    }
                 }

                //..get business contacsts
                var industryIds = employers
                    .Select(e => e.IndustryId)
                    .Distinct()
                    .ToArray();

                if (industryIds.Length != 0) {
                    // Fetch contacts in a single async call
                    var industryList = await _industries.GetDistrictsAsync(false, industryIds);
                    if(industryList.Count > 0) {
                        // Bulk update category names
                        foreach (var employer in result) {
                            if (industryList.TryGetValue((long)employer.IndustryId, out var industryName)) {
                                employer.Industry = industryName;
                            }
                        }
                    }          
                }

                return new PagedResponse<List<IndividualEmployerDto>> {
                    ResponseCode = (int)ResponseCode.SUCCESS,
                    ResponseMessage = ResponseCode.SUCCESS.GetDescription(),
                    ResponseDescription = "Drivers retrieved successfully",
                    Data = result,
                    Meta = new Meta {
                        TotalCount = totalCount,
                        PageSize = request.PageSize,
                        CurrentPage = request.Page,
                        TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
                    }
                    
                };

            } catch (Exception ex) {
                _logger.LogToFile($"{ex.Message}", "ERROR");
                _logger.LogToFile($"{ex.StackTrace}", "STACKTRACE");

                return new PagedResponse<List<IndividualEmployerDto>> {
                    ResponseCode = (int)ResponseCode.SERVERERROR,
                    ResponseMessage = ex.Message,
                    ResponseDescription = "Oops! Something went wrong",
                    Data = [],
                    Meta = new Meta {
                        TotalCount = 0,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageSize,
                        TotalPages = 0
                    }

                };
            }
        }

    }

}
