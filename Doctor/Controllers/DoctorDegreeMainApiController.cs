using Doctor.Application.Services;
using Doctor.Dtos.RequestDto.DoctorDegreeDto;
using Doctor.Dtos.ResponseDto.DoctorDegreeDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/app/doctor-degree")]
    public class DoctorDegreeMainApiController : ControllerBase
    {
        private readonly DoctorDegreeService _degreeService;
        private readonly MapperService _mapperService;

        public DoctorDegreeMainApiController(DoctorDegreeService degreeService, MapperService mapperService)
        {
            _degreeService = degreeService;
            _mapperService = mapperService;
        }

        [HttpPost]
        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorDegree([FromBody] DoctorDegreeInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _degreeService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Doctor degree created successfully");
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
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorDegree(int id)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _degreeService.Delete(id);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Doctor degree deleted successfully");
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
        public async Task<ActionResult<ApiResponse<DoctorDegreeApiResponseDto>>> GetDoctorDegreeById(int id)
        {
            var apiResponse = new ApiResponse<DoctorDegreeApiResponseDto>();
            try
            {
                var degree = await _degreeService.GetById(id);
                if (degree.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Degree not found");
                    return Ok(apiResponse);
                }

                var mappedDegree = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorDegree, DoctorDegreeApiResponseDto>(degree.Result);
                apiResponse.Results = mappedDegree;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Degree retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("doctor-degree-list-by-doctor-id/{doctorId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorDegreeApiResponseDto>>>> GetDegreesByDoctorId(int doctorId)
        {
            var apiResponse = new ApiResponse<List<DoctorDegreeApiResponseDto>>();
            try
            {
                var degrees = await _degreeService.GetByDoctorId(doctorId);
                if (degrees.Result == null || degrees.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorDegreeApiResponseDto>(), "No degrees found");
                    return Ok(apiResponse);
                }

                var mappedDegrees = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorDegree, DoctorDegreeApiResponseDto>(degrees.Result);
                apiResponse.Results = mappedDegrees;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Degrees retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorDegreeApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorDegreeApiResponseDto>>>> GetAllDoctorDegrees()
        {
            var apiResponse = new ApiResponse<List<DoctorDegreeApiResponseDto>>();
            try
            {
                var degrees = await _degreeService.GetAll();
                if (degrees.Result == null || degrees.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorDegreeApiResponseDto>(), "No degrees found");
                    return Ok(apiResponse);
                }

                var mappedDegrees = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorDegree, DoctorDegreeApiResponseDto>(degrees.Result);
                apiResponse.Results = mappedDegrees;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Degrees retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorDegreeApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("by-doctor-id/{doctorId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorDegreeApiResponseDto>>>> GetDegreesByDoctorIdAlt(int doctorId)
        {
            // Alternative endpoint - same as doctor-degree-list-by-doctor-id
            return await GetDegreesByDoctorId(doctorId);
        }

        [HttpPut]
        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorDegree([FromBody] DoctorDegreeUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _degreeService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Doctor degree updated successfully");
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

