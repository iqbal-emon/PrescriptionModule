using CommonHistory.Application;
using CommonHistory.Application.Services;
using CommonHistory.Dtos.RequestDto.CommonHistoryDto;
using CommonHistory.Dtos.ResponseDto.CommonHistoryDto;
using CommonHistory.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Permission;

namespace CommonHistory.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class CommonHistoryController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly CommonHistoryService _commonHistoryService;

        public CommonHistoryController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            CommonHistoryService commonHistoryService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _commonHistoryService = commonHistoryService;
        }

        [Authorize(Policy = PermissionConstants.CommonHistoryGetAll)]
        [HttpGet("gets-all-common-history")]
        public async Task<ActionResult<ApiResponse<List<CommonHistoryApiResponseDto>>>> GetAllCommonHistory()
        {
            var apiResponse = new ApiResponse<List<CommonHistoryApiResponseDto>>();
            try
            {
                var commonHistories = await _commonHistoryService.GetAll();

                var mappedCommonHistories = await _mapperService.MapList<Entities.EntityClass.CommonHistory, CommonHistoryApiResponseDto>(commonHistories.Result);

                if (commonHistories.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, CommonHistoryApiConstantsReponseMessage.common_history_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedCommonHistories;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, CommonHistoryApiConstantsReponseMessage.common_history_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, CommonHistoryApiConstantsReponseMessage.common_history_see_try_catch);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.CommonHistoryGetAll)]
        [HttpGet("gets-bookmarks-common-histories")]
        public async Task<ActionResult<ApiResponse<List<CommonHistoryApiResponseDto>>>> GetHighlyUsedCommonHistories(int doctorId)
        {
            var apiResponse = new ApiResponse<List<CommonHistoryApiResponseDto>>();
            try
            {
                var commonHistories = await _commonHistoryService.GetBookMarks(doctorId);

                var mappedSymptoms = await _mapperService.MapList<Entities.EntityClass.CommonHistory, CommonHistoryApiResponseDto>(commonHistories.Result);

                if (commonHistories.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, CommonHistoryApiConstantsReponseMessage.common_history_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSymptoms;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, CommonHistoryApiConstantsReponseMessage.common_history_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, CommonHistoryApiConstantsReponseMessage.common_history_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.CommonHistoryGetAll)]
        [HttpGet("gets-all-common-history-by-name")]
        public async Task<ActionResult<ApiResponse<List<CommonHistoryApiResponseDto>>>> GetAllCommonHistoryByName(string? CommonHistoryName = null)
        {
            var apiResponse = new ApiResponse<List<CommonHistoryApiResponseDto>>();
            try
            {
                var commonHistories = await _commonHistoryService.GetAllCommonHistoryByName(CommonHistoryName);

                var mappedCommonHistories = await _mapperService.MapList<Entities.EntityClass.CommonHistory, CommonHistoryApiResponseDto>(commonHistories.Result);

                if (commonHistories.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, CommonHistoryApiConstantsReponseMessage.common_history_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedCommonHistories;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, CommonHistoryApiConstantsReponseMessage.common_history_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, CommonHistoryApiConstantsReponseMessage.common_history_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.CommonHistoryGetId)]
        [HttpGet("get-common-history-by-id")]
        public async Task<ActionResult<ApiResponse<CommonHistoryApiResponseDto>>> GetCommonHistoryById(int commonHistoryId)
        {
            var apiResponse = new ApiResponse<CommonHistoryApiResponseDto>();
            try
            {
                var commonHistory = await _commonHistoryService.GetById(commonHistoryId);
                var mappedCommonHistory = await _mapperService.MapSingle<Entities.EntityClass.CommonHistory, CommonHistoryApiResponseDto>(commonHistory.Result);
                if (commonHistory.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, CommonHistoryApiConstantsReponseMessage.common_history_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedCommonHistory;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, CommonHistoryApiConstantsReponseMessage.common_history_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, CommonHistoryApiConstantsReponseMessage.common_history_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.CommonHistoryCreate)]
        [HttpPost("create-common-history")]
        public async Task<ActionResult<ApiResponse<int>>> CreateCommonHistory(CommonHistoryInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _commonHistoryService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, CommonHistoryApiConstantsReponseMessage.common_history_insert_success_message);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                        return Ok(apiResponse);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, CommonHistoryApiConstantsReponseMessage.common_history_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, CommonHistoryApiConstantsReponseMessage.common_history_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.CommonHistoryUpdate)]
        [HttpPut("update-common-history")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateCommonHistory(CommonHistoryUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _commonHistoryService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, CommonHistoryApiConstantsReponseMessage.common_history_update_success_message);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                        return Ok(apiResponse);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, CommonHistoryApiConstantsReponseMessage.common_history_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, CommonHistoryApiConstantsReponseMessage.common_history_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.CommonHistoryDelete)]
        [HttpDelete("delete-common-history")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteCommonHistory(int commonHistoryId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _commonHistoryService.Delete(commonHistoryId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, CommonHistoryApiConstantsReponseMessage.common_history_delete_success_message);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
                        return Ok(apiResponse);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, CommonHistoryApiConstantsReponseMessage.common_history_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, CommonHistoryApiConstantsReponseMessage.common_history_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
