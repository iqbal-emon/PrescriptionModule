using Entities.EntityClass.PatientEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatienFolowUp.Application.Services;
using PatienFolowUp.Dtos.RequestDto.Patients;
using PatienFolowUp.Dtos.RequestDto.PatientsDto;
using PatienFolowUp.Dtos.ResponseDto.Patients;
using PatienFolowUp.Utility;
using Patients.Dtos.ResponseDto.PatientsDto;
using SharedService.CommonService;
using SharedService.MapService;
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
    string searchTerm = "")
        {
            var apiResponse = new PagedWithResponse<List<PatientDataDto>>();

            try
            {
                // Call service to get paged patients
                var patients = await _patientService.GetAllPatients(pageNumber, pageSize, searchTerm, doctorId);

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

    }
}
