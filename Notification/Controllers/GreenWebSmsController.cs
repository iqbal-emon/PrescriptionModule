using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Application;
using Notification.Dtos.RequestDto;
using Notification.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace Notification.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class GreenWebSmsController : ControllerBase
    {
        private readonly GreenWebSMSService _greenWebSMSService;
        public GreenWebSmsController(GreenWebSMSService greenWebSMSService)
        {
            _greenWebSMSService = greenWebSMSService;
        }
        [Authorize(Policy = PermissionConstants.MedicationCreate)]
        [HttpPost("send-sms")]
        public async Task<ActionResult<ApiResponse<SmsResponseDto>>> CreateNotification(SmsSendRequestDto request)
        {
            
            var apiResponse = new ApiResponse<SmsResponseDto>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _greenWebSMSService.SendSmsGreenWebAsync(request);

                    if (response.IsSuccess)
                    {
                        apiResponse.Results = response.Result;
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, response.Message);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, response.Result, response.Message);
                        return Ok(apiResponse);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, apiResponse.Results, apiResponse.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, apiResponse.Results, apiResponse.Message);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
