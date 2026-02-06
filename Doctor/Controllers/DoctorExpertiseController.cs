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
using System.Runtime.InteropServices;
using Doctor.Dtos.RequestDto.DoctorExpertiseDto;
using Doctor.Dtos.ResponseDto.DoctorExpertiseDto;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class DoctorExpertiseController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly DoctorExpertiseService _expertiseService;

        public DoctorExpertiseController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            DoctorExpertiseService expertiseService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _expertiseService = expertiseService;
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("gets-all-doctor-expertise")]
        public async Task<ActionResult<ApiResponse<List<DoctorExpertiseApiResponseDto>>>> GetAllDoctorExpertises()
        {
            var apiResponse = new ApiResponse<List<DoctorExpertiseApiResponseDto>>();
            try
            {
                var expertiseList = await _expertiseService.GetAll();
                var mappedExpertises = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorExpertise, DoctorExpertiseApiResponseDto>(expertiseList.Result);

                if (expertiseList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorExpertiseApiConstantsResponseMessage.expertise_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedExpertises;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorExpertiseApiConstantsResponseMessage.expertise_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorExpertiseApiConstantsResponseMessage.expertise_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        [HttpGet("get-doctor-expertise-by-id")]
        public async Task<ActionResult<ApiResponse<DoctorExpertiseApiResponseDto>>> GetDoctorExpertiseById(int expertiseId)
        {
            var apiResponse = new ApiResponse<DoctorExpertiseApiResponseDto>();
            try
            {
                var expertise = await _expertiseService.GetById(expertiseId);
                var mappedExpertise = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorExpertise, DoctorExpertiseApiResponseDto>(expertise.Result);

                if (expertise.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorExpertiseApiConstantsResponseMessage.expertise_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedExpertise;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorExpertiseApiConstantsResponseMessage.expertise_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorExpertiseApiConstantsResponseMessage.expertise_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        [HttpPost("create-doctor-expertise")]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorExpertise(DoctorExpertiseInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _expertiseService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorExpertiseApiConstantsResponseMessage.expertise_insert_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorExpertiseApiConstantsResponseMessage.expertise_inserted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorExpertiseApiConstantsResponseMessage.expertise_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        [HttpPut("update-doctor-expertise")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorExpertise(DoctorExpertiseUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _expertiseService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorExpertiseApiConstantsResponseMessage.expertise_update_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorExpertiseApiConstantsResponseMessage.expertise_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorExpertiseApiConstantsResponseMessage.expertise_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        [HttpDelete("delete-doctor-expertise-by-id")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorExpertise(int expertiseId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _expertiseService.Delete(expertiseId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorExpertiseApiConstantsResponseMessage.expertise_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorExpertiseApiConstantsResponseMessage.expertise_deleted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorExpertiseApiConstantsResponseMessage.expertise_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}

