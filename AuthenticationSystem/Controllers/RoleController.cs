using AuthenticationSystem.Application.Services;
using AuthenticationSystem.Dtos.RequestDto.RoleDto;
using AuthenticationSystem.Dtos.ResponseDto.RoleDto;
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
    public class RoleController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly RoleService _roleService;

        public RoleController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            RoleService roleService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _roleService = roleService;
        }

        [HttpGet("gets-all-roles")]
        public async Task<ActionResult<ApiResponse<List<RoleApiResponseDto>>>> GetAllRoles()
        {
            var apiResponse = new ApiResponse<List<RoleApiResponseDto>>();
            try
            {
                var roles = await _roleService.GetAll();
                var mappedRoles = await _mapperService.MapList<Role, RoleApiResponseDto>(roles.Result);
                if (roles.Result == null || roles.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.role_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedRoles;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.role_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.role_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("get-role-by-id")]
        public async Task<ActionResult<ApiResponse<RoleApiResponseDto>>> GetRoleById(int roleId)
        {
            var apiResponse = new ApiResponse<RoleApiResponseDto>();
            try
            {
                var role = await _roleService.GetById(roleId);
                var mappedRole = await _mapperService.MapSingle<Role, RoleApiResponseDto>(role.Result);
                if (role.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.role_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedRole;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.role_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.role_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpPost("create-role")]
        public async Task<ActionResult<ApiResponse<int>>> CreateRole(RoleInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _roleService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.role_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.role_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.role_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [HttpPut("update-role")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateRole(RoleUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _roleService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.role_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.role_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.role_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [HttpDelete("delete-role")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteRole(int roleId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _roleService.Delete(roleId);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.role_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, AuthApiConstantsResponseMessage.role_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, AuthApiConstantsResponseMessage.role_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }
    }
}

