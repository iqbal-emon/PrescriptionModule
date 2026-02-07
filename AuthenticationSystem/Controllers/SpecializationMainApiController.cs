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

namespace AuthenticationSystem.Controllers
{
    [ApiController]
    [Route("api/app/specialization")]
    public class SpecializationMainApiController : ControllerBase
    {
        private readonly SpecializationService _specializationService;
        private readonly MapperService _mapperService;

        public SpecializationMainApiController(SpecializationService specializationService, MapperService mapperService)
        {
            _specializationService = specializationService;
            _mapperService = mapperService;
        }

        [HttpPost]
        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreateSpecialization([FromBody] SpecializationInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _specializationService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, SpecializationApiConstantsResponseMessage.specialization_insert_success_message);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, SpecializationApiConstantsResponseMessage.specialization_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, SpecializationApiConstantsResponseMessage.specialization_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        public async Task<ActionResult<ApiResponse<SpecializationApiResponseDto>>> GetSpecializationById(int id)
        {
            var apiResponse = new ApiResponse<SpecializationApiResponseDto>();
            try
            {
                var specialization = await _specializationService.GetById(id);
                if (specialization.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, SpecializationApiConstantsResponseMessage.specialization_null_of_get_list);
                    return Ok(apiResponse);
                }

                var mappedSpecialization = await _mapperService.MapSingle<Entities.EntityClass.Specialization, SpecializationApiResponseDto>(specialization.Result);
                apiResponse.Results = mappedSpecialization;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, SpecializationApiConstantsResponseMessage.specialization_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, SpecializationApiConstantsResponseMessage.specialization_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("by-speciality-id/{specialityId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<SpecializationApiResponseDto>>>> GetBySpecialityId(int specialityId)
        {
            var apiResponse = new ApiResponse<List<SpecializationApiResponseDto>>();
            try
            {
                var specializations = await _specializationService.GetBySpecialityId(specialityId);
                if (specializations.Result == null || specializations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecializationApiResponseDto>(), SpecializationApiConstantsResponseMessage.specialization_null_of_get_list);
                    return Ok(apiResponse);
                }

                var mappedSpecializations = await _mapperService.MapList<Entities.EntityClass.Specialization, SpecializationApiResponseDto>(specializations.Result);
                apiResponse.Results = mappedSpecializations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, SpecializationApiConstantsResponseMessage.specialization_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecializationApiResponseDto>(), SpecializationApiConstantsResponseMessage.specialization_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<SpecializationApiResponseDto>>>> GetAllSpecializations()
        {
            var apiResponse = new ApiResponse<List<SpecializationApiResponseDto>>();
            try
            {
                var specializations = await _specializationService.GetAll();
                if (specializations.Result == null || specializations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecializationApiResponseDto>(), SpecializationApiConstantsResponseMessage.specialization_null_of_get_list);
                    return Ok(apiResponse);
                }

                var mappedSpecializations = await _mapperService.MapList<Entities.EntityClass.Specialization, SpecializationApiResponseDto>(specializations.Result);
                apiResponse.Results = mappedSpecializations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, SpecializationApiConstantsResponseMessage.specialization_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecializationApiResponseDto>(), SpecializationApiConstantsResponseMessage.specialization_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("by-specialty-id/{specialityId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<SpecializationApiResponseDto>>>> GetBySpecialtyId(int specialityId)
        {
            // Alternative endpoint - same as by-speciality-id
            return await GetBySpecialityId(specialityId);
        }

        [HttpGet("filtering")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<SpecializationApiResponseDto>>>> GetFilteredSpecializations([FromQuery] string searchTerm = "")
        {
            var apiResponse = new ApiResponse<List<SpecializationApiResponseDto>>();
            try
            {
                var specializations = await _specializationService.GetAll();
                if (specializations.Result == null || specializations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecializationApiResponseDto>(), SpecializationApiConstantsResponseMessage.specialization_null_of_get_list);
                    return Ok(apiResponse);
                }

                var mappedSpecializations = await _mapperService.MapList<Entities.EntityClass.Specialization, SpecializationApiResponseDto>(specializations.Result);
                
                // Apply search filter if provided
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    mappedSpecializations = mappedSpecializations
                        .Where(s => s.SpecializationName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                   (s.Description != null && s.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
                        .ToList();
                }

                apiResponse.Results = mappedSpecializations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, SpecializationApiConstantsResponseMessage.specialization_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecializationApiResponseDto>(), SpecializationApiConstantsResponseMessage.specialization_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpPut]
        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateSpecialization([FromBody] SpecializationUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _specializationService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, SpecializationApiConstantsResponseMessage.specialization_update_success_message);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, SpecializationApiConstantsResponseMessage.specialization_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, SpecializationApiConstantsResponseMessage.specialization_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteSpecialization(int id)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _specializationService.Delete(id);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, SpecializationApiConstantsResponseMessage.specialization_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, SpecializationApiConstantsResponseMessage.specialization_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, SpecializationApiConstantsResponseMessage.specialization_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}

