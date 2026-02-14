using AuthenticationSystem.Application.Services;
using AuthenticationSystem.Dtos.RequestDto;
using AuthenticationSystem.Dtos.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Utility.ApiResponse;
using Utility.Response;

namespace AuthenticationSystem.Controllers
{
    [Route("api/2025-02/user-accounts")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthUserService _authUserService;
        private readonly IConfiguration _configuration;

        public UserAccountsController(UserService userService, AuthUserService authUserService, IConfiguration configuration)
        {
            _userService = userService;
            _authUserService = authUserService;
            _configuration = configuration;
        }

        [HttpPost("decode-jwt")]
        public async Task<ActionResult<ApiResponse<Dictionary<string, object>>>> DecodeJwt([FromBody] DecodeJwtRequestDto request)
        {
            var apiResponse = new ApiResponse<Dictionary<string, object>>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.Token))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Token is required");
                    return Ok(apiResponse);
                }

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(request.Token);
                var claims = jsonToken.Claims.ToDictionary(c => c.Type, c => (object)c.Value);

                ApiResponseHelper.SetSuccessResponse(apiResponse, claims, "JWT decoded successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error decoding JWT: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPost("is-user-exists")]
        public async Task<ActionResult<ApiResponse<bool>>> IsUserExists([FromQuery] string userName)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, "Username is required");
                    return Ok(apiResponse);
                }

                var userResponse = await _userService.GetByUserName(userName);
                var exists = userResponse?.Result != null;

                ApiResponseHelper.SetSuccessResponse(apiResponse, exists, exists ? "User exists" : "User does not exist", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error checking user existence: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login([FromBody] UserLoginRequestDto request)
        {
            // This should redirect to AuthController or use the same logic
            // For now, return a response indicating to use /api/2025-02/auth/login-api
            var apiResponse = new ApiResponse<LoginResponseDto>();
            ApiResponseHelper.SetFailedResponse(apiResponse, null, "Please use /api/2025-02/auth/login-api endpoint");
            return Ok(apiResponse);
        }

        [HttpPost("refresh-access-token")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> RefreshAccessToken([FromBody] RefreshTokenRequestDto request)
        {
            // This should redirect to AuthController or use the same logic
            var apiResponse = new ApiResponse<LoginResponseDto>();
            ApiResponseHelper.SetFailedResponse(apiResponse, null, "Please use /api/2025-02/auth/refresh-token endpoint");
            return Ok(apiResponse);
        }

        //[HttpPost("reset-password")]
        //public async Task<ActionResult<ApiResponse<bool>>> ResetPassword([FromBody] ResetPasswordRequestDto request)
        //{
        //    var apiResponse = new ApiResponse<bool>();
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.NewPassword))
        //        {
        //            ApiResponseHelper.SetFailedResponse(apiResponse, false, "UserName and NewPassword are required");
        //            return Ok(apiResponse);
        //        }

        //        var userResponse = await _userService.GetByUserName(request.UserName);
        //        if (userResponse?.Result == null)
        //        {
        //            ApiResponseHelper.SetFailedResponse(apiResponse, false, "User not found");
        //            return Ok(apiResponse);
        //        }

        //        // TODO: Implement password reset logic
        //        // This requires updating the user's password hash in the database
        //        ApiResponseHelper.SetFailedResponse(apiResponse, false, "Password reset functionality not yet implemented");
        //    }
        //    catch (Exception ex)
        //    {
        //        ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error resetting password: {ex.Message}");
        //    }
        //    return Ok(apiResponse);
        //}

        //[HttpPost("reset-password_App")]
        //public async Task<ActionResult<ApiResponse<bool>>> ResetPasswordApp([FromBody] ResetPasswordRequestDto request)
        //{
        //    // Same as reset-password but for mobile app
        //    return await ResetPassword(request);
        //}

        [HttpPost("signup-user")]
        public async Task<ActionResult<ApiResponse<SignUpResponseDto>>> SignupUser([FromQuery] string password, [FromQuery] string role, [FromBody] SignUpRequestDto request)
        {
            var apiResponse = new ApiResponse<SignUpResponseDto>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(password))
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
                // This requires creating a new user with the provided password and role
                ApiResponseHelper.SetFailedResponse(apiResponse, null, "User signup functionality not yet implemented");
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error during signup: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPost("user-data-remove")]
        public async Task<ActionResult<ApiResponse<bool>>> UserDataRemove([FromQuery] string role, [FromBody] UserDataRemoveRequestDto request)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.UserName))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, "UserName is required");
                    return Ok(apiResponse);
                }

                var userResponse = await _userService.GetByUserName(request.UserName);
                if (userResponse?.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, "User not found");
                    return Ok(apiResponse);
                }

                // TODO: Implement user data removal logic based on role
                // This may require deleting related profile data (Doctor, Patient, Agent)
                ApiResponseHelper.SetFailedResponse(apiResponse, false, "User data removal functionality not yet implemented");
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error removing user data: {ex.Message}");
            }
            return Ok(apiResponse);
        }
    }

    // DTOs
    public class DecodeJwtRequestDto
    {
        public string Token { get; set; }
    }

    public class ResetPasswordRequestDto
    {
        //public string UserName { get; set; }
        public string UserId { get; set; } // Support userId from frontend
        public string NewPassword { get; set; }
        //public string OldPassword { get; set; }
    }

    public class SignUpRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
    }

    public class SignUpResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class UserDataRemoveRequestDto
    {
        public string UserName { get; set; }
    }
}

