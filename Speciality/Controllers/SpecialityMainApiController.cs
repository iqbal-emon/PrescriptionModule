using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.MapService;
using Speciality.Application.Services;
using Speciality.Dtos.RequestDto.SpecialityDto;
using Speciality.Dtos.ResponseDto.SpecialityDto;
using Speciality.Utility;
using System;
using System.Collections.Generic;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace Speciality.Controllers
{
    [ApiController]
    [Route("api/2025-02/speciality")]
    public class SpecialityMainApiController : ControllerBase
    {
        private readonly SpecialityService _specialityService;
        private readonly MapperService _mapperService;

        public SpecialityMainApiController(SpecialityService specialityService, MapperService mapperService)
        {
            _specialityService = specialityService;
            _mapperService = mapperService;
        }

        [HttpPost]
        [Authorize(Policy = PermissionConstants.SpecialityCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreateSpeciality([FromBody] SpecialityInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _specialityService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Speciality created successfully");
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
        public async Task<ActionResult<ApiResponse<SpecialityApiResponseDto>>> GetSpecialityById(int id)
        {
            var apiResponse = new ApiResponse<SpecialityApiResponseDto>();
            try
            {
                var speciality = await _specialityService.GetById(id);
                if (speciality.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Speciality not found");
                    return Ok(apiResponse);
                }

                var mappedSpeciality = await _mapperService.MapSingle<Entities.EntityClass.Speciality, SpecialityApiResponseDto>(speciality.Result);
                apiResponse.Results = mappedSpeciality;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Speciality retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet]
        [Authorize(Policy = PermissionConstants.SpecialityGetAll)]
        public async Task<ActionResult<ApiResponse<List<SpecialityApiResponseDto>>>> GetAllSpecialities()
        {
            var apiResponse = new ApiResponse<List<SpecialityApiResponseDto>>();
            try
            {
                var specialities = await _specialityService.GetAll();
                if (specialities.Result == null || specialities.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecialityApiResponseDto>(), "No specialities found");
                    return Ok(apiResponse);
                }

                var mappedSpecialities = await _mapperService.MapList<Entities.EntityClass.Speciality, SpecialityApiResponseDto>(specialities.Result);
                apiResponse.Results = mappedSpecialities;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Specialities retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecialityApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPut]
        [Authorize(Policy = PermissionConstants.SpecialityUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateSpeciality([FromBody] SpecialityUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _specialityService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Speciality updated successfully");
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
        public async Task<ActionResult<ApiResponse<bool>>> DeleteSpeciality(int id)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _specialityService.Delete(id);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Speciality deleted successfully");
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

