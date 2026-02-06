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
using Doctor.Dtos.ResponseDto.DoctorFeesSetupDto;
using Doctor.Dtos.RequestDto.DoctorFeesSetupDto;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class DoctorFeesSetupController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly DoctorFeesSetupService _feesSetupService;

        public DoctorFeesSetupController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            DoctorFeesSetupService feesSetupService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _feesSetupService = feesSetupService;
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("gets-all-doctor-fees-setups")]
        public async Task<ActionResult<ApiResponse<List<DoctorFeesSetupApiResponseDto>>>> GetAllDoctorFeesSetups()
        {
            var apiResponse = new ApiResponse<List<DoctorFeesSetupApiResponseDto>>();
            try
            {
                var feesSetupList = await _feesSetupService.GetAll();
                var mappedFeesSetups = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorFeesSetup, DoctorFeesSetupApiResponseDto>(feesSetupList.Result);

                if (feesSetupList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorFeesSetupApiConstantsResponseMessage.feessetup_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedFeesSetups;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorFeesSetupApiConstantsResponseMessage.feessetup_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorFeesSetupApiConstantsResponseMessage.feessetup_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        [HttpGet("get-doctor-fees-setup-by-id")]
        public async Task<ActionResult<ApiResponse<DoctorFeesSetupApiResponseDto>>> GetDoctorFeesSetupById(int feesSetupId)
        {
            var apiResponse = new ApiResponse<DoctorFeesSetupApiResponseDto>();
            try
            {
                var feesSetup = await _feesSetupService.GetById(feesSetupId);
                var mappedFeesSetup = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorFeesSetup, DoctorFeesSetupApiResponseDto>(feesSetup.Result);

                if (feesSetup.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorFeesSetupApiConstantsResponseMessage.feessetup_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedFeesSetup;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorFeesSetupApiConstantsResponseMessage.feessetup_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorFeesSetupApiConstantsResponseMessage.feessetup_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        [HttpPost("create-doctor-fees-setup")]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorFeesSetup(DoctorFeesSetupInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _feesSetupService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorFeesSetupApiConstantsResponseMessage.feessetup_insert_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorFeesSetupApiConstantsResponseMessage.feessetup_inserted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorFeesSetupApiConstantsResponseMessage.feessetup_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        [HttpPut("update-doctor-fees-setup")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorFeesSetup(DoctorFeesSetupUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _feesSetupService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorFeesSetupApiConstantsResponseMessage.feessetup_update_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorFeesSetupApiConstantsResponseMessage.feessetup_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorFeesSetupApiConstantsResponseMessage.feessetup_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        [HttpDelete("delete-doctor-fees-setup-by-id")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorFeesSetup(int feesSetupId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _feesSetupService.Delete(feesSetupId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorFeesSetupApiConstantsResponseMessage.feessetup_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorFeesSetupApiConstantsResponseMessage.feessetup_deleted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorFeesSetupApiConstantsResponseMessage.feessetup_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("get-doctor-fees-setup-list-by-doctor-id")]
        public async Task<ActionResult<ApiResponse<List<DoctorFeesSetupApiResponseDto>>>> GetDoctorFeesSetupListByDoctorId(int doctorId)
        {
            var apiResponse = new ApiResponse<List<DoctorFeesSetupApiResponseDto>>();
            try
            {
                var feesSetupList = await _feesSetupService.GetByDoctorId(doctorId);
                var mappedFeesSetups = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorFeesSetup, DoctorFeesSetupApiResponseDto>(feesSetupList.Result);

                if (feesSetupList.Result == null || feesSetupList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorFeesSetupApiConstantsResponseMessage.feessetup_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedFeesSetups;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorFeesSetupApiConstantsResponseMessage.feessetup_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorFeesSetupApiConstantsResponseMessage.feessetup_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}

