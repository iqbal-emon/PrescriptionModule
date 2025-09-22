using Doctor.Application.Services;
using Doctor.Dtos.RequestDto.DoctorDto;
using Doctor.Dtos.ResponseDto.DoctorDto;
using Doctor.Utility;
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

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class DoctorController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly DoctorService _doctorService;
        public DoctorController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            DoctorService doctorService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _doctorService = doctorService;
        }

        [Authorize(Policy = PermissionConstants.DoctorGetAll)]
        [HttpGet("gets-all-doctors")]
        public async Task<ActionResult<ApiResponse<List<DoctorApiResponseDto>>>> GetAllDoctors()
        {
            var apiResponse = new ApiResponse<List<DoctorApiResponseDto>>();
            try
            {
                var doctors = await _doctorService.GetAll();
                var mappedDoctors = await _mapperService.MapList<Entities.EntityClass.Doctor, DoctorApiResponseDto>(doctors.Result);
                if (doctors.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorApiConstantsResponseMessage.doctor_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedDoctors;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorApiConstantsResponseMessage.doctor_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorApiConstantsResponseMessage.doctor_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DoctorGetId)]
        [HttpGet("get-doctor-by-id")]
        public async Task<ActionResult<ApiResponse<DoctorApiResponseDto>>> GetDoctorById(int doctorId)
        {
            var apiResponse = new ApiResponse<DoctorApiResponseDto>();
            try
            {
                var doctor = await _doctorService.GetById(doctorId);
                var mappedDoctor = await _mapperService.MapSingle<Entities.EntityClass.Doctor, DoctorApiResponseDto>(doctor.Result);
                if (doctor.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorApiConstantsResponseMessage.doctor_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedDoctor;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorApiConstantsResponseMessage.doctor_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorApiConstantsResponseMessage.doctor_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DoctorGetId)]
        [HttpGet("get-doctor-by-user-id")]
        public async Task<ActionResult<ApiResponse<DoctorApiResponseDto>>> GetByReferenceId(int doctorUserId)
        {
            var apiResponse = new ApiResponse<DoctorApiResponseDto>();
            try
            {
                var doctor = await _doctorService.GetByReferenceId(doctorUserId);
                var mappedDoctor = await _mapperService.MapSingle<Entities.EntityClass.Doctor, DoctorApiResponseDto>(doctor.Result);
                if (doctor.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorApiConstantsResponseMessage.doctor_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedDoctor;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorApiConstantsResponseMessage.doctor_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorApiConstantsResponseMessage.doctor_see_try_catch);
            }
            return Ok(apiResponse);
        }




        [Authorize(Policy = PermissionConstants.DoctorCreate)]
        [HttpPost("create-doctor")]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctor(DoctorInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _doctorService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorApiConstantsResponseMessage.doctor_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorApiConstantsResponseMessage.doctor_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorApiConstantsResponseMessage.doctor_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DoctorUpdate)]
        [HttpPut("update-doctor")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctor(DoctorUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _doctorService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorApiConstantsResponseMessage.doctor_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorApiConstantsResponseMessage.doctor_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorApiConstantsResponseMessage.doctor_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DoctorDelete)]
        [HttpDelete("delete-doctor")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctor(int doctorId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _doctorService.Delete(doctorId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorApiConstantsResponseMessage.doctor_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorApiConstantsResponseMessage.doctor_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorApiConstantsResponseMessage.doctor_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }


    }
}
