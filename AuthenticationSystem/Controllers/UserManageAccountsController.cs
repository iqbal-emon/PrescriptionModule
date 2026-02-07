using AuthenticationSystem.Application.Services;
using AuthenticationSystem.Dtos.RequestDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utility.ApiResponse;
using Utility.Response;

namespace AuthenticationSystem.Controllers
{
    [Route("api/2025-02/user-manage-accounts")]
    [ApiController]
    public class UserManageAccountsController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthUserService _authUserService;

        public UserManageAccountsController(UserService userService, AuthUserService authUserService)
        {
            _userService = userService;
            _authUserService = authUserService;
        }

        [HttpPost("check-user-exist-by-user-name")]
        public async Task<ActionResult<ApiResponse<bool>>> CheckUserExistByUserName([FromQuery] string mobileNo)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (string.IsNullOrWhiteSpace(mobileNo))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, "Mobile number is required");
                    return Ok(apiResponse);
                }

                var userResponse = await _userService.GetByUserName(mobileNo);
                var exists = userResponse?.Result != null;

                ApiResponseHelper.SetSuccessResponse(apiResponse, exists, exists ? "User exists" : "User does not exist", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error checking user existence: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("user-roles/{userId}")]
        public async Task<ActionResult<ApiResponse<List<string>>>> GetUserRoles(int userId)
        {
            var apiResponse = new ApiResponse<List<string>>();
            try
            {
                var permissions = await _authUserService.GetUserPermissions(userId);
                ApiResponseHelper.SetSuccessResponse(apiResponse, permissions, "User roles retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<string>(), $"Error retrieving user roles: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<ApiResponse<bool>>> ResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.NewPassword))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, "UserName and NewPassword are required");
                    return Ok(apiResponse);
                }

                var userResponse = await _userService.GetByUserName(request.UserName);
                if (userResponse?.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, "User not found");
                    return Ok(apiResponse);
                }

                // TODO: Implement password reset logic
                ApiResponseHelper.SetFailedResponse(apiResponse, false, "Password reset functionality not yet implemented");
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error resetting password: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPost("save-otp-for-verify-user-later")]
        public async Task<ActionResult<ApiResponse<bool>>> SaveOtpForVerifyUserLater([FromBody] SaveOtpRequestDto request)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                // TODO: Implement OTP saving logic
                // This should save OTP to database for later verification
                ApiResponseHelper.SetFailedResponse(apiResponse, false, "OTP save functionality not yet implemented");
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error saving OTP: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPost("send-otp")]
        public async Task<ActionResult<ApiResponse<bool>>> SendOtp([FromBody] SendOtpRequestDto request)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.MobileNo))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, "Mobile number is required");
                    return Ok(apiResponse);
                }

                // TODO: Implement OTP sending logic
                // This should generate OTP and send via SMS
                ApiResponseHelper.SetFailedResponse(apiResponse, false, "OTP sending functionality not yet implemented");
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error sending OTP: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPost("signup-user")]
        public async Task<ActionResult<ApiResponse<SignUpResponseDto>>> SignupUser([FromBody] SignUpRequestDto request)
        {
            var apiResponse = new ApiResponse<SignUpResponseDto>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "UserName and Password are required");
                    return Ok(apiResponse);
                }

                // Check if user exists
                var existingUser = await _userService.GetByUserName(request.UserName);
                if (existingUser?.Result != null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "User already exists");
                    return Ok(apiResponse);
                }

                // TODO: Implement user signup logic
                ApiResponseHelper.SetFailedResponse(apiResponse, null, "User signup functionality not yet implemented");
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error during signup: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPost("user-password-changes-script")]
        public async Task<ActionResult<ApiResponse<bool>>> UserPasswordChangesScript([FromBody] ChangePasswordRequestDto request)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.NewPassword))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, "UserName and NewPassword are required");
                    return Ok(apiResponse);
                }

                // TODO: Implement password change logic
                ApiResponseHelper.SetFailedResponse(apiResponse, false, "Password change functionality not yet implemented");
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error changing password: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPost("verify-otp")]
        public async Task<ActionResult<ApiResponse<bool>>> VerifyOtp([FromBody] VerifyOtpRequestDto request)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.MobileNo) || string.IsNullOrWhiteSpace(request.Otp))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, "Mobile number and OTP are required");
                    return Ok(apiResponse);
                }

                // TODO: Implement OTP verification logic
                ApiResponseHelper.SetFailedResponse(apiResponse, false, "OTP verification functionality not yet implemented");
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error verifying OTP: {ex.Message}");
            }
            return Ok(apiResponse);
        }
    }

    // DTOs
    //public class ResetPasswordRequestDto
    //{
    //    public string UserName { get; set; }
    //    public string NewPassword { get; set; }
    //    public string OldPassword { get; set; }
    //}

    public class SaveOtpRequestDto
    {
        public string MobileNo { get; set; }
        public string Otp { get; set; }
    }

    public class SendOtpRequestDto
    {
        public string MobileNo { get; set; }
    }

    //public class SignUpRequestDto
    //{
    //    public string UserName { get; set; }
    //    public string Password { get; set; }
    //    public string Email { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public string Name { get; set; }
    //    public string RoleId { get; set; }
    //}

    //public class SignUpResponseDto
    //{
    //    public int UserId { get; set; }
    //    public string UserName { get; set; }
    //    public bool Success { get; set; }
    //    public string Message { get; set; }
    //}

    public class ChangePasswordRequestDto
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class VerifyOtpRequestDto
    {
        public string MobileNo { get; set; }
        public string Otp { get; set; }
    }
}

