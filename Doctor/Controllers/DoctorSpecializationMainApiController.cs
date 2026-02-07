using Doctor.Application.Services;
using Doctor.Dtos.RequestDto.DoctorSpecializationDto;
using Doctor.Dtos.ResponseDto.DoctorSpecializationDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/doctor-specialization")]
    public class DoctorSpecializationMainApiController : ControllerBase
    {
        private readonly DoctorSpecializationService _specializationService;
        private readonly MapperService _mapperService;

        public DoctorSpecializationMainApiController(DoctorSpecializationService specializationService, MapperService mapperService)
        {
            _specializationService = specializationService;
            _mapperService = mapperService;
        }

        [HttpPost]
        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorSpecialization([FromBody] DoctorSpecializationInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _specializationService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Doctor specialization created successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorSpecialization(int id)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _specializationService.Delete(id);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Doctor specialization deleted successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        public async Task<ActionResult<ApiResponse<DoctorSpecializationApiResponseDto>>> GetDoctorSpecializationById(int id)
        {
            var apiResponse = new ApiResponse<DoctorSpecializationApiResponseDto>();
            try
            {
                var specialization = await _specializationService.GetById(id);
                if (specialization.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Specialization not found");
                    return Ok(apiResponse);
                }

                var mappedSpecialization = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorSpecialization, DoctorSpecializationApiResponseDto>(specialization.Result);
                apiResponse.Results = mappedSpecialization;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Specialization retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("by-speciality-id/{specialityId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorSpecializationApiResponseDto>>>> GetBySpecialityId(int specialityId)
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

        [HttpGet("doctor-specialization-list-by-doctor-id/{doctorId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorSpecializationApiResponseDto>>>> GetSpecializationsByDoctorId(int doctorId)
        {
            var apiResponse = new ApiResponse<List<DoctorSpecializationApiResponseDto>>();
            try
            {
                var specializations = await _specializationService.GetByDoctorId(doctorId);
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

        [HttpGet("doctor-specialization-list-by-doctor-id-speciality-id")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorSpecializationApiResponseDto>>>> GetByDoctorIdAndSpecialityId([FromQuery] int doctorId, [FromQuery] int specialityId)
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

        [HttpGet("doctor-specialization-list-by-speciality-id/{specialityId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorSpecializationApiResponseDto>>>> GetBySpecialityIdAlt(int specialityId)
        {
            // Same as by-speciality-id endpoint
            return await GetBySpecialityId(specialityId);
        }

        [HttpGet]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorSpecializationApiResponseDto>>>> GetAllDoctorSpecializations()
        {
            var apiResponse = new ApiResponse<List<DoctorSpecializationApiResponseDto>>();
            try
            {
                var specializations = await _specializationService.GetAll();
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

        [HttpGet("by-doctor-id-sp-id")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorSpecializationApiResponseDto>>>> GetByDoctorIdAndSpecialityIdAlt([FromQuery] int doctorId, [FromQuery] int specialityId)
        {
            // Same as doctor-specialization-list-by-doctor-id-speciality-id
            return await GetByDoctorIdAndSpecialityId(doctorId, specialityId);
        }

        [HttpPut]
        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorSpecialization([FromBody] DoctorSpecializationUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _specializationService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Doctor specialization updated successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }
    }
}

