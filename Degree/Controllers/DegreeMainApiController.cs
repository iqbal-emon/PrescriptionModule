using Degree.Application.Services;
using Degree.Dtos.RequestDto.DegreeDto;
using Degree.Dtos.ResponseDto.DegreeDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace Degree.Controllers
{
    [ApiController]
    [Route("api/2025-02/degree")]
    public class DegreeMainApiController : ControllerBase
    {
        private readonly Degreervice _degreeService;
        private readonly MapperService _mapperService;

        public DegreeMainApiController(Degreervice degreeService, MapperService mapperService)
        {
            _degreeService = degreeService;
            _mapperService = mapperService;
        }

        [HttpPost]
        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreateDegree([FromBody] DegreeInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _degreeService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Degree created successfully");
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
        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        public async Task<ActionResult<ApiResponse<DegreeApiResponseDto>>> GetDegreeById(int id)
        {
            var apiResponse = new ApiResponse<DegreeApiResponseDto>();
            try
            {
                var degree = await _degreeService.GetById(id);
                if (degree.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Degree not found");
                    return Ok(apiResponse);
                }

                var mappedDegree = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.Degree, DegreeApiResponseDto>(degree.Result);
                apiResponse.Results = mappedDegree;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Degree retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DegreeApiResponseDto>>>> GetAllDegrees()
        {
            var apiResponse = new ApiResponse<List<DegreeApiResponseDto>>();
            try
            {
                var degrees = await _degreeService.GetAll();
                if (degrees.Result == null || degrees.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DegreeApiResponseDto>(), "No degrees found");
                    return Ok(apiResponse);
                }

                var mappedDegrees = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.Degree, DegreeApiResponseDto>(degrees.Result);
                apiResponse.Results = mappedDegrees;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Degrees retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DegreeApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPut]
        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDegree([FromBody] DegreeUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _degreeService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Degree updated successfully");
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

