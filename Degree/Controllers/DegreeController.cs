
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Degree.Dtos.RequestDto.DegreeDto;
using Degree.Application.Services;
using Degree.Utility;
using Degree.Dtos.ResponseDto.DegreeDto;

namespace Degree.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class DegreeController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly Degreervice _degreeService;

        public DegreeController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            Degreervice degreeService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _degreeService = degreeService;
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("gets-all-degrees")]
        public async Task<ActionResult<ApiResponse<List<DegreeApiResponseDto>>>> GetAllDegrees()
        {
            var apiResponse = new ApiResponse<List<DegreeApiResponseDto>>();
            try
            {
                var degreeList = await _degreeService.GetAll();
                var mappedDegrees = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.Degree, DegreeApiResponseDto>(degreeList.Result);

                if (degreeList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DegreeApiConstantsResponseMessage.degree_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDegrees;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DegreeApiConstantsResponseMessage.degree_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DegreeApiConstantsResponseMessage.degree_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        [HttpGet("get-degree-by-id")]
        public async Task<ActionResult<ApiResponse<DegreeApiResponseDto>>> GetDegreeById(int degreeId)
        {
            var apiResponse = new ApiResponse<DegreeApiResponseDto>();
            try
            {
                var degree = await _degreeService.GetById(degreeId);
                var mappedDegree = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.Degree, DegreeApiResponseDto>(degree.Result);

                if (degree.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DegreeApiConstantsResponseMessage.degree_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDegree;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DegreeApiConstantsResponseMessage.degree_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DegreeApiConstantsResponseMessage.degree_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        [HttpPost("create-degree")]
        public async Task<ActionResult<ApiResponse<int>>> CreateDegree(DegreeInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _degreeService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DegreeApiConstantsResponseMessage.degree_insert_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DegreeApiConstantsResponseMessage.degree_inserted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DegreeApiConstantsResponseMessage.degree_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        [HttpPut("update-degree")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDegree(DegreeUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _degreeService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DegreeApiConstantsResponseMessage.degree_update_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DegreeApiConstantsResponseMessage.degree_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DegreeApiConstantsResponseMessage.degree_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        [HttpDelete("delete-degree-by-id")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDegree(int degreeId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _degreeService.Delete(degreeId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DegreeApiConstantsResponseMessage.degree_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, DegreeApiConstantsResponseMessage.degree_deleted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, DegreeApiConstantsResponseMessage.degree_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}