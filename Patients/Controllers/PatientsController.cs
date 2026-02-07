using Entities.EntityClass.PatientEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatienFolowUp.Application.Services;
using PatienFolowUp.Dtos.RequestDto.Patients;
using PatienFolowUp.Dtos.RequestDto.PatientsDto;
using PatienFolowUp.Dtos.ResponseDto.Patients;
using PatienFolowUp.Utility;
using Patients.Dtos.ResponseDto.DoctorDto;
using Patients.Dtos.ResponseDto.PatientsDto;
using SharedService.CommonService;
using SharedService.MapService;
using System.Net.Http;
using System.Net.Http.Json;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace PatienFolowUp.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PatientsController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly PatientsService _patientService;
        public PatientsController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            PatientsService patientsService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _patientService = patientsService;
        }
        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        [HttpGet("gets-all-patients")]

        public async Task<ActionResult<PagedWithResponse<List<PatientDataDto>>>> GetAllPatients(
    int pageNumber = 1,
    int pageSize = 10,
    int? doctorId = null,
    string searchTerm = "",string? followupdate="")
        {
            var apiResponse = new PagedWithResponse<List<PatientDataDto>>();

            try
            {
                // Call service to get paged patients
                var patients = await _patientService.GetAllPatients(pageNumber, pageSize, searchTerm, doctorId, followupdate);

                if (patients.Result == null && patients.TotalCount == 0)
                {
                    // Failed response with empty list and total count 0
                    ApiResponseHelper.SetFailedResponse(
                        apiResponse,
                        result: new List<PatientDataDto>(),
                        totalCount: patients.TotalCount,
                        message: PatientsApiConstantsResponseMessage.patients_null_of_get_list
                    );
                    return Ok(apiResponse);
                }

                // Success response
                ApiResponseHelper.SetSuccessResponse(
                    apiResponse,
                    result: patients.Result,
                    totalCount: patients.TotalCount,
                    message: PatientsApiConstantsResponseMessage.patients_get_all_success,
                    status: StatusResponseMessage.success,
                    statusCode: StatusCodes.Status200OK
                );
            }
            catch (Exception ex)
            {
                // Failed response in case of exception
                ApiResponseHelper.SetFailedResponse(
                    apiResponse,
                    result: new List<PatientDataDto>(),
                    totalCount: 0,
                    message: PatientsApiConstantsResponseMessage.patients_see_try_catch
                );
            }

            return Ok(apiResponse);
        }





        [Authorize(Policy = PermissionConstants.PatientsGetId)]
        [HttpGet("get-patients-by-id")]
        public async Task<ActionResult<ApiResponse<PatientsApiResponseDto>>> GetPatientsById(int PatientId)
        {
            var apiResponse = new ApiResponse<PatientsApiResponseDto>();
            try
            {
                var patients = await _patientService.GetById(PatientId);
                var mappedPatients =patients.Result;
                if (patients.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PatientsApiConstantsResponseMessage.patients_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPatients;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PatientsApiConstantsResponseMessage.patients_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PatientsApiConstantsResponseMessage.patients_see_try_catch);
            }
            return Ok(apiResponse);
        }


        [Authorize(Policy = PermissionConstants.PatientsGetId)]
        [HttpGet("get-patients-by-phone_no")]
        public async Task<ActionResult<ApiResponse<PatientsApiResponseDto>>> GetByPhoneNo(string phoneNo)
        {
            var apiResponse = new ApiResponse<PatientsApiResponseDto>();
            try
            {
                var patients = await _patientService.GetByPhoneNo(phoneNo);
                var mappedPatients = patients.Result;
                if (patients.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PatientsApiConstantsResponseMessage.patients_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPatients;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PatientsApiConstantsResponseMessage.patients_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PatientsApiConstantsResponseMessage.patients_see_try_catch);
            }
            return Ok(apiResponse);
        }



        [HttpGet("gets-all-followup-patients")]
        public async Task<ActionResult<ApiResponse<FollowUpResponseDto>>> GetFollowUpPatients(
    [FromQuery] int? doctorId = null,
    [FromQuery] string startDate = "",
    [FromQuery] string endDate = "")
        {
            var apiResponse = new ApiResponse<FollowUpResponseDto>();
            try
            {
                var patients = await _patientService.GetFollowUpPatients(doctorId, startDate, endDate);

                if (patients.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Failed to load patient follow up.");
                    return Ok(apiResponse);
                }
                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(
                    apiResponse,
                    apiResponse.Results,
                    "Patient FollowUp retrieved successfully.",
                    StatusResponseMessage.success,
                    StatusCodes.Status200OK
                );
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, "Failed to load patient follow up.");
            }

            return Ok(apiResponse);
        }









        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        [HttpGet("get-age-distribution")]
        public async Task<ActionResult<ApiResponse<List<PatientAgeDistributionResponseDto>>>> GetAgeDistribution()
        {
            var apiResponse = new ApiResponse<List<PatientAgeDistributionResponseDto>>();

            try
            {
                var result = await _patientService.GetAgeDistribution();

                if (result == null || !result.IsSuccess)
                {
                    // Setting a failed response with a descriptive message
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Failed to load patient age distribution.");
                    return Ok(apiResponse);  // Return the API response with the failure message
                }

                // On success, populate the response with the results and return a success status
                apiResponse.Results = result.Result.ToList();
                ApiResponseHelper.SetSuccessResponse(
                    apiResponse,
                    apiResponse.Results,
                    "Patient age distribution retrieved successfully.",
                    StatusResponseMessage.success,
                    StatusCodes.Status200OK
                );
            }
            catch (Exception ex)
            {
                // Log the exception if needed for debugging purposes (e.g., _logger.LogError(ex, "Error retrieving age distribution"))

                // Setting a failed response in case of an error during the process
                ApiResponseHelper.SetFailedResponse(apiResponse, null, "Error retrieving patient age distribution.");
            }

            return Ok(apiResponse);
        }



        [Authorize(Policy = PermissionConstants.PatientsGetId)]
        [HttpGet("get-patient-by-user-id")]
        public async Task<ActionResult<ApiResponse<PatientsApiResponseDto>>> GetPatientsByReferenceId(int patientUserId)
        {
            var apiResponse = new ApiResponse<PatientsApiResponseDto>();
            try
            {
                var patients = await _patientService.GetByRoleAndReferenceId(patientUserId);
                var mappedPatients = await _mapperService.MapSingle<Patient, PatientsApiResponseDto>(patients.Result);
                if (patients.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PatientsApiConstantsResponseMessage.patients_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPatients;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PatientsApiConstantsResponseMessage.patients_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PatientsApiConstantsResponseMessage.patients_see_try_catch);
            }
            return Ok(apiResponse);
        }




        [Authorize(Policy = PermissionConstants.PatientsCreate)]
        [HttpPost("create-patients")]
        public async Task<ActionResult<ApiResponse<int>>> CreatePatients(PatientsInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {

                if (ModelState.IsValid)
                {
                    var response = await _patientService.Insert(request);
                   
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PatientsApiConstantsResponseMessage.patients_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PatientsApiConstantsResponseMessage.patients_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PatientsApiConstantsResponseMessage.patients_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.PatientsUpdate)]
        [HttpPut("update-patients")]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePatients(PatientsUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _patientService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PatientsApiConstantsResponseMessage.patients_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PatientsApiConstantsResponseMessage.patients_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PatientsApiConstantsResponseMessage.patients_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.PatientsDelete)]
        [HttpDelete("delete-patients")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePatients(int PatientId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _patientService.Delete(PatientId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PatientsApiConstantsResponseMessage.patients_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, PatientsApiConstantsResponseMessage.patients_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, PatientsApiConstantsResponseMessage.patients_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PatientsGetId)]
        [HttpGet("get-patient-by-phone-and-code")]
        public async Task<ActionResult<ApiResponse<PatientsApiResponseDto>>> GetPatientByPhoneAndCode([FromQuery] string pCode, [FromQuery] string pPhone)
        {
            var apiResponse = new ApiResponse<PatientsApiResponseDto>();
            try
            {
                var patients = await _patientService.GetByPhoneAndCode(pCode, pPhone);
                if (patients.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PatientsApiConstantsResponseMessage.patients_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PatientsApiConstantsResponseMessage.patients_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PatientsApiConstantsResponseMessage.patients_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PatientsGetId)]
        [HttpGet("get-patient-by-user-name")]
        public async Task<ActionResult<ApiResponse<PatientsApiResponseDto>>> GetPatientByUserName([FromQuery] string userName)
        {
            var apiResponse = new ApiResponse<PatientsApiResponseDto>();
            try
            {
                var patients = await _patientService.GetByUserName(userName);
                if (patients.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PatientsApiConstantsResponseMessage.patients_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PatientsApiConstantsResponseMessage.patients_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PatientsApiConstantsResponseMessage.patients_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PatientsGetId)]
        [HttpGet("get-patient-by-user-id-direct")]
        public async Task<ActionResult<ApiResponse<PatientsApiResponseDto>>> GetPatientByUserIdDirect([FromQuery] int userId)
        {
            var apiResponse = new ApiResponse<PatientsApiResponseDto>();
            try
            {
                var patients = await _patientService.GetByUserId(userId);
                if (patients.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PatientsApiConstantsResponseMessage.patients_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PatientsApiConstantsResponseMessage.patients_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PatientsApiConstantsResponseMessage.patients_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        [HttpGet("get-all-patients-list")]
        public async Task<ActionResult<ApiResponse<List<PatientsApiResponseDto>>>> GetAllPatientsList()
        {
            var apiResponse = new ApiResponse<List<PatientsApiResponseDto>>();
            try
            {
                var patients = await _patientService.GetAllPatients();
                if (patients.Result == null || !patients.Result.Any())
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), PatientsApiConstantsResponseMessage.patients_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PatientsApiConstantsResponseMessage.patients_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), PatientsApiConstantsResponseMessage.patients_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        [HttpGet("get-patient-list-by-user-profile-id")]
        public async Task<ActionResult<ApiResponse<List<PatientsApiResponseDto>>>> GetPatientListByUserProfileId([FromQuery] int profileId, [FromQuery] string role)
        {
            var apiResponse = new ApiResponse<List<PatientsApiResponseDto>>();
            try
            {
                var patients = await _patientService.GetPatientListByUserProfileId(profileId, role);
                if (patients.Result == null || !patients.Result.Any())
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), PatientsApiConstantsResponseMessage.patients_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PatientsApiConstantsResponseMessage.patients_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), PatientsApiConstantsResponseMessage.patients_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        [HttpGet("get-patient-list-by-search-user-profile-id")]
        public async Task<ActionResult<ApiResponse<List<PatientsApiResponseDto>>>> GetPatientListBySearchUserProfileId([FromQuery] int profileId, [FromQuery] string role, [FromQuery] string name = "")
        {
            var apiResponse = new ApiResponse<List<PatientsApiResponseDto>>();
            try
            {
                var patients = await _patientService.GetPatientListBySearchUserProfileId(profileId, role, name);
                if (patients.Result == null || !patients.Result.Any())
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), PatientsApiConstantsResponseMessage.patients_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PatientsApiConstantsResponseMessage.patients_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), PatientsApiConstantsResponseMessage.patients_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PatientsGetAll)]
        [HttpGet("get-patient-list-filter")]
        public async Task<ActionResult<ApiResponse<List<PatientsApiResponseDto>>>> GetPatientListFilter([FromQuery] string searchTerm = "")
        {
            var apiResponse = new ApiResponse<List<PatientsApiResponseDto>>();
            try
            {
                var patients = await _patientService.GetPatientListFilter(searchTerm);
                if (patients.Result == null || !patients.Result.Any())
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), PatientsApiConstantsResponseMessage.patients_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = patients.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PatientsApiConstantsResponseMessage.patients_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientsApiResponseDto>(), PatientsApiConstantsResponseMessage.patients_see_try_catch);
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

        // Alternative routes for backward compatibility with patient-profile endpoints
        [HttpPost("patient-profile")]
        [Authorize(Policy = PermissionConstants.PatientsCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreatePatientProfile([FromBody] PatientsInsertRequestDto request)
        {
            // Redirect to CreatePatients
            return await CreatePatients(request);
        }

        [HttpGet("patient-profile/{id}")]
        [Authorize(Policy = PermissionConstants.PatientsGetId)]
        public async Task<ActionResult<ApiResponse<PatientsApiResponseDto>>> GetPatientProfileById(int id)
        {
            // Redirect to GetPatientsById
            return await GetPatientsById(id);
        }

        [HttpGet("patient-profile/by-user-id/{userId}")]
        [Authorize(Policy = PermissionConstants.PatientsGetId)]
        public async Task<ActionResult<ApiResponse<PatientsApiResponseDto>>> GetPatientByUserIdRoute(int userId)
        {
            // Redirect to GetPatientByUserIdDirect
            return await GetPatientByUserIdDirect(userId);
        }

        [HttpPut("patient-profile")]
        [Authorize(Policy = PermissionConstants.PatientsUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePatientProfile([FromBody] PatientsUpdateRequestDto request)
        {
            // Redirect to UpdatePatients
            return await UpdatePatients(request);
        }

    }
}
