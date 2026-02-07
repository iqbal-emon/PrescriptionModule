using Doctor.Application.Services;
using Doctor.Dtos.RequestDto.DoctorChamberDto;
using Doctor.Dtos.ResponseDto.DoctorChamberDto;
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
    [Route("api/2025-02/doctor-chamber")]
    public class DoctorChamberMainApiController : ControllerBase
    {
        private readonly DoctorChamberService _chamberService;
        private readonly MapperService _mapperService;

        public DoctorChamberMainApiController(DoctorChamberService chamberService, MapperService mapperService)
        {
            _chamberService = chamberService;
            _mapperService = mapperService;
        }

        [HttpPost]
        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorChamber([FromBody] DoctorChamberInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _chamberService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Doctor chamber created successfully");
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

        [HttpPut]
        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorChamber([FromBody] DoctorChamberUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _chamberService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Doctor chamber updated successfully");
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
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorChamber(int id)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _chamberService.Delete(id);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Doctor chamber deleted successfully");
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
        public async Task<ActionResult<ApiResponse<DoctorChamberApiResponseDto>>> GetDoctorChamberById(int id)
        {
            var apiResponse = new ApiResponse<DoctorChamberApiResponseDto>();
            try
            {
                var chamber = await _chamberService.GetById(id);
                if (chamber.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Chamber not found");
                    return Ok(apiResponse);
                }

                var mappedChamber = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorChamber, DoctorChamberApiResponseDto>(chamber.Result);
                apiResponse.Results = mappedChamber;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Chamber retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("doctor-chamber-list-by-doctor-id/{doctorProfileId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorChamberApiResponseDto>>>> GetChambersByDoctorId(int doctorProfileId)
        {
            var apiResponse = new ApiResponse<List<DoctorChamberApiResponseDto>>();
            try
            {
                var chambers = await _chamberService.GetByDoctorId(doctorProfileId);
                if (chambers.Result == null || chambers.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorChamberApiResponseDto>(), "No chambers found");
                    return Ok(apiResponse);
                }

                var mappedChambers = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorChamber, DoctorChamberApiResponseDto>(chambers.Result);
                apiResponse.Results = mappedChambers;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Chambers retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorChamberApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }
    }
}

