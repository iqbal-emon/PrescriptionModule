using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Services;
using Notification.Dtos.ResponseDto.NotificationDto;
using Notification.Utility;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace Notification.Controllers
{
    [ApiController]
    [Route("api/app/notification")]
    public class NotificationMainApiController : ControllerBase
    {
        private readonly NotificationService _notificationService;
        private readonly MapperService _mapperService;

        public NotificationMainApiController(NotificationService notificationService, MapperService mapperService)
        {
            _notificationService = notificationService;
            _mapperService = mapperService;
        }

        [HttpGet("by-user-id/{userId}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<List<NotificationApiResponseDto>>>> GetNotificationsByUserId(int userId, [FromQuery] string role = "")
        {
            var apiResponse = new ApiResponse<List<NotificationApiResponseDto>>();
            try
            {
                var notifications = await _notificationService.GetByUserId(userId, role);
                if (notifications.Result == null || notifications.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<NotificationApiResponseDto>(), NotificationResponseMessage.common_null_of_get_list);
                    return Ok(apiResponse);
                }

                var mappedNotifications = await _mapperService.MapList<Entities.EntityClass.Notification, NotificationApiResponseDto>(notifications.Result);
                apiResponse.Results = mappedNotifications;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, NotificationResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<NotificationApiResponseDto>(), NotificationResponseMessage.common_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}

