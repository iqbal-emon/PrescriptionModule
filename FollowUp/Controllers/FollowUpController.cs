
using FollowUp.Application.Services;
using FollowUp.Dtos.RequestDto.FollowUp;
using FollowUp.Dtos.RequestDto.FollowUpDto;
using FollowUp.Dtos.ResponseDto.FollowUp;
using FollowUp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using SharedService.Model;
using System.Data;
using System.Security;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace FolowUp.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class FollowUpController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly FollowUpService _followUpService;
        public FollowUpController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            FollowUpService followUpService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _followUpService = followUpService;
        }
        [Authorize(Policy = PermissionConstants.FollowupGetAll)]
        [HttpGet("gets-all-followup")]
        public async Task<ActionResult<ApiResponse<List<FollowUpApiResponseDto>>>> GetAllFollowUp()
        {
            var apiResponse = new ApiResponse<List<FollowUpApiResponseDto>>();
            try
            {
                var followup = await _followUpService.GetAll();

                var mappedFollowup = await _mapperService.MapList<Entities.EntityClass.FollowUp, FollowUpApiResponseDto>(followup.Result);

                if (followup.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, FollowUpApiConstantsResponseMessage.followUp_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedFollowup;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, FollowUpApiConstantsResponseMessage.followUp_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, FollowUpApiConstantsResponseMessage.followUp_see_try_catch);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.FollowupGetId)]
        [HttpGet("get-followup-by-id")]
        public async Task<ActionResult<ApiResponse<FollowUpApiResponseDto>>> GetFollowUpById(int followupId)
        {
            var apiResponse = new ApiResponse<FollowUpApiResponseDto>();
            try
            {
                var followup = await _followUpService.GetById(followupId);
                var mappedPatients = await _mapperService.MapSingle<Entities.EntityClass.FollowUp, FollowUpApiResponseDto>(followup.Result);
                if (followup.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, FollowUpApiConstantsResponseMessage.followUp_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPatients;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, FollowUpApiConstantsResponseMessage.followUp_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, FollowUpApiConstantsResponseMessage.followUp_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.FollowupGetId)]
        [HttpGet("gets-bookmarks-followup")]
        public async Task<ActionResult<ApiResponse<List<FollowUpApiResponseDto>>>> GetHighlyUsedFollowUp()
        {
            var apiResponse = new ApiResponse<List<FollowUpApiResponseDto>>();
            try
            {
                var followups = await _followUpService.GetBookMarks();

                var mappedSymptoms = await _mapperService.MapList<Entities.EntityClass.FollowUp, FollowUpApiResponseDto>(followups.Result);

                if (followups.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, FollowUpApiConstantsResponseMessage.followUp_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSymptoms;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, FollowUpApiConstantsResponseMessage.followUp_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, FollowUpApiConstantsResponseMessage.followUp_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.FollowupGetId)]
        [HttpGet("gets-all-followup-by-name")]
        public async Task<ActionResult<ApiResponse<List<FollowUpApiResponseDto>>>> GetAllFollowUpByName(string? followUpName = null)
        {
            var apiResponse = new ApiResponse<List<FollowUpApiResponseDto>>();
            try
            {
                var commonFollowUp = await _followUpService.GetAllByName(followUpName);

                var mappedCommonHistories = await _mapperService.MapList<Entities.EntityClass.FollowUp, FollowUpApiResponseDto>(commonFollowUp.Result);

                if (commonFollowUp.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, FollowUpApiConstantsResponseMessage.followUp_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedCommonHistories;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, FollowUpApiConstantsResponseMessage.followUp_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, FollowUpApiConstantsResponseMessage.followUp_see_try_catch);
            }
            return Ok(apiResponse);
        }



        [Authorize(Policy = PermissionConstants.FollowupCreate)]
        [HttpPost("create-followup")]
        public async Task<ActionResult<ApiResponse<int>>> CreateFollowUp(FollowUpInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {

                if (ModelState.IsValid)
                {
                    var response = await _followUpService.Insert(request);
                   
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, FollowUpApiConstantsResponseMessage.followUp_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, FollowUpApiConstantsResponseMessage.followUp_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, FollowUpApiConstantsResponseMessage.followUp_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.FollowupUpdate)]
        [HttpPut("update-followup")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateFollowUp(FollowUpUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _followUpService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, FollowUpApiConstantsResponseMessage.followUp_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, FollowUpApiConstantsResponseMessage.followUp_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, FollowUpApiConstantsResponseMessage.followUp_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.FollowupDelete)]
        [HttpDelete("delete-followup")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFollowUp(int FollowUpId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _followUpService.Delete(FollowUpId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, FollowUpApiConstantsResponseMessage.followUp_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, FollowUpApiConstantsResponseMessage.followUp_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, FollowUpApiConstantsResponseMessage.followUp_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

    }
}
