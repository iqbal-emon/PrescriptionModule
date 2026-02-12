using AuthenticationSystem.Application.Services;
using AuthenticationSystem.Dtos.RequestDto;
using AuthenticationSystem.Dtos.RequestDto.UserDto;
using AuthenticationSystem.Dtos.ResponseDto;
using Doctor.Dtos.RequestDto.DoctorDto;
using Doctor.Dtos.ResponseDto.DoctorDto;
using Entities.EntityClass;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Utility.ApiResponse;
using Utility.Response;

namespace AuthenticationSystem.Controllers
{
    [Route("api/2025-02")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IUserService _authUserService;
        private readonly RoleService _roleService;
        private readonly FirebaseAuthService _firebaseAuthService;
        private readonly IConfiguration _configuration;

        public AuthController(UserService userService, IUserService authUserService, RoleService roleService, 
            FirebaseAuthService firebaseAuthService, IConfiguration configuration)
        {
            _userService = userService;
            _authUserService = authUserService;
            _roleService = roleService;
            _firebaseAuthService = firebaseAuthService;
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

                // Try to get user by username first
                var userResponse = await _userService.GetByUserName(request.Username);
                
                // If not found by username, try by phone number (for mobile login)
                if (userResponse?.Result == null)
                {
                    // Check if username looks like a phone number (contains only digits, +, or spaces)
                    var cleanedPhone = request.Username.Replace("+", "").Replace(" ", "").Replace("-", "").Trim();
                    if (cleanedPhone.All(char.IsDigit) && cleanedPhone.Length >= 10)
                    {
                        userResponse = await _userService.GetByPhoneNo(cleanedPhone);
                    }
                }

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
                var roles = permissions ?? new List<string>();

                // Validate userLoginType if provided (ensure user is logging into correct portal)
                if (request.UserLoginType.HasValue && request.UserLoginType.Value > 0)
                {
                    // Map role names to login types: 1=Patient, 2=Doctor, 3=Agent, 4=Admin
                    int expectedLoginType = 0;
                    var primaryRole = roles.FirstOrDefault()?.ToLower();
                    
                    if (primaryRole == "patient")
                        expectedLoginType = 1;
                    else if (primaryRole == "doctor")
                        expectedLoginType = 2;
                    else if (primaryRole == "agent")
                        expectedLoginType = 3;
                    else if (primaryRole == "admin" || primaryRole == "sgadmin")
                        expectedLoginType = 4;

                    // Validate that the requested login type matches the user's role
                    if (expectedLoginType > 0 && request.UserLoginType.Value != expectedLoginType)
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, null, "Role does not match. You are not authorized for this portal.");
                        return Ok(apiResponse);
                    }
                }

                // Fetch doctor ID if user type is Doctor (same as firebase/verify)
                int? doctorId = null;
                if (userResponse.Result.UserType != null && userResponse.Result.UserType.Equals("Doctor", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        using var httpClient = new HttpClient();
                        var baseUrl = $"{Request.Scheme}://{Request.Host}";
                        var checkEndpoint = $"/api/2025-02/get-doctor-by-user-id?doctorUserId={userResponse.Result.UserID}";
                        var checkRequest = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}{checkEndpoint}");

                        if (Request.Headers.ContainsKey("Authorization"))
                        {
                            var authToken = Request.Headers["Authorization"].ToString();
                            if (!string.IsNullOrEmpty(authToken))
                            {
                                checkRequest.Headers.Add("Authorization", authToken);
                            }
                        }

                        var checkResponse = await httpClient.SendAsync(checkRequest);
                        if (checkResponse.IsSuccessStatusCode)
                        {
                            var checkResponseContent = await checkResponse.Content.ReadAsStringAsync();
                            var checkApiResponse = JsonSerializer.Deserialize<ApiResponse<DoctorApiResponseDto>>(checkResponseContent, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });

                            if (checkApiResponse?.IsSuccess == true && checkApiResponse.Results != null)
                            {
                                doctorId = checkApiResponse.Results.DoctorID;
                                Log.Information("Retrieved DoctorID: {DoctorId} for user UserID: {UserId}", doctorId, userResponse.Result.UserID);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Warning(ex, "Failed to fetch doctor ID for user UserID: {UserId}", userResponse.Result.UserID);
                        // Don't fail login if doctor ID fetch fails
                    }
                }

                // Generate tokens
                var accessToken = GenerateJwtToken(userResponse.Result, roles);
                var refreshToken = GenerateRefreshToken(userResponse.Result);

                // Build login response with same structure as firebase/verify
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
                    UserEmail = userResponse.Result.Email,
                    DoctorId = doctorId
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

        [AllowAnonymous]
        [HttpPost("firebase/verify")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> FirebaseVerify([FromBody] FirebaseVerifyRequestDto? request)
        {
            var apiResponse = new ApiResponse<LoginResponseDto>();
            try
            {
                // Log received request for debugging
                Log.Information("FirebaseVerify endpoint called. Request received: {@Request}", request);
                Log.Information("ModelState.IsValid: {IsValid}", ModelState.IsValid);
                
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    var errorMessage = string.Join("; ", errors);
                    Log.Warning("ModelState validation failed. Errors: {Errors}", errorMessage);
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Validation error: {errorMessage}");
                    return BadRequest(apiResponse);
                }

                if (request == null)
                {
                    Log.Warning("FirebaseVerify: Request body is null");
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Request body is required");
                    return BadRequest(apiResponse);
                }

                // Log received parameters
                Log.Information("FirebaseVerify: Received IdToken: {HasIdToken}, FirebaseToken: {HasFirebaseToken}", 
                    !string.IsNullOrWhiteSpace(request.IdToken), 
                    !string.IsNullOrWhiteSpace(request.FirebaseToken));
                Log.Debug("FirebaseVerify: IdToken value: {IdToken}, FirebaseToken value: {FirebaseToken}", 
                    request.IdToken ?? "null", 
                    request.FirebaseToken ?? "null");

                if (string.IsNullOrWhiteSpace(request.FirebaseToken) && string.IsNullOrWhiteSpace(request.IdToken))
                {
                    Log.Warning("FirebaseVerify: Both tokens are null or empty");
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Firebase token is required");
                    return BadRequest(apiResponse);
                }

                var token = request.IdToken ?? request.FirebaseToken;
                Log.Information("FirebaseVerify: Using token: {TokenLength} characters", token?.Length ?? 0);

                // Verify Firebase ID token using FirebaseAuthService
                var decoded = await _firebaseAuthService.VerifyTokenAsync(token);
                if (decoded == null)
                {
                    Log.Warning("Firebase token verification failed: Token is invalid or expired");
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Invalid or expired Firebase token");
                    return Unauthorized(apiResponse);
                }
                
                Log.Information("Firebase token verified successfully. UID: {Uid}", decoded.Uid);

                // Extract user info from Firebase token
                decoded.Claims.TryGetValue("email", out var emailObj);
                decoded.Claims.TryGetValue("name", out var nameObj);
                decoded.Claims.TryGetValue("picture", out var pictureObj);

                string? email = emailObj?.ToString();
                string? name = nameObj?.ToString();
                string? picture = pictureObj?.ToString();

                if (string.IsNullOrWhiteSpace(email))
                {
                    Log.Warning("Firebase token does not contain email");
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Invalid token: email not found");
                    return BadRequest(apiResponse);
                }

                // Determine login type from Firebase claims
                string loginType = "google";
                if (decoded.Claims.TryGetValue("firebase", out var firebaseObj) && firebaseObj != null)
                {
                    var firebaseClaim = firebaseObj.ToString();
                    if (firebaseClaim.Contains("sign_in_provider"))
                    {
                        // Try to extract provider from claims
                        loginType = firebaseClaim.Contains("google.com") ? "google" :
                                   firebaseClaim.Contains("password") ? "email" :
                                   firebaseClaim.Contains("phone") ? "phone" :
                                   firebaseClaim.Contains("facebook.com") ? "facebook" : "unknown";
                    }
                }

                Log.Information("Firebase login type: {LoginType}, Email: {Email}, Name: {Name}", loginType, email, name);

                // Check if user exists by email
                var existingUserResponse = await _userService.GetByEmail(email);
                Entities.EntityClass.User? existingUser = existingUserResponse?.Result;

                // Get doctor role - try multiple patterns similar to Mycompany
                var roleListResponse = await _roleService.GetAll();
                if (roleListResponse?.Result == null || !roleListResponse.Result.Any())
                {
                    Log.Error("No roles found in system");
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "No roles configured in system. Please contact administrator.");
                    return BadRequest(apiResponse);
                }

                // Try to find doctor role with multiple patterns
                var doctorRole = roleListResponse.Result.FirstOrDefault(x => 
                    x.Name != null && x.Name.ToLowerInvariant().Contains("doctor")) ??
                    roleListResponse.Result.FirstOrDefault(x => 
                        x.Name != null && (x.Name.ToLowerInvariant().Contains("user") || 
                                          x.Name.ToLowerInvariant().Contains("default"))) ??
                    roleListResponse.Result.FirstOrDefault(x => x.IsDefault && x.IsActive) ??
                    roleListResponse.Result.FirstOrDefault(x => x.IsActive);
                
                if (doctorRole == null)
                {
                    Log.Error("No suitable role found. Available roles: {Roles}", 
                        string.Join(", ", roleListResponse.Result.Select(r => r.Name ?? "Unknown")));
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "No suitable role found. Please contact administrator.");
                    return BadRequest(apiResponse);
                }

                Log.Information("Using role: {RoleName} (ID: {RoleId}) for Firebase login", doctorRole.Name, doctorRole.Id);

                LoginResponseDto loginResponse;
                int? doctorId = null; // Declare doctorId at method scope

                if (existingUser == null)
                {
                    // Create new user
                    Log.Information("Creating new user for email: {Email}", email);
                    
                    // Generate a default password hash (users will need to reset password)
                    var defaultPassword = "Prescripto@Zak.Com1431";
                    var passwordHash = HashPassword(defaultPassword);

                    // Split full name into first and last name
                    var fullName = name ?? email.Split('@')[0];
                    var nameParts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var firstName = nameParts.Length > 0 ? nameParts[0] : fullName;
                    var lastName = nameParts.Length > 1 ? string.Join(" ", nameParts.Skip(1)) : null;

                    // Create DTO that matches exactly the stored procedure parameters
                    var userInsertDto = new UserInsertStoredProcedureDto
                    {
                        TenantID = 1, // Default tenant - matches other controllers
                        FirstName = firstName,
                        LastName = lastName,
                        FullName = fullName,
                        UserName = email,
                        Email = email,
                        PasswordHash = passwordHash,
                        UserType = "Doctor", // Set user type
                        PhoneNumber = null,
                        ContactNo = null,
                        RoleId = doctorRole.Id,
                        IsActive = true,
                        ReferenceUserId = null
                    };

                    // Insert user using service layer
                    var insertResponse = await _userService.InsertWithStoredProcedureDto(userInsertDto);
                    if (!insertResponse.IsSuccess || insertResponse.Result <= 0)
                    {
                        Log.Error("Failed to create user: {Message}", insertResponse.Message);
                        ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Failed to create user: {insertResponse.Message}");
                        return BadRequest(apiResponse);
                    }

                    // Get the newly created user
                    var newUserResponse = await _userService.GetById(insertResponse.Result);
                    if (newUserResponse?.Result == null)
                    {
                        Log.Error("Failed to retrieve newly created user");
                        ApiResponseHelper.SetFailedResponse(apiResponse, null, "User created but failed to retrieve user details");
                        return BadRequest(apiResponse);
                    }

                    existingUser = newUserResponse.Result;
                    Log.Information("New user created successfully. UserID: {UserId}", existingUser.UserID);

                    // Create Doctor profile if user type is "Doctor" via HTTP API call
                    if (existingUser.UserType != null && existingUser.UserType.Equals("Doctor", StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            using var httpClient = new HttpClient();
                            
                            // Get the base URL from the current request
                            var baseUrl = $"{Request.Scheme}://{Request.Host}";
                            
                            // First, check if doctor profile already exists for this user
                            var checkEndpoint = $"/api/2025-02/get-doctor-by-user-id?doctorUserId={existingUser.UserID}";
                            var checkRequest = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}{checkEndpoint}");

                            // Forward authorization header from current request if available
                            if (Request.Headers.ContainsKey("Authorization"))
                            {
                                var authToken = Request.Headers["Authorization"].ToString();
                                if (!string.IsNullOrEmpty(authToken))
                                {
                                    checkRequest.Headers.Add("Authorization", authToken);
                                }
                            }

                            // Check if doctor exists
                            var checkResponse = await httpClient.SendAsync(checkRequest);
                            bool doctorExists = false;

                            if (checkResponse.IsSuccessStatusCode)
                            {
                                var checkResponseContent = await checkResponse.Content.ReadAsStringAsync();
                                
                                // Try to deserialize as DoctorApiResponseDto first
                                var checkApiResponse = JsonSerializer.Deserialize<ApiResponse<DoctorApiResponseDto>>(checkResponseContent, new JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive = true
                                });

                                // If doctor exists and response is successful, skip insertion
                                if (checkApiResponse?.IsSuccess == true && checkApiResponse.Results != null)
                                {
                                    doctorExists = true;
                                    doctorId = checkApiResponse.Results.DoctorID;
                                    Log.Information("Doctor profile already exists for UserID: {UserId}, DoctorID: {DoctorId}. Skipping insertion.", 
                                        existingUser.UserID, doctorId);
                                }
                            }

                            // Only create doctor profile if it doesn't exist
                            if (!doctorExists)
                            {
                                var createEndpoint = "/api/2025-02/create-doctor";
                                
                                // Create doctor insert DTO
                                var doctorInsertDto = new DoctorInsertRequestDto
                                {
                                    UserID = existingUser.UserID,
                                    Specialization = null, // Can be updated later
                                    LicenseNumber = null, // Can be updated later
                                    DoctorReferenceID = 0, // Default value
                                    HospitalAffiliation = null // Can be updated later
                                };

                                // Serialize the request body
                                var jsonContent = JsonSerializer.Serialize(doctorInsertDto);
                                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                // Create the HTTP request
                                var createHttpRequest = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}{createEndpoint}")
                                {
                                    Content = content
                                };

                                // Forward authorization header from current request if available
                                if (Request.Headers.ContainsKey("Authorization"))
                                {
                                    var authToken = Request.Headers["Authorization"].ToString();
                                    if (!string.IsNullOrEmpty(authToken))
                                    {
                                        createHttpRequest.Headers.Add("Authorization", authToken);
                                    }
                                }

                                // Make the API call to create doctor
                                var createResponse = await httpClient.SendAsync(createHttpRequest);
                                
                                if (createResponse.IsSuccessStatusCode)
                                {
                                    var createResponseContent = await createResponse.Content.ReadAsStringAsync();
                                    var doctorApiResponse = JsonSerializer.Deserialize<ApiResponse<int>>(createResponseContent, new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });

                                    if (doctorApiResponse?.IsSuccess == true && doctorApiResponse.Results > 0)
                                    {
                                        doctorId = doctorApiResponse.Results;
                                        Log.Information("Doctor profile created successfully for UserID: {UserId}, DoctorID: {DoctorId}", 
                                            existingUser.UserID, doctorId);
                                    }
                                    else
                                    {
                                        Log.Warning("Failed to create Doctor profile for UserID: {UserId}. Response: {Response}", 
                                            existingUser.UserID, createResponseContent);
                                    }
                                }
                                else
                                {
                                    var errorContent = await createResponse.Content.ReadAsStringAsync();
                                    Log.Warning("Failed to create Doctor profile for UserID: {UserId}. Status: {Status}, Error: {Error}", 
                                        existingUser.UserID, createResponse.StatusCode, errorContent);
                                    // Don't fail the login if doctor profile creation fails
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, "Exception occurred while checking/creating Doctor profile for UserID: {UserId}", existingUser.UserID);
                            // Don't fail the login if doctor profile creation fails
                        }
                    }
                }
                else
                {
                    Log.Information("Existing user found. UserID: {UserId}", existingUser.UserID);
                    
                    // For existing users, fetch doctor ID if user type is Doctor
                    if (existingUser.UserType != null && existingUser.UserType.Equals("Doctor", StringComparison.OrdinalIgnoreCase) && doctorId == null)
                    {
                        try
                        {
                            using var httpClient = new HttpClient();
                            var baseUrl = $"{Request.Scheme}://{Request.Host}";
                            var checkEndpoint = $"/api/2025-02/get-doctor-by-user-id?doctorUserId={existingUser.UserID}";
                            var checkRequest = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}{checkEndpoint}");

                            if (Request.Headers.ContainsKey("Authorization"))
                            {
                                var authToken = Request.Headers["Authorization"].ToString();
                                if (!string.IsNullOrEmpty(authToken))
                                {
                                    checkRequest.Headers.Add("Authorization", authToken);
                                }
                            }

                            var checkResponse = await httpClient.SendAsync(checkRequest);
                            if (checkResponse.IsSuccessStatusCode)
                            {
                                var checkResponseContent = await checkResponse.Content.ReadAsStringAsync();
                                var checkApiResponse = JsonSerializer.Deserialize<ApiResponse<DoctorApiResponseDto>>(checkResponseContent, new JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive = true
                                });

                                if (checkApiResponse?.IsSuccess == true && checkApiResponse.Results != null)
                                {
                                    doctorId = checkApiResponse.Results.DoctorID;
                                    Log.Information("Retrieved DoctorID: {DoctorId} for existing user UserID: {UserId}", doctorId, existingUser.UserID);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Warning(ex, "Failed to fetch doctor ID for existing user UserID: {UserId}", existingUser.UserID);
                        }
                    }
                }

                // Get user permissions/roles
                var permissions = await _authUserService.GetUserPermissions(existingUser.UserID);
                var roles = permissions ?? new List<string>();

                // Generate tokens
                var accessToken = GenerateJwtToken(existingUser, roles);
                var refreshToken = GenerateRefreshToken(existingUser);

                // Build login response
                loginResponse = new LoginResponseDto
                {
                    UserId = existingUser.UserID,
                    UserName = existingUser.UserName ?? email,
                    RoleName = roles,
                    Success = true,
                    Message = existingUserResponse?.Result == null ? "User created and login successful" : "Login successful",
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    LoginType = loginType,
                    UserEmail = existingUser.Email ?? email,
                    DoctorId = doctorId
                };

                ApiResponseHelper.SetSuccessResponse(apiResponse, loginResponse, loginResponse.Message, StatusResponseMessage.success, StatusCodes.Status200OK);
                Log.Information("Firebase login successful for user: {Email}", email);
            }
            catch (FirebaseAuthException ex)
            {
                Log.Error(ex, "Firebase authentication error");
                ApiResponseHelper.SetFailedResponse(apiResponse, null, "Invalid or expired Firebase token");
                return Unauthorized(apiResponse);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error verifying Firebase token");
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error verifying Firebase token: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        private string HashPassword(string password)
        {
            // Simple hash for default password - in production, use BCrypt or similar
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
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
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
            {
                return false;
            }

            // Trim the hashed password from database (may have whitespace)
            hashedPassword = hashedPassword.Trim();

            // Check if the hash is BCrypt format (starts with $2a$, $2b$, or $2y$)
            if (hashedPassword.StartsWith("$2a$") || hashedPassword.StartsWith("$2b$") || hashedPassword.StartsWith("$2y$"))
            {
                // BCrypt hash - would need BCrypt.Net package
                // For now, return false as BCrypt is not available
                // TODO: Add BCrypt.Net package and implement: return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
                return false;
            }

            // SHA256 hash (matches HashPassword method)
            try
            {
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    var computedHash = Convert.ToBase64String(hashedBytes);
                    
                    // Use constant-time comparison to prevent timing attacks
                    return ConstantTimeEquals(computedHash, hashedPassword);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error verifying password");
                return false;
            }
        }

        /// <summary>
        /// Constant-time string comparison to prevent timing attacks
        /// </summary>
        private bool ConstantTimeEquals(string a, string b)
        {
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            int result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }
            return result == 0;
        }
    }
}
