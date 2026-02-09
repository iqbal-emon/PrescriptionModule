using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.MapService;
using Specialization.Application.Services;
using Specialization.Dtos.RequestDto.SpecializationDto;
using Specialization.Dtos.ResponseDto.SpecializationDto;
using Specialization.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace Specialization.Controllers
{
    [ApiController]
    [Route("api/2025-02/specialization")]
    public class SpecializationController : ControllerBase
    {
        private readonly SpecializationService _specializationService;
        private readonly MapperService _mapperService;

        public SpecializationController(SpecializationService specializationService, MapperService mapperService)
        {
            _specializationService = specializationService;
            _mapperService = mapperService;
        }

        [HttpPost]
        [Authorize(Policy = PermissionConstants.SpecialityCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreateSpecialization([FromBody] SpecializationInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _specializationService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Specialization created successfully");
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

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionConstants.SpecialityGetId)]
        public async Task<ActionResult<ApiResponse<SpecializationApiResponseDto>>> GetSpecializationById(int id)
        {
            var apiResponse = new ApiResponse<SpecializationApiResponseDto>();
            try
            {
                var specialization = await _specializationService.GetById(id);
                if (specialization.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Specialization not found");
                    return Ok(apiResponse);
                }

                var mappedSpecialization = await _mapperService.MapSingle<Entities.EntityClass.Specialization, SpecializationApiResponseDto>(specialization.Result);
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
        [Authorize(Policy = PermissionConstants.SpecialityGetAll)]
        public async Task<ActionResult<ApiResponse<List<SpecializationApiResponseDto>>>> GetSpecializationsBySpecialityId(int specialityId)
        {
            var apiResponse = new ApiResponse<List<SpecializationApiResponseDto>>();
            try
            {
                var specializations = await _specializationService.GetBySpecialityId(specialityId);
                if (specializations.Result == null || specializations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecializationApiResponseDto>(), "No specializations found");
                    return Ok(apiResponse);
                }

                var mappedSpecializations = await _mapperService.MapList<Entities.EntityClass.Specialization, SpecializationApiResponseDto>(specializations.Result);
                apiResponse.Results = mappedSpecializations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Specializations retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecializationApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet]
        [Authorize(Policy = PermissionConstants.SpecialityGetAll)]
        public async Task<ActionResult<ApiResponse<List<SpecializationApiResponseDto>>>> GetAllSpecializations()
        {
            var apiResponse = new ApiResponse<List<SpecializationApiResponseDto>>();
            try
            {
                var specializations = await _specializationService.GetAll();
                if (specializations.Result == null || specializations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecializationApiResponseDto>(), "No specializations found");
                    return Ok(apiResponse);
                }

                var mappedSpecializations = await _mapperService.MapList<Entities.EntityClass.Specialization, SpecializationApiResponseDto>(specializations.Result);
                apiResponse.Results = mappedSpecializations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Specializations retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecializationApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        // Removed duplicate route: by-specialty-id (typo variant of by-speciality-id)
        // Use by-speciality-id/{specialityId} instead

        [HttpGet("filtering")]
        [Authorize(Policy = PermissionConstants.SpecialityGetAll)]
        public async Task<ActionResult<ApiResponse<List<SpecializationApiResponseDto>>>> GetSpecializationsFiltering()
        {
            var apiResponse = new ApiResponse<List<SpecializationApiResponseDto>>();
            try
            {
                // Get all specializations that are used (have doctor specializations)
                var specializations = await _specializationService.GetFiltered();
                if (specializations.Result == null || specializations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecializationApiResponseDto>(), "No specializations found");
                    return Ok(apiResponse);
                }

                var mappedSpecializations = await _mapperService.MapList<Entities.EntityClass.Specialization, SpecializationApiResponseDto>(specializations.Result);
                apiResponse.Results = mappedSpecializations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Specializations retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecializationApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPut]
        [Authorize(Policy = PermissionConstants.SpecialityUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateSpecialization([FromBody] SpecializationUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _specializationService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Specialization updated successfully");
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
        [Authorize(Policy = PermissionConstants.SpecialityDelete)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteSpecialization(int id)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _specializationService.Delete(id);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Specialization deleted successfully");
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
    }
}

