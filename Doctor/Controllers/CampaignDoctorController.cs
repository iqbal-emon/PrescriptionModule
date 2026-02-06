using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doctor.Application.Services;
using Doctor.Utility;
using Doctor.Dtos.ResponseDto.CampaignDoctorDto;
using Doctor.Dtos.RequestDto.CampaignDoctorDto;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class CampaignDoctorController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly CampaignDoctorService _campaignDoctorService;

        public CampaignDoctorController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            CampaignDoctorService campaignDoctorService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _campaignDoctorService = campaignDoctorService;
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("gets-all-campaign-doctors")]
        public async Task<ActionResult<ApiResponse<List<CampaignDoctorApiResponseDto>>>> GetAllCampaignDoctors()
        {
            var apiResponse = new ApiResponse<List<CampaignDoctorApiResponseDto>>();
            try
            {
                var campaignDoctorList = await _campaignDoctorService.GetAll();
                var mappedCampaignDoctors = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.CampaignDoctor, CampaignDoctorApiResponseDto>(campaignDoctorList.Result);

                if (campaignDoctorList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedCampaignDoctors;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        [HttpGet("get-campaign-doctor-by-id")]
        public async Task<ActionResult<ApiResponse<CampaignDoctorApiResponseDto>>> GetCampaignDoctorById(int campaignDoctorId)
        {
            var apiResponse = new ApiResponse<CampaignDoctorApiResponseDto>();
            try
            {
                var campaignDoctor = await _campaignDoctorService.GetById(campaignDoctorId);
                var mappedCampaignDoctor = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.CampaignDoctor, CampaignDoctorApiResponseDto>(campaignDoctor.Result);

                if (campaignDoctor.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedCampaignDoctor;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        [HttpPost("create-campaign-doctor")]
        public async Task<ActionResult<ApiResponse<int>>> CreateCampaignDoctor(CampaignDoctorInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _campaignDoctorService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_insert_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_inserted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        [HttpPut("update-campaign-doctor")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateCampaignDoctor(CampaignDoctorUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _campaignDoctorService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_update_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        [HttpDelete("delete-campaign-doctor-by-id")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteCampaignDoctor(int campaignDoctorId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _campaignDoctorService.Delete(campaignDoctorId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_deleted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("get-campaign-doctor-list-by-doctor-id")]
        public async Task<ActionResult<ApiResponse<List<CampaignDoctorApiResponseDto>>>> GetCampaignDoctorListByDoctorId(int doctorId)
        {
            var apiResponse = new ApiResponse<List<CampaignDoctorApiResponseDto>>();
            try
            {
                var campaignDoctorList = await _campaignDoctorService.GetByDoctorId(doctorId);
                var mappedCampaignDoctors = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.CampaignDoctor, CampaignDoctorApiResponseDto>(campaignDoctorList.Result);

                if (campaignDoctorList.Result == null || campaignDoctorList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedCampaignDoctors;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, CampaignDoctorApiConstantsResponseMessage.campaigndoctor_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}

