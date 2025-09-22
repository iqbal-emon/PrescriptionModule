using Investigation.Application.Services;
using Investigation.Dtos.RequestDto.InvestigationDto;
using Investigation.Dtos.ResponseDto.InvestigationDto;
using Investigation.Utility;
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

namespace Investigation.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class InvestigationController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly InvestigationService _investigationService;
        public InvestigationController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            InvestigationService investigationService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _investigationService = investigationService;
        }

        [Authorize(Policy = PermissionConstants.InvestigationGetAll)]
        [HttpGet("gets-all-investigation")]
        public async Task<ActionResult<ApiResponse<List<InvestigationApiResponseDto>>>> GetAllInvestigations()
        {
            var apiResponse = new ApiResponse<List<InvestigationApiResponseDto>>();
            try
            {
                var investigations = await _investigationService.GetAll();
                var mappedInvestigations = await _mapperService.MapList<Entities.EntityClass.Investigation, InvestigationApiResponseDto>(investigations.Result);

                if (investigations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, InvestigationApiConstantsResponseMessage.investigation_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedInvestigations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, InvestigationApiConstantsResponseMessage.investigation_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, InvestigationApiConstantsResponseMessage.investigation_see_try_catch);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.InvestigationGetAll)]
        [HttpGet("gets-bookmarks-investigation")]
        public async Task<ActionResult<ApiResponse<List<InvestigationApiResponseDto>>>> GetHighlyUsedInvestigation()
        {
            var apiResponse = new ApiResponse<List<InvestigationApiResponseDto>>();
            try
            {
                var investigations = await _investigationService.GetBookMarks();
                var mappedInvestigations = await _mapperService.MapList<Entities.EntityClass.Investigation, InvestigationApiResponseDto>(investigations.Result);

                if (investigations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, InvestigationApiConstantsResponseMessage.investigation_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedInvestigations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, InvestigationApiConstantsResponseMessage.investigation_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, InvestigationApiConstantsResponseMessage.investigation_see_try_catch);
            }
            return Ok(apiResponse);
        }



        [Authorize(Policy = PermissionConstants.InvestigationGetAll)]
        [HttpGet("gets-all-investigation-by-name")]
        public async Task<ActionResult<ApiResponse<List<InvestigationApiResponseDto>>>> GetAllInvestigationByName(string? investigationName = null)
        {
            var apiResponse = new ApiResponse<List<InvestigationApiResponseDto>>();
            try
            {
                var investigations = await _investigationService.GetAllInvestigationByName(investigationName);
                var mappedInvestigations = await _mapperService.MapList<Entities.EntityClass.Investigation, InvestigationApiResponseDto>(investigations.Result);

                if (investigations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, InvestigationApiConstantsResponseMessage.investigation_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedInvestigations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, InvestigationApiConstantsResponseMessage.investigation_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, InvestigationApiConstantsResponseMessage.investigation_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.InvestigationGetId)]
        [HttpGet("get-investigation-by-id")]
        public async Task<ActionResult<ApiResponse<InvestigationApiResponseDto>>> GetInvestigationById(int investigationId)
        {
            var apiResponse = new ApiResponse<InvestigationApiResponseDto>();
            try
            {
                var investigation = await _investigationService.GetById(investigationId);
                var mappedInvestigation = await _mapperService.MapSingle<Entities.EntityClass.Investigation, InvestigationApiResponseDto>(investigation.Result);
                if (investigation.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, InvestigationApiConstantsResponseMessage.investigation_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedInvestigation;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, InvestigationApiConstantsResponseMessage.investigation_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, InvestigationApiConstantsResponseMessage.investigation_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.InvestigationCreate)]
        [HttpPost("create-investigation")]
        public async Task<ActionResult<ApiResponse<int>>> CreateInvestigation(InvestigationInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _investigationService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, InvestigationApiConstantsResponseMessage.investigation_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, InvestigationApiConstantsResponseMessage.investigation_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, InvestigationApiConstantsResponseMessage.investigation_see_try_catch);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.InvestigationUpdate)]
        [HttpPut("update-investigation")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateInvestigation(InvestigationUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _investigationService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, InvestigationApiConstantsResponseMessage.investigation_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, InvestigationApiConstantsResponseMessage.investigation_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, InvestigationApiConstantsResponseMessage.investigation_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.FollowupDelete)]
        [HttpDelete("delete-investigation")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteInvestigation(int InvestigationId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _investigationService.Delete(InvestigationId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, InvestigationApiConstantsResponseMessage.investigation_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, InvestigationApiConstantsResponseMessage.investigation_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, InvestigationApiConstantsResponseMessage.investigation_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }




    }


}

