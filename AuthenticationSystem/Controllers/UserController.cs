using AuthenticationSystem.Application.Services;
using AuthenticationSystem.Dtos.RequestDto.UserDto;
using AuthenticationSystem.Dtos.ResponseDto.UserDto;
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
    public class UserController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly UserService _userService;

        public UserController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            UserService userService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _userService = userService;
        }

        [HttpGet("getsallusers")]
        public async Task<ActionResult<ApiResponse<List<UserApiResponseDto>>>> GetAllUsers()
        {
            var apiResponse = new ApiResponse<List<UserApiResponseDto>>();
            try
            {
                var users = await _userService.GetAll();
                var mappedUsers = await _mapperService.MapList<Entities.EntityClass.User, UserApiResponseDto>(users.Result);
                if (users.Result == null || users.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.user_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedUsers;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.user_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.user_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("getuserbyid")]
        public async Task<ActionResult<ApiResponse<UserApiResponseDto>>> GetUserById(int userId)
        {
            var apiResponse = new ApiResponse<UserApiResponseDto>();
            try
            {
                var user = await _userService.GetById(userId);
                var mappedUser = await _mapperService.MapSingle<Entities.EntityClass.User, UserApiResponseDto>(user.Result);
                if (user.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.user_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedUser;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.user_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.user_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("getuserbyusername")]
        public async Task<ActionResult<ApiResponse<UserApiResponseDto>>> GetUserByUserName(string userName)
        {
            var apiResponse = new ApiResponse<UserApiResponseDto>();
            try
            {
                var user = await _userService.GetByUserName(userName);
                var mappedUser = await _mapperService.MapSingle<Entities.EntityClass.User, UserApiResponseDto>(user.Result);
                if (user.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.user_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedUser;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.user_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.user_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("getuserbyemail")]
        public async Task<ActionResult<ApiResponse<UserApiResponseDto>>> GetUserByEmail(string email)
        {
            var apiResponse = new ApiResponse<UserApiResponseDto>();
            try
            {
                var user = await _userService.GetByEmail(email);
                var mappedUser = await _mapperService.MapSingle<Entities.EntityClass.User, UserApiResponseDto>(user.Result);
                if (user.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.user_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedUser;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.user_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.user_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("getusersbyroleid")]
        public async Task<ActionResult<ApiResponse<List<UserApiResponseDto>>>> GetUsersByRoleId(int roleId)
        {
            var apiResponse = new ApiResponse<List<UserApiResponseDto>>();
            try
            {
                var users = await _userService.GetByRoleId(roleId);
                var mappedUsers = await _mapperService.MapList<Entities.EntityClass.User, UserApiResponseDto>(users.Result);
                if (users.Result == null || users.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.user_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedUsers;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.user_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.user_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpPost("createuser")]
        public async Task<ActionResult<ApiResponse<int>>> CreateUser(UserInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _userService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.user_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.user_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.user_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [HttpPut("updateuser")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateUser(UserUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _userService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.user_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.user_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.user_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [HttpDelete("deleteuser")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteUser(int userId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _userService.Delete(userId);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.user_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, AuthApiConstantsResponseMessage.user_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, AuthApiConstantsResponseMessage.user_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }
    }
}

