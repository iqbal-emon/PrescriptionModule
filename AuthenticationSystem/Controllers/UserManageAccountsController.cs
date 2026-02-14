using AuthenticationSystem.Application.Services;
using AuthenticationSystem.Dtos.RequestDto;
using AuthenticationSystem.Dtos.RequestDto.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Security.Cryptography;
using System.Text;
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

        // Test endpoint to verify routing is working
        [HttpGet("test-route")]
        [AllowAnonymous]
        public ActionResult<string> TestRoute()
        {
            Log.Information("TestRoute endpoint hit - routing is working!");
            return Ok("UserManageAccountsController routing is working!");
        }

        [HttpPost("reset-password")]
        [AllowAnonymous] // Temporarily allow anonymous to test if authorization is the issue
        public async Task<ActionResult<ApiResponse<bool>>> ResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            // Debug: Log that the endpoint was hit
            System.Diagnostics.Debug.WriteLine("=== ResetPassword endpoint HIT! ===");
            Console.WriteLine("=== ResetPassword endpoint HIT! ===");
            Log.Information("ResetPassword endpoint called - Method: {Method}, Path: {Path}", 
                HttpContext.Request.Method, HttpContext.Request.Path);
            
            var apiResponse = new ApiResponse<bool>();
            try
            {
                // Check ModelState for validation errors
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .Select(x => new { Field = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage) })
                        .ToList();
                    
                    Log.Warning("ModelState validation failed. Errors: {Errors}", 
                        System.Text.Json.JsonSerializer.Serialize(errors));
                    
                    var errorMessage = string.Join(", ", errors.SelectMany(e => e.Errors));
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, 
                        $"Validation failed: {errorMessage}");
                    return BadRequest(apiResponse);
                }
                
                // Log raw request info
                Log.Information("Request Content-Type: {ContentType}, ContentLength: {ContentLength}", 
                    HttpContext.Request.ContentType, HttpContext.Request.ContentLength);
                
                // Check if request is null (model binding failed)
                if (request == null)
                {
                    // Try to read raw body to see what was sent
                    HttpContext.Request.EnableBuffering();
                    HttpContext.Request.Body.Position = 0;
                    string rawBody = "";
                    try
                    {
                        using (var reader = new System.IO.StreamReader(HttpContext.Request.Body, System.Text.Encoding.UTF8, leaveOpen: true))
                        {
                            rawBody = await reader.ReadToEndAsync();
                            HttpContext.Request.Body.Position = 0;
                        }
                    }
                    catch { }
                    
                    Log.Warning("ResetPassword: Request is null. Raw body: {RawBody}, Content-Type: {ContentType}", 
                        rawBody, HttpContext.Request.ContentType);
                    System.Diagnostics.Debug.WriteLine($"ERROR: Request is NULL. Raw body: {rawBody}");
                    Console.WriteLine($"ERROR: Request is NULL. Raw body: {rawBody}");
                    
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, 
                        "Invalid request format - request body is required. Expected JSON format: { userId: string, newPassword: string }");
                    return BadRequest(apiResponse);
                }
                
                // Debug: Log the request
                //System.Diagnostics.Debug.WriteLine($"ResetPassword request - UserName: '{request?.UserName}', UserId: '{request?.UserId}', NewPassword: {(request?.NewPassword != null ? "SET" : "NULL")}");
                //Console.WriteLine($"ResetPassword request - UserName: '{request?.UserName}', UserId: '{request?.UserId}', NewPassword: {(request?.NewPassword != null ? "SET" : "NULL")}");
                //Log.Information("ResetPassword request received - UserName: {UserName}, UserId: {UserId}, HasNewPassword: {HasNewPassword}", 
                //    request?.UserName, request?.UserId, request?.NewPassword != null);
                
                // Support both UserName and UserId (from frontend)
                Entities.EntityClass.User user = null;
                
                //if (!string.IsNullOrWhiteSpace(request.UserName))
                //{
                //    var userResponse = await _userService.GetByUserName(request.UserName);
                //    if (userResponse?.Result == null)
                //    {
                //        ApiResponseHelper.SetFailedResponse(apiResponse, false, "User not found");
                //        return Ok(apiResponse);
                //    }
                //    user = userResponse.Result;
                //}
                 if (!string.IsNullOrWhiteSpace(request.UserId))
                {
                    // Try to parse userId as int
                    if (int.TryParse(request.UserId, out int userId))
                    {
                        var userResponse = await _userService.GetById(userId);
                        if (userResponse?.Result == null)
                        {
                            ApiResponseHelper.SetFailedResponse(apiResponse, false, "User not found");
                            return Ok(apiResponse);
                        }
                        user = userResponse.Result;
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, false, "Invalid user ID format");
                        return Ok(apiResponse);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, "UserName or UserId is required");
                    return Ok(apiResponse);
                }

                // Handle both camelCase (from frontend) and PascalCase
                string newPassword = request.NewPassword;
                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    //Log.Warning("NewPassword is null or empty. UserId: {UserId}, UserName: {UserName}", 
                    //    request.UserId, request.UserName);
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, 
                        "NewPassword is required and cannot be empty");
                    return BadRequest(apiResponse);
                }

                // Hash the new password
                var hashedPassword = HashPassword(newPassword);

                // Update user password
                var updateDto = new UserUpdateRequestDto
                {
                    Id = user.UserID,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    PasswordHash = hashedPassword,
                    ContactNo = user.ContactNo,
                    RoleId = user.RoleId,
                    IsActive = user.IsActive
                };

                var updateResponse = await _userService.Update(updateDto);
                if (updateResponse?.Result > 0)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, true, "Password updated successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, "Failed to update password");
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error resetting password: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        private string HashPassword(string password)
        {
            // SHA256 hash (matches HashPassword method from AuthController)
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
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

