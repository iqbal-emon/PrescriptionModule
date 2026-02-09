using AuthenticationSystem.Application.Services;
using AuthenticationSystem.Dtos.RequestDto;
using AuthenticationSystem.Dtos.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utility.ApiResponse;
using Utility.Response;

namespace AuthenticationSystem.Controllers
{
    [Route("api/2025-02/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthUserService _authUserService;
        private readonly IConfiguration _configuration;

        public AuthController(UserService userService, AuthUserService authUserService, IConfiguration configuration)
        {
            _userService = userService;
            _authUserService = authUserService;
            _configuration = configuration;
        }

        [HttpPost("login-api")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> LoginApi([FromBody] UserLoginRequestDto request)
        {
            var apiResponse = new ApiResponse<LoginResponseDto>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Username and password are required");
                    return Ok(apiResponse);
                }

                var userResponse = await _userService.GetByUserName(request.Username);
                if (userResponse?.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Invalid username or password");
                    return Ok(apiResponse);
                }

                // Verify password (simplified - you should use proper password hashing)
                // TODO: Implement proper password verification
                var isValidPassword = VerifyPassword(request.Password, userResponse.Result.PasswordHash);
                if (!isValidPassword)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Invalid username or password");
                    return Ok(apiResponse);
                }

                // Get user permissions/roles
                var permissions = await _authUserService.GetUserPermissions(userResponse.Result.UserID);
                var roles = permissions; // Assuming permissions contain role names

                // Generate tokens
                var accessToken = GenerateJwtToken(userResponse.Result, roles);
                var refreshToken = GenerateRefreshToken(userResponse.Result);

                var loginResponse = new LoginResponseDto
                {
                    UserId = userResponse.Result.UserID,
                    UserName = userResponse.Result.UserName,
                    RoleName = roles,
                    Success = true,
                    Message = "Login successful",
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    LoginType = "standard",
                    UserEmail = userResponse.Result.Email
                };

                ApiResponseHelper.SetSuccessResponse(apiResponse, loginResponse, "Login successful", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error during login: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var apiResponse = new ApiResponse<LoginResponseDto>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Refresh token is required");
                    return Ok(apiResponse);
                }

                // Validate refresh token and extract user info
                var principal = ValidateToken(request.RefreshToken);
                if (principal == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Invalid refresh token");
                    return Ok(apiResponse);
                }

                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Invalid token");
                    return Ok(apiResponse);
                }

                var userResponse = await _userService.GetById(userId);
                if (userResponse?.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "User not found");
                    return Ok(apiResponse);
                }

                var permissions = await _authUserService.GetUserPermissions(userResponse.Result.UserID);
                var roles = permissions;

                var newAccessToken = GenerateJwtToken(userResponse.Result, roles);
                var newRefreshToken = GenerateRefreshToken(userResponse.Result);

                var loginResponse = new LoginResponseDto
                {
                    UserId = userResponse.Result.UserID,
                    UserName = userResponse.Result.UserName,
                    RoleName = roles,
                    Success = true,
                    Message = "Token refreshed successfully",
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                    LoginType = "standard",
                    UserEmail = userResponse.Result.Email
                };

                ApiResponseHelper.SetSuccessResponse(apiResponse, loginResponse, "Token refreshed successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error refreshing token: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPost("verify-access-token")]
        public async Task<ActionResult<ApiResponse<bool>>> VerifyAccessToken([FromBody] VerifyAccessTokenRequestDto request)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.AccessToken))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, "Access token is required");
                    return Ok(apiResponse);
                }

                var principal = ValidateToken(request.AccessToken);
                var isValid = principal != null;

                ApiResponseHelper.SetSuccessResponse(apiResponse, isValid, isValid ? "Token is valid" : "Token is invalid or expired", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error verifying token: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPost("firebase/verify")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> FirebaseVerify([FromBody] FirebaseVerifyRequestDto request)
        {
            var apiResponse = new ApiResponse<LoginResponseDto>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.FirebaseToken) && string.IsNullOrWhiteSpace(request.IdToken))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Firebase token is required");
                    return Ok(apiResponse);
                }

                var token = request.IdToken ?? request.FirebaseToken;

                // Verify Firebase token using stored procedure
                // Note: This requires FirebaseAdmin package to be installed
                // For now, we'll use a simplified approach that checks if user exists
                // You should implement proper Firebase token verification using FirebaseAdmin
                
                // Check if user exists by email from token (simplified - should verify token first)
                // TODO: Implement proper Firebase token verification
                // var firebaseToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
                // var email = firebaseToken.Claims.GetValueOrDefault("email")?.ToString();
                
                // For now, return a placeholder that indicates the endpoint exists
                // The actual implementation should verify the Firebase token and create/login user
                ApiResponseHelper.SetFailedResponse(apiResponse, null, "Firebase verification requires FirebaseAdmin package. Please implement proper token verification.");
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error verifying Firebase token: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        private string GenerateJwtToken(Entities.EntityClass.User user, List<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "YourSuperSecretKeyForJWTTokenGeneration");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken(Entities.EntityClass.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:RefreshKey"] ?? "YourSuperSecretKeyForRefreshTokenGeneration");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), // Refresh token valid for 7 days
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "YourSuperSecretKeyForJWTTokenGeneration");
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            // TODO: Implement proper password verification using BCrypt or similar
            // For now, this is a placeholder
            // You should use BCrypt.Net or similar library
            //return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return false;
        }
    }
}
