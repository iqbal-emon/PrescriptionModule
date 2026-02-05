using AuthenticationSystem.Application.Services;
using AuthenticationSystem.Dtos.RequestDto.RolePermissionDto;
using AuthenticationSystem.Dtos.ResponseDto.RolePermissionDto;
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
    public class RolePermissionController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly RolePermissionService _rolePermissionService;

        public RolePermissionController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            RolePermissionService rolePermissionService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _rolePermissionService = rolePermissionService;
        }

        [HttpGet("gets-all-role-permissions")]
        public async Task<ActionResult<ApiResponse<List<RolePermissionApiResponseDto>>>> GetAllRolePermissions()
        {
            var apiResponse = new ApiResponse<List<RolePermissionApiResponseDto>>();
            try
            {
                var rolePermissions = await _rolePermissionService.GetAll();
                var mappedRolePermissions = await _mapperService.MapList<RolePermission, RolePermissionApiResponseDto>(rolePermissions.Result);
                if (rolePermissions.Result == null || rolePermissions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.rolePermission_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedRolePermissions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.rolePermission_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.rolePermission_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("get-role-permission-by-id")]
        public async Task<ActionResult<ApiResponse<RolePermissionApiResponseDto>>> GetRolePermissionById(int rolePermissionId)
        {
            var apiResponse = new ApiResponse<RolePermissionApiResponseDto>();
            try
            {
                var rolePermission = await _rolePermissionService.GetById(rolePermissionId);
                var mappedRolePermission = await _mapperService.MapSingle<RolePermission, RolePermissionApiResponseDto>(rolePermission.Result);
                if (rolePermission.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.rolePermission_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedRolePermission;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.rolePermission_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.rolePermission_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("get-role-permissions-by-role-id")]
        public async Task<ActionResult<ApiResponse<List<RolePermissionApiResponseDto>>>> GetRolePermissionsByRoleId(int roleId)
        {
            var apiResponse = new ApiResponse<List<RolePermissionApiResponseDto>>();
            try
            {
                var rolePermissions = await _rolePermissionService.GetByRoleId(roleId);
                var mappedRolePermissions = await _mapperService.MapList<RolePermission, RolePermissionApiResponseDto>(rolePermissions.Result);
                if (rolePermissions.Result == null || rolePermissions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.rolePermission_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedRolePermissions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.rolePermission_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.rolePermission_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("get-role-permissions-by-permission-id")]
        public async Task<ActionResult<ApiResponse<List<RolePermissionApiResponseDto>>>> GetRolePermissionsByPermissionId(int permissionId)
        {
            var apiResponse = new ApiResponse<List<RolePermissionApiResponseDto>>();
            try
            {
                var rolePermissions = await _rolePermissionService.GetByPermissionId(permissionId);
                var mappedRolePermissions = await _mapperService.MapList<RolePermission, RolePermissionApiResponseDto>(rolePermissions.Result);
                if (rolePermissions.Result == null || rolePermissions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.rolePermission_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedRolePermissions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.rolePermission_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.rolePermission_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpPost("create-role-permission")]
        public async Task<ActionResult<ApiResponse<int>>> CreateRolePermission(RolePermissionInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _rolePermissionService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.rolePermission_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.rolePermission_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.rolePermission_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [HttpPut("update-role-permission")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateRolePermission(RolePermissionUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _rolePermissionService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.rolePermission_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.rolePermission_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.rolePermission_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [HttpDelete("delete-role-permission")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteRolePermission(int rolePermissionId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _rolePermissionService.Delete(rolePermissionId);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.rolePermission_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, AuthApiConstantsResponseMessage.rolePermission_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, AuthApiConstantsResponseMessage.rolePermission_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }
    }
}

