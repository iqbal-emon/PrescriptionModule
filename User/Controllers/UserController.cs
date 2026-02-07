using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using System.Runtime.InteropServices;
using User.Application.Services;
using User.Dtos.RequestDto;
using User.Dtos.RequestDto.UserDto;
using User.Dtos.ResponseDto.UserDto;
using User.Utility;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace User.Controllers
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

        [Authorize(Policy = PermissionConstants.UserGetAll)]
        [HttpGet("gets-all-users")]
        public async Task<ActionResult<ApiResponse<List<UserApiResponseDto>>>> GetAllUsers()
        {
            var apiResponse = new ApiResponse<List<UserApiResponseDto>>();
            try
            {
                var users = await _userService.GetAll();
                var mappedUsers = await _mapperService.MapList<Entities.EntityClass.User, UserApiResponseDto>(users.Result);

                if (users.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, UserApiConstantsResponseMessage.user_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedUsers;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, UserApiConstantsResponseMessage.user_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, UserApiConstantsResponseMessage.user_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.UserGetId)]
        [HttpGet("get-user-by-id")]
        public async Task<ActionResult<ApiResponse<UserApiResponseDto>>> GetUserById(int userId)
        {
            var apiResponse = new ApiResponse<UserApiResponseDto>();
            try
            {
                var user = await _userService.GetById(userId);
                var mappedUser = await _mapperService.MapSingle<Entities.EntityClass.User, UserApiResponseDto>(user.Result);

                if (user.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, UserApiConstantsResponseMessage.user_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedUser;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, UserApiConstantsResponseMessage.user_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, UserApiConstantsResponseMessage.user_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.UserGetId)]
        [HttpGet("get-user-by-referenceid-role")]
        public async Task<ActionResult<ApiResponse<UserApiResponseDto>>> GetUserByRoleAndReferenceId(int referenceId, string role,int tenantId)
        {
            var apiResponse = new ApiResponse<UserApiResponseDto>();
            try
            {
                var user = await _userService.GetByRoleAndReferenceId(referenceId, role, tenantId);
                var mappedUser = await _mapperService.MapSingle<Entities.EntityClass.User, UserApiResponseDto>(user.Result);

                if (user.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, UserApiConstantsResponseMessage.user_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedUser;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, UserApiConstantsResponseMessage.user_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, UserApiConstantsResponseMessage.user_see_try_catch);
            }
            return Ok(apiResponse);
        }



        [Authorize(Policy = PermissionConstants.UserCreate)]
        [HttpPost("create-user")]
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
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, UserApiConstantsResponseMessage.user_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, UserApiConstantsResponseMessage.User_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, UserApiConstantsResponseMessage.user_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.UserUpdate)]
        [HttpPut("update-user")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateUser(UserUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var response = await _userService.Update(request);

                    

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, UserApiConstantsResponseMessage.user_update_success_message);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                        return Ok(apiResponse);
                    }
                    }
                    catch (Exception ex)
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, UserApiConstantsResponseMessage.user_see_try_catch);
                        return Ok(apiResponse);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, UserApiConstantsResponseMessage.user_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, UserApiConstantsResponseMessage.user_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.UserDelete)]
        [HttpDelete("delete-user-by-id")]
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
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, UserApiConstantsResponseMessage.user_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, UserApiConstantsResponseMessage.user_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, UserApiConstantsResponseMessage.user_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
