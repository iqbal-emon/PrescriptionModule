using Entities.EntityClass.PatientEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatienFolowUp.Application.Services;
using PatienFolowUp.Dtos.RequestDto.Patients;
using PatienFolowUp.Dtos.RequestDto.PatientsDto;
using PatienFolowUp.Dtos.ResponseDto.Patients;
using Patients.Dtos.ResponseDto.DoctorDto;
using Patients.Dtos.ResponseDto.PatientsDto;
using SharedService.MapService;
using System.Net.Http;
using System.Net.Http.Json;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace PatienFolowUp.Controllers
{
    [ApiController]
    [Route("api/2025-02/patient-profile")]
    public class PatientProfileController : ControllerBase
    {
        private readonly PatientsService _patientService;
        private readonly MapperService _mapperService;

        public PatientProfileController(PatientsService patientService, MapperService mapperService)
        {
            _patientService = patientService;
            _mapperService = mapperService;
        }

        [HttpPost]
        [Authorize(Policy = PermissionConstants.PatientsCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreatePatientProfile([FromBody] PatientsInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _patientService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Patient profile created successfully");
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, "Invalid model state");
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error creating patient profile: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionConstants.PatientsGetId)]
        public async Task<ActionResult<ApiResponse<PatientsApiResponseDto>>> GetPatientProfileById(int id)
        {
            var apiResponse = new ApiResponse<PatientsApiResponseDto>();
            try
            {
                var patients = await _patientService.GetById(id);
                if (patients.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Patient not found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Patient retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error retrieving patient: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("by-phone-and-code")]
        [Authorize(Policy = PermissionConstants.PatientsGetId)]
        public async Task<ActionResult<ApiResponse<PatientsApiResponseDto>>> GetPatientByPhoneAndCode([FromQuery] string pCode, [FromQuery] string pPhone)
        {
            var apiResponse = new ApiResponse<PatientsApiResponseDto>();
            try
            {
                var patients = await _patientService.GetByPhoneAndCode(pCode, pPhone);
                if (patients.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Patient not found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Patient retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error retrieving patient: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("by-user-id/{userId}")]
        [Authorize(Policy = PermissionConstants.PatientsGetId)]
        public async Task<ActionResult<ApiResponse<PatientsApiResponseDto>>> GetPatientByUserId(int userId)
        {
            var apiResponse = new ApiResponse<PatientsApiResponseDto>();
            try
            {
                var patients = await _patientService.GetByUserId(userId);
                if (patients.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Patient not found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Patient retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error retrieving patient: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("by-user-name")]
        [Authorize(Policy = PermissionConstants.PatientsGetId)]
        public async Task<ActionResult<ApiResponse<PatientsApiResponseDto>>> GetPatientByUserName([FromQuery] string userName)
        {
            var apiResponse = new ApiResponse<PatientsApiResponseDto>();
            try
            {
                var patients = await _patientService.GetByUserName(userName);
                if (patients.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Patient not found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Patient retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error retrieving patient: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("doctor-list-by-creator-id-filter/{profileId}")]
        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorApiResponseDto>>>> GetDoctorListByCreatorIdFilter(int profileId)
        {
            var apiResponse = new ApiResponse<List<DoctorApiResponseDto>>();
            try
            {
                // Call the API endpoint instead of direct service
                using var httpClient = new HttpClient();
                
                // Get the base URL from the current request
                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var apiEndpoint = $"/api/2025-02/doctor-profile/by-creator-id/{profileId}";
                
                // Get the authorization token from the current request and add to request message
                var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}{apiEndpoint}");
                
                // Forward authorization header from current request
                if (Request.Headers.ContainsKey("Authorization"))
                {
                    var authToken = Request.Headers["Authorization"].ToString();
                    if (!string.IsNullOrEmpty(authToken))
                    {
                        request.Headers.Add("Authorization", authToken);
                    }
                }

                // Make the API call
                var response = await httpClient.SendAsync(request);
                
                if (!response.IsSuccessStatusCode)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorApiResponseDto>(), $"API call failed with status: {response.StatusCode}");
                    return Ok(apiResponse);
                }

                // Parse the response
                var apiResult = await response.Content.ReadFromJsonAsync<ApiResponse<List<DoctorApiResponseDto>>>();
                
                if (apiResult?.Results == null || apiResult.Results.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorApiResponseDto>(), "No doctors found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = apiResult.Results;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Doctors retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("doctor-list-filter")]
        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        public async Task<ActionResult<ApiResponse<List<PatientsApiResponseDto>>>> GetPatientListFilter([FromQuery] string searchTerm = "")
        {
            var apiResponse = new ApiResponse<List<PatientsApiResponseDto>>();
            try
            {
                var patients = await _patientService.GetPatientListFilter(searchTerm);
                if (patients.Result == null || !patients.Result.Any())
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), "No patients found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Patients retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet]
        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        public async Task<ActionResult<ApiResponse<List<PatientsApiResponseDto>>>> GetAllPatients()
        {
            var apiResponse = new ApiResponse<List<PatientsApiResponseDto>>();
            try
            {
                var patients = await _patientService.GetAllPatients();
                if (patients.Result == null || !patients.Result.Any())
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), "No patients found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Patients retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("patient-list-by-admin")]
        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        public async Task<ActionResult<ApiResponse<List<PatientsApiResponseDto>>>> GetPatientListByAdmin()
        {
            var apiResponse = new ApiResponse<List<PatientsApiResponseDto>>();
            try
            {
                // TODO: Implement admin-specific filtering
                // For now, return all patients
                var patients = await _patientService.GetAllPatients();
                if (patients.Result == null || !patients.Result.Any())
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), "No patients found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Patients retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("patient-list-by-agent-master/{masterId}")]
        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        public async Task<ActionResult<ApiResponse<List<PatientsApiResponseDto>>>> GetPatientListByAgentMaster(int masterId)
        {
            var apiResponse = new ApiResponse<List<PatientsApiResponseDto>>();
            try
            {
                var patients = await _patientService.GetPatientListByAgentMaster(masterId);
                if (patients.Result == null || !patients.Result.Any())
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), "No patients found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Patients retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("patient-list-by-agent-super-visor/{supervisorId}")]
        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        public async Task<ActionResult<ApiResponse<List<PatientsApiResponseDto>>>> GetPatientListByAgentSupervisor(int supervisorId)
        {
            var apiResponse = new ApiResponse<List<PatientsApiResponseDto>>();
            try
            {
                var patients = await _patientService.GetPatientListByAgentSupervisor(supervisorId);
                if (patients.Result == null || !patients.Result.Any())
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), "No patients found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Patients retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("patient-list-by-search-user-profile-id/{profileId}")]
        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        public async Task<ActionResult<ApiResponse<List<PatientsApiResponseDto>>>> GetPatientListBySearchUserProfileId(int profileId, [FromQuery] string role, [FromQuery] string name = "")
        {
            var apiResponse = new ApiResponse<List<PatientsApiResponseDto>>();
            try
            {
                var patients = await _patientService.GetPatientListBySearchUserProfileId(profileId, role, name);
                if (patients.Result == null || !patients.Result.Any())
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), "No patients found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Patients retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("patient-list-by-user-profile-id/{profileId}")]
        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        public async Task<ActionResult<ApiResponse<List<PatientsApiResponseDto>>>> GetPatientListByUserProfileId(int profileId, [FromQuery] string role)
        {
            var apiResponse = new ApiResponse<List<PatientsApiResponseDto>>();
            try
            {
                var patients = await _patientService.GetPatientListByUserProfileId(profileId, role);
                if (patients.Result == null || !patients.Result.Any())
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), "No patients found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Patients retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("patient-list-filter-by-admin/{userId}")]
        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        public async Task<ActionResult<ApiResponse<List<PatientsApiResponseDto>>>> GetPatientListFilterByAdmin(int userId, [FromQuery] string role)
        {
            var apiResponse = new ApiResponse<List<PatientsApiResponseDto>>();
            try
            {
                // TODO: Implement admin-specific filtering with role
                // For now, return filtered list
                var patients = await _patientService.GetPatientListFilter("");
                if (patients.Result == null || !patients.Result.Any())
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), "No patients found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Patients retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPut]
        [Authorize(Policy = PermissionConstants.PatientsUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePatientProfile([FromBody] PatientsUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _patientService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Patient profile updated successfully");
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, "Invalid model state");
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error updating patient profile: {ex.Message}");
            }
            return Ok(apiResponse);
        }
    }
}

