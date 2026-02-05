using AuthenticationSystem.Application.Services;
using AuthenticationSystem.Dtos.RequestDto.PermissionDto;
using AuthenticationSystem.Dtos.ResponseDto.PermissionDto;
using AuthenticationSystem.Utility;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using Utility.ApiResponse;

namespace AuthenticationSystem.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PermissionController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly PermissionService _permissionService;

        public PermissionController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            PermissionService permissionService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _permissionService = permissionService;
        }

        [HttpGet("gets-all-permissions")]
        public async Task<ActionResult<ApiResponse<List<PermissionApiResponseDto>>>> GetAllPermissions()
        {
            var apiResponse = new ApiResponse<List<PermissionApiResponseDto>>();
            try
            {
                var permissions = await _permissionService.GetAll();
                var mappedPermissions = await _mapperService.MapList<Permission, PermissionApiResponseDto>(permissions.Result);
                if (permissions.Result == null || permissions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.permission_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedPermissions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.permission_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.permission_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("get-permission-by-id")]
        public async Task<ActionResult<ApiResponse<PermissionApiResponseDto>>> GetPermissionById(int permissionId)
        {
            var apiResponse = new ApiResponse<PermissionApiResponseDto>();
            try
            {
                var permission = await _permissionService.GetById(permissionId);
                var mappedPermission = await _mapperService.MapSingle<Permission, PermissionApiResponseDto>(permission.Result);
                if (permission.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.permission_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedPermission;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.permission_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.permission_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpPost("create-permission")]
        public async Task<ActionResult<ApiResponse<int>>> CreatePermission(PermissionInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _permissionService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.permission_insert_success_message);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                        return Ok(apiResponse);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.permission_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.permission_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [HttpPut("update-permission")]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePermission(PermissionUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _permissionService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.permission_update_success_message);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                        return Ok(apiResponse);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.permission_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.permission_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [HttpDelete("delete-permission")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePermission(int permissionId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _permissionService.Delete(permissionId);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.permission_delete_success_message);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
                        return Ok(apiResponse);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, AuthApiConstantsResponseMessage.permission_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, AuthApiConstantsResponseMessage.permission_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }
    }
}

