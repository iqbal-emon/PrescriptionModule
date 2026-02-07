using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doctor.Application.Services;
using Doctor.Utility;
using Doctor.Dtos.ResponseDto.DoctorSpecializationDto;
using Doctor.Dtos.RequestDto.DoctorSpecializationDto;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class DoctorSpecializationController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly DoctorSpecializationService _specializationService;

        public DoctorSpecializationController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            DoctorSpecializationService specializationService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _specializationService = specializationService;
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("gets-all-doctor-specializations")]
        public async Task<ActionResult<ApiResponse<List<DoctorSpecializationApiResponseDto>>>> GetAllDoctorSpecializations()
        {
            var apiResponse = new ApiResponse<List<DoctorSpecializationApiResponseDto>>();
            try
            {
                var specializationList = await _specializationService.GetAll();
                var mappedSpecializations = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorSpecialization, DoctorSpecializationApiResponseDto>(specializationList.Result);

                if (specializationList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorSpecializationApiConstantsResponseMessage.specialization_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSpecializations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorSpecializationApiConstantsResponseMessage.specialization_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorSpecializationApiConstantsResponseMessage.specialization_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        [HttpGet("get-doctor-specialization-by-id")]
        public async Task<ActionResult<ApiResponse<DoctorSpecializationApiResponseDto>>> GetDoctorSpecializationById(int specializationId)
        {
            var apiResponse = new ApiResponse<DoctorSpecializationApiResponseDto>();
            try
            {
                var specialization = await _specializationService.GetById(specializationId);
                var mappedSpecialization = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorSpecialization, DoctorSpecializationApiResponseDto>(specialization.Result);

                if (specialization.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorSpecializationApiConstantsResponseMessage.specialization_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSpecialization;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorSpecializationApiConstantsResponseMessage.specialization_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorSpecializationApiConstantsResponseMessage.specialization_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        [HttpPost("create-doctor-specialization")]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorSpecialization(DoctorSpecializationInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _specializationService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorSpecializationApiConstantsResponseMessage.specialization_insert_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorSpecializationApiConstantsResponseMessage.specialization_inserted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorSpecializationApiConstantsResponseMessage.specialization_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        [HttpPut("update-doctor-specialization")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorSpecialization(DoctorSpecializationUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _specializationService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorSpecializationApiConstantsResponseMessage.specialization_update_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorSpecializationApiConstantsResponseMessage.specialization_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorSpecializationApiConstantsResponseMessage.specialization_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        [HttpDelete("delete-doctor-specialization-by-id")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorSpecialization(int specializationId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _specializationService.Delete(specializationId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorSpecializationApiConstantsResponseMessage.specialization_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorSpecializationApiConstantsResponseMessage.specialization_deleted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorSpecializationApiConstantsResponseMessage.specialization_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("get-doctor-specialization-list-by-doctor-id")]
        public async Task<ActionResult<ApiResponse<List<DoctorSpecializationApiResponseDto>>>> GetDoctorSpecializationListByDoctorId(int doctorId)
        {
            var apiResponse = new ApiResponse<List<DoctorSpecializationApiResponseDto>>();
            try
            {
                var specializationList = await _specializationService.GetByDoctorId(doctorId);
                var mappedSpecializations = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorSpecialization, DoctorSpecializationApiResponseDto>(specializationList.Result);

                if (specializationList.Result == null || specializationList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorSpecializationApiConstantsResponseMessage.specialization_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSpecializations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorSpecializationApiConstantsResponseMessage.specialization_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorSpecializationApiConstantsResponseMessage.specialization_see_try_catch);
            }
            return Ok(apiResponse);
        }

        // ========== Merged from DoctorSpecializationMainApiController - Backward Compatibility Routes ==========

        [HttpPost("doctor-specialization")]
        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorSpecializationMainApi([FromBody] DoctorSpecializationInsertRequestDto request)
        {
            return await CreateDoctorSpecialization(request);
        }

        [HttpDelete("doctor-specialization/{id}")]
        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorSpecializationMainApi(int id)
        {
            return await DeleteDoctorSpecialization(id);
        }

        [HttpGet("doctor-specialization/{id}")]
        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        public async Task<ActionResult<ApiResponse<DoctorSpecializationApiResponseDto>>> GetDoctorSpecializationByIdMainApi(int id)
        {
            return await GetDoctorSpecializationById(id);
        }

        [HttpGet("doctor-specialization/by-speciality-id/{specialityId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorSpecializationApiResponseDto>>>> GetBySpecialityIdMainApi(int specialityId)
        {
            var apiResponse = new ApiResponse<List<DoctorSpecializationApiResponseDto>>();
            try
            {
                var specializations = await _specializationService.GetBySpecialityId(specialityId);
                if (specializations.Result == null || specializations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorSpecializationApiResponseDto>(), "No specializations found");
                    return Ok(apiResponse);
                }

                var mappedSpecializations = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorSpecialization, DoctorSpecializationApiResponseDto>(specializations.Result);
                apiResponse.Results = mappedSpecializations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Specializations retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorSpecializationApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("doctor-specialization/doctor-specialization-list-by-doctor-id/{doctorId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorSpecializationApiResponseDto>>>> GetSpecializationsByDoctorIdMainApi(int doctorId)
        {
            return await GetDoctorSpecializationListByDoctorId(doctorId);
        }

        [HttpGet("doctor-specialization/doctor-specialization-list-by-doctor-id-speciality-id")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorSpecializationApiResponseDto>>>> GetByDoctorIdAndSpecialityIdMainApi([FromQuery] int doctorId, [FromQuery] int specialityId)
        {
            var apiResponse = new ApiResponse<List<DoctorSpecializationApiResponseDto>>();
            try
            {
                var specializations = await _specializationService.GetByDoctorIdAndSpecialityId(doctorId, specialityId);
                if (specializations.Result == null || specializations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorSpecializationApiResponseDto>(), "No specializations found");
                    return Ok(apiResponse);
                }

                var mappedSpecializations = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorSpecialization, DoctorSpecializationApiResponseDto>(specializations.Result);
                apiResponse.Results = mappedSpecializations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Specializations retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorSpecializationApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("doctor-specialization/doctor-specialization-list-by-speciality-id/{specialityId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorSpecializationApiResponseDto>>>> GetBySpecialityIdAltMainApi(int specialityId)
        {
            // Same as by-speciality-id endpoint
            return await GetBySpecialityIdMainApi(specialityId);
        }

        [HttpGet("doctor-specialization")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorSpecializationApiResponseDto>>>> GetAllDoctorSpecializationsMainApi()
        {
            return await GetAllDoctorSpecializations();
        }

        [HttpGet("doctor-specialization/by-doctor-id-sp-id")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorSpecializationApiResponseDto>>>> GetByDoctorIdAndSpecialityIdAltMainApi([FromQuery] int doctorId, [FromQuery] int specialityId)
        {
            // Same as doctor-specialization-list-by-doctor-id-speciality-id
            return await GetByDoctorIdAndSpecialityIdMainApi(doctorId, specialityId);
        }

        [HttpPut("doctor-specialization")]
        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorSpecializationMainApi([FromBody] DoctorSpecializationUpdateRequestDto request)
        {
            return await UpdateDoctorSpecialization(request);
        }
    }
}

