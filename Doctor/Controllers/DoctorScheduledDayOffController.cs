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
using Doctor.Dtos.ResponseDto.DoctorScheduledDayOffDto;
using Doctor.Dtos.RequestDto.DoctorScheduledDayOffDto;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class DoctorScheduledDayOffController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly DoctorScheduledDayOffService _dayOffService;

        public DoctorScheduledDayOffController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            DoctorScheduledDayOffService dayOffService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _dayOffService = dayOffService;
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("gets-all-doctor-scheduled-day-offs")]
        public async Task<ActionResult<ApiResponse<List<DoctorScheduledDayOffApiResponseDto>>>> GetAllDoctorScheduledDayOffs()
        {
            var apiResponse = new ApiResponse<List<DoctorScheduledDayOffApiResponseDto>>();
            try
            {
                var dayOffList = await _dayOffService.GetAll();
                var mappedDayOffs = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorScheduledDayOff, DoctorScheduledDayOffApiResponseDto>(dayOffList.Result);

                if (dayOffList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDayOffs;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        [HttpGet("get-doctor-scheduled-day-off-by-id")]
        public async Task<ActionResult<ApiResponse<DoctorScheduledDayOffApiResponseDto>>> GetDoctorScheduledDayOffById(int dayOffId)
        {
            var apiResponse = new ApiResponse<DoctorScheduledDayOffApiResponseDto>();
            try
            {
                var dayOff = await _dayOffService.GetById(dayOffId);
                var mappedDayOff = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorScheduledDayOff, DoctorScheduledDayOffApiResponseDto>(dayOff.Result);

                if (dayOff.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDayOff;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        [HttpPost("create-doctor-scheduled-day-off")]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorScheduledDayOff(DoctorScheduledDayOffInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _dayOffService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_insert_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_inserted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        [HttpPut("update-doctor-scheduled-day-off")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorScheduledDayOff(DoctorScheduledDayOffUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _dayOffService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_update_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        [HttpDelete("delete-doctor-scheduled-day-off-by-id")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorScheduledDayOff(int dayOffId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _dayOffService.Delete(dayOffId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_deleted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorScheduledDayOffApiConstantsResponseMessage.dayoff_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}

