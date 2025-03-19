using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Operators.Moddleware.Data.Entities.Access;
using Operators.Moddleware.Data.Entities.Business;
using Operators.Moddleware.Helpers;
using Operators.Moddleware.HttpHelpers;
using Operators.Moddleware.Services;
using Operators.Moddleware.Services.Access;
using Operators.Moddleware.Services.Business;
using Operators.Moddleware.Services.Settings;
using System.Net.Mail;

namespace Operators.Moddleware.Controllers {

    public class MemberController : BaseController {
        
        private readonly IServiceLogger _logger;
        private readonly IMemberService _members;
        private readonly IDistrictService _districts;
        private readonly IAttachmentService _attachment;
        private readonly DecryptionHandler decrypter;

        public MemberController(IMapper mapper, IServiceLogger logger, 
            IBranchService branches, 
            IParameterService parameters, 
            IUserService userService,
            IDistrictService districts,
            IAttachmentService attachment,
            IMemberService members) : 
            base(mapper, branches, parameters, userService) {
            
            _logger = logger;
            _logger.Channel = $"[MEMBERS {DateTime.Now:yyyyMMMdddHHmmss}]";
            _members = members;
            _attachment = attachment;
            _districts = districts;
            decrypter = new(_logger);
        }

        
        [HttpPost("getAllMembers")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllMembers([FromBody] ApplyRequest request) {
            try {
                long userId = request.UserId;
                User user = await UserService.FindUserByIdAsync(userId, true);
                if (user == null)
                {
                    return NotFound(new SystemResponse<Member>
                    {
                        ResponseCode = (int)ResponseCode.NOTFOUND,
                        ResponseMessage = ResponseCode.NOTFOUND.GetDescription(),
                        ResponseDescription = $"No User found with User ID '{request.UserId}'",
                    });
                }

                int page = request.Page > 0 ? request.Page : 1;
                int pageSize = request.PageSize > 0 ? request.PageSize : 10;

                var pagedResult = await _members.PageAllAsync(page, pageSize, request.IncludeDeleted);

                if (request.Decrypt != null && request.Decrypt.Length > 0)
                {
                    try
                    {
                        pagedResult.Entities = decrypter.DecryptProperties(pagedResult.Entities, request.Decrypt);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, new SystemResponse<Member>
                        {
                            ResponseCode = (int)ResponseCode.SERVERERROR,
                            ResponseMessage = ex.Message,
                            ResponseDescription = "Oops! Something went wrong"
                        });
                    }
                }

                return Ok(new {
                    Data = pagedResult.Entities,
                    TotalCount = pagedResult.Count,
                    Page = pagedResult.Page,
                    PageSize = pagedResult.Size
                });
            } catch (Exception ex) {
                _logger.LogToFile($"{ex.Message}", "ERROR");
                _logger.LogToFile($"{ex.StackTrace}", "STACKTRACE");

                return StatusCode(500, new SystemResponse<Member>
                {
                    ResponseCode = (int)ResponseCode.FAILED,
                    ResponseMessage = ResponseCode.FAILED.GetDescription(),
                    ResponseDescription = "Oops something went wrong"
                });
            }
        }

    }

}
