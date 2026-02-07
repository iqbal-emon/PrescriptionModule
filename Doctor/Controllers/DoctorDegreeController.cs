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
using System.Runtime.InteropServices;
using Doctor.Dtos.ResponseDto.DoctorDegreeDto;
using Doctor.Dtos.RequestDto.DoctorDegreeDto;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class DoctorDegreeController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly DoctorDegreeService _degreeService;

        public DoctorDegreeController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            DoctorDegreeService degreeService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _degreeService = degreeService;
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("gets-all-doctor-degrees")]
        public async Task<ActionResult<ApiResponse<List<DoctorDegreeApiResponseDto>>>> GetAllDoctorDegrees()
        {
            var apiResponse = new ApiResponse<List<DoctorDegreeApiResponseDto>>();
            try
            {
                var degreeList = await _degreeService.GetAll();
                var mappedDegrees = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorDegree, DoctorDegreeApiResponseDto>(degreeList.Result);

                if (degreeList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorDegreeApiConstantsResponseMessage.degree_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDegrees;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorDegreeApiConstantsResponseMessage.degree_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorDegreeApiConstantsResponseMessage.degree_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        [HttpGet("get-doctor-degree-by-id")]
        public async Task<ActionResult<ApiResponse<DoctorDegreeApiResponseDto>>> GetDoctorDegreeById(int degreeId)
        {
            var apiResponse = new ApiResponse<DoctorDegreeApiResponseDto>();
            try
            {
                var degree = await _degreeService.GetById(degreeId);
                var mappedDegree = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorDegree, DoctorDegreeApiResponseDto>(degree.Result);

                if (degree.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorDegreeApiConstantsResponseMessage.degree_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDegree;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorDegreeApiConstantsResponseMessage.degree_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorDegreeApiConstantsResponseMessage.degree_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        [HttpPost("create-doctor-degree")]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorDegree(DoctorDegreeInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _degreeService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorDegreeApiConstantsResponseMessage.degree_insert_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorDegreeApiConstantsResponseMessage.degree_inserted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorDegreeApiConstantsResponseMessage.degree_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        [HttpPut("update-doctor-degree")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorDegree(DoctorDegreeUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _degreeService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorDegreeApiConstantsResponseMessage.degree_update_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorDegreeApiConstantsResponseMessage.degree_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorDegreeApiConstantsResponseMessage.degree_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        [HttpDelete("delete-doctor-degree-by-id")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorDegree(int degreeId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _degreeService.Delete(degreeId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorDegreeApiConstantsResponseMessage.degree_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorDegreeApiConstantsResponseMessage.degree_deleted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorDegreeApiConstantsResponseMessage.degree_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("get-doctor-degree-list-by-doctor-id")]
        public async Task<ActionResult<ApiResponse<List<DoctorDegreeApiResponseDto>>>> GetDoctorDegreeListByDoctorId(int doctorId)
        {
            var apiResponse = new ApiResponse<List<DoctorDegreeApiResponseDto>>();
            try
            {
                var degreeList = await _degreeService.GetByDoctorId(doctorId);
                var mappedDegrees = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorDegree, DoctorDegreeApiResponseDto>(degreeList.Result);

                if (degreeList.Result == null || degreeList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorDegreeApiConstantsResponseMessage.degree_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDegrees;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorDegreeApiConstantsResponseMessage.degree_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorDegreeApiConstantsResponseMessage.degree_see_try_catch);
            }
            return Ok(apiResponse);
        }

        // ========== Merged from DoctorDegreeMainApiController - Backward Compatibility Routes ==========

        [HttpPost("doctor-degree")]
        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorDegreeMainApi([FromBody] DoctorDegreeInsertRequestDto request)
        {
            // Redirect to CreateDoctorDegree method
            return await CreateDoctorDegree(request);
        }

        [HttpDelete("doctor-degree/{id}")]
        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorDegreeMainApi(int id)
        {
            // Redirect to DeleteDoctorDegree method
            return await DeleteDoctorDegree(id);
        }

        [HttpGet("doctor-degree/{id}")]
        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        public async Task<ActionResult<ApiResponse<DoctorDegreeApiResponseDto>>> GetDoctorDegreeByIdMainApi(int id)
        {
            // Redirect to GetDoctorDegreeById method
            return await GetDoctorDegreeById(id);
        }

        [HttpGet("doctor-degree/doctor-degree-list-by-doctor-id/{doctorId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorDegreeApiResponseDto>>>> GetDegreesByDoctorIdMainApi(int doctorId)
        {
            // Redirect to GetDoctorDegreeListByDoctorId method
            return await GetDoctorDegreeListByDoctorId(doctorId);
        }

        [HttpGet("doctor-degree")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorDegreeApiResponseDto>>>> GetAllDoctorDegreesMainApi()
        {
            // Redirect to GetAllDoctorDegrees method
            return await GetAllDoctorDegrees();
        }

        [HttpGet("doctor-degree/by-doctor-id/{doctorId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorDegreeApiResponseDto>>>> GetDegreesByDoctorIdAltMainApi(int doctorId)
        {
            // Alternative endpoint - redirects to GetDoctorDegreeListByDoctorId
            return await GetDoctorDegreeListByDoctorId(doctorId);
        }

        [HttpPut("doctor-degree")]
        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorDegreeMainApi([FromBody] DoctorDegreeUpdateRequestDto request)
        {
            // Redirect to UpdateDoctorDegree method
            return await UpdateDoctorDegree(request);
        }
    }
}

