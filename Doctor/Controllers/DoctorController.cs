using Doctor.Application.Services;
using Doctor.Dtos.RequestDto.DoctorDto;
using Doctor.Dtos.ResponseDto.DoctorDto;
using Doctor.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Permission;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class DoctorController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly DoctorService _doctorService;
        public DoctorController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            DoctorService doctorService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _doctorService = doctorService;
        }

        [Authorize(Policy = PermissionConstants.DoctorGetAll)]
        [HttpGet("gets-all-doctors")]
        public async Task<ActionResult<ApiResponse<List<DoctorApiResponseDto>>>> GetAllDoctors()
        {
            var apiResponse = new ApiResponse<List<DoctorApiResponseDto>>();
            try
            {
                var doctors = await _doctorService.GetAll();
                var mappedDoctors = await _mapperService.MapList<Entities.EntityClass.Doctor, DoctorApiResponseDto>(doctors.Result);
                if (doctors.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorApiConstantsResponseMessage.doctor_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedDoctors;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorApiConstantsResponseMessage.doctor_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorApiConstantsResponseMessage.doctor_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DoctorGetId)]
        [HttpGet("get-doctor-by-id")]
        public async Task<ActionResult<ApiResponse<DoctorApiResponseDto>>> GetDoctorById(int doctorId)
        {
            var apiResponse = new ApiResponse<DoctorApiResponseDto>();
            try
            {
                var doctor = await _doctorService.GetById(doctorId);
                var mappedDoctor = await _mapperService.MapSingle<Entities.EntityClass.Doctor, DoctorApiResponseDto>(doctor.Result);
                if (doctor.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorApiConstantsResponseMessage.doctor_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedDoctor;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorApiConstantsResponseMessage.doctor_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorApiConstantsResponseMessage.doctor_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DoctorGetId)]
        [HttpGet("get-doctor-by-user-id")]
        public async Task<ActionResult<ApiResponse<DoctorApiResponseDto>>> GetByReferenceId(int doctorUserId)
        {
            var apiResponse = new ApiResponse<DoctorApiResponseDto>();
            try
            {
                var doctor = await _doctorService.GetByReferenceId(doctorUserId);
                var mappedDoctor = await _mapperService.MapSingle<Entities.EntityClass.Doctor, DoctorApiResponseDto>(doctor.Result);
                if (doctor.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorApiConstantsResponseMessage.doctor_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedDoctor;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorApiConstantsResponseMessage.doctor_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorApiConstantsResponseMessage.doctor_see_try_catch);
            }
            return Ok(apiResponse);
        }




        [Authorize(Policy = PermissionConstants.DoctorCreate)]
        [HttpPost("create-doctor")]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctor(DoctorInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _doctorService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorApiConstantsResponseMessage.doctor_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorApiConstantsResponseMessage.doctor_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorApiConstantsResponseMessage.doctor_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DoctorUpdate)]
        [HttpPut("update-doctor")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctor(DoctorUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _doctorService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorApiConstantsResponseMessage.doctor_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorApiConstantsResponseMessage.doctor_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorApiConstantsResponseMessage.doctor_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DoctorDelete)]
        [HttpDelete("delete-doctor")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctor(int doctorId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _doctorService.Delete(doctorId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorApiConstantsResponseMessage.doctor_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorApiConstantsResponseMessage.doctor_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorApiConstantsResponseMessage.doctor_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        // Additional endpoints from DoctorProfileController
        [HttpGet("active-doctor-list")]
        [Authorize(Policy = PermissionConstants.DoctorGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorApiResponseDto>>>> GetActiveDoctorList()
        {
            var apiResponse = new ApiResponse<List<DoctorApiResponseDto>>();
            try
            {
                var doctors = await _doctorService.GetByActiveStatus(true);
                if (doctors.Result == null || doctors.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorApiResponseDto>(), "No active doctors found");
                    return Ok(apiResponse);
                }

                var mappedDoctors = await _mapperService.MapList<Entities.EntityClass.Doctor, DoctorApiResponseDto>(doctors.Result);
                apiResponse.Results = mappedDoctors;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Active doctors retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("get-doctor-by-user-name")]
        [Authorize(Policy = PermissionConstants.DoctorGetId)]
        public async Task<ActionResult<ApiResponse<DoctorApiResponseDto>>> GetDoctorByUserName([FromQuery] string userName)
        {
            var apiResponse = new ApiResponse<DoctorApiResponseDto>();
            try
            {
                var doctor = await _doctorService.GetByUserName(userName);
                if (doctor.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Doctor not found");
                    return Ok(apiResponse);
                }

                var mappedDoctor = await _mapperService.MapSingle<Entities.EntityClass.Doctor, DoctorApiResponseDto>(doctor.Result);
                apiResponse.Results = mappedDoctor;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Doctor retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("get-doctor-by-user-email")]
        [Authorize(Policy = PermissionConstants.DoctorGetId)]
        public async Task<ActionResult<ApiResponse<DoctorApiResponseDto>>> GetDoctorByUserEmail([FromQuery] string emailAddress)
        {
            var apiResponse = new ApiResponse<DoctorApiResponseDto>();
            try
            {
                var doctor = await _doctorService.GetByEmail(emailAddress);
                if (doctor.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Doctor not found");
                    return Ok(apiResponse);
                }

                var mappedDoctor = await _mapperService.MapSingle<Entities.EntityClass.Doctor, DoctorApiResponseDto>(doctor.Result);
                apiResponse.Results = mappedDoctor;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Doctor retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("currently-online-doctor-list")]
        [Authorize(Policy = PermissionConstants.DoctorGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorApiResponseDto>>>> GetCurrentlyOnlineDoctorList()
        {
            var apiResponse = new ApiResponse<List<DoctorApiResponseDto>>();
            try
            {
                var doctors = await _doctorService.GetByOnlineStatus(true);
                if (doctors.Result == null || doctors.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorApiResponseDto>(), "No online doctors found");
                    return Ok(apiResponse);
                }

                var mappedDoctors = await _mapperService.MapList<Entities.EntityClass.Doctor, DoctorApiResponseDto>(doctors.Result);
                apiResponse.Results = mappedDoctors;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Online doctors retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("doctor-list-filter")]
        [Authorize(Policy = PermissionConstants.DoctorGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorApiResponseDto>>>> GetDoctorListFilter([FromQuery] string searchTerm = "")
        {
            var apiResponse = new ApiResponse<List<DoctorApiResponseDto>>();
            try
            {
                var doctors = await _doctorService.GetAll();
                var mappedDoctors = await _mapperService.MapList<Entities.EntityClass.Doctor, DoctorApiResponseDto>(doctors.Result);
                
                // Apply search filter if provided
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    // TODO: Implement proper filtering logic
                }
                
                apiResponse.Results = mappedDoctors;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Doctors retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("doctor-list-filter-by-admin")]
        [Authorize(Policy = PermissionConstants.DoctorGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorApiResponseDto>>>> GetDoctorListFilterByAdmin([FromQuery] string searchTerm = "")
        {
            // Same as GetDoctorListFilter but with admin permissions
            return await GetDoctorListFilter(searchTerm);
        }

        [HttpGet("doctor-list-filter-mobile-app")]
        [Authorize(Policy = PermissionConstants.DoctorGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorApiResponseDto>>>> GetDoctorListFilterMobileApp([FromQuery] string searchTerm = "")
        {
            // Same as GetDoctorListFilter but optimized for mobile
            return await GetDoctorListFilter(searchTerm);
        }

        [HttpGet("doctors-count-by-filters")]
        [Authorize(Policy = PermissionConstants.DoctorGetAll)]
        public async Task<ActionResult<ApiResponse<int>>> GetDoctorsCountByFilters([FromQuery] string searchTerm = "")
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var doctors = await _doctorService.GetAll();
                var count = doctors.Result?.Count ?? 0;
                ApiResponseHelper.SetSuccessResponse(apiResponse, count, "Doctors count retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("doctor-list-by-admin")]
        [Authorize(Policy = PermissionConstants.DoctorGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorApiResponseDto>>>> GetDoctorListByAdmin()
        {
            // Same as GetAllDoctors but with admin permissions
            return await GetAllDoctors();
        }

        [HttpGet("live-online-doctor-list")]
        [Authorize(Policy = PermissionConstants.DoctorGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorApiResponseDto>>>> GetLiveOnlineDoctorList()
        {
            var apiResponse = new ApiResponse<List<DoctorApiResponseDto>>();
            try
            {
                var doctors = await _doctorService.GetByOnlineStatus(true);
                if (doctors.Result == null || doctors.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorApiResponseDto>(), "No live online doctors found");
                    return Ok(apiResponse);
                }

                var mappedDoctors = await _mapperService.MapList<Entities.EntityClass.Doctor, DoctorApiResponseDto>(doctors.Result);
                apiResponse.Results = mappedDoctors;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Live online doctors retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("by-creator-id/{profileId}")]
        [Authorize(Policy = PermissionConstants.DoctorGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorApiResponseDto>>>> GetDoctorListByCreatorId(int profileId)
        {
            var apiResponse = new ApiResponse<List<DoctorApiResponseDto>>();
            try
            {
                var doctors = await _doctorService.GetByCreatorId(profileId);
                if (doctors.Result == null || doctors.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorApiResponseDto>(), "No doctors found");
                    return Ok(apiResponse);
                }

                var mappedDoctors = await _mapperService.MapList<Entities.EntityClass.Doctor, DoctorApiResponseDto>(doctors.Result);
                apiResponse.Results = mappedDoctors;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Doctors retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPut("active-status-by-admin/{id}")]
        [Authorize(Policy = PermissionConstants.DoctorUpdate)]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateActiveStatusByAdmin(int id, [FromQuery] bool activeStatus)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _doctorService.UpdateActiveStatus(id, activeStatus);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Active status updated successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPut("doctors-online-status/{id}")]
        [Authorize(Policy = PermissionConstants.DoctorUpdate)]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateDoctorsOnlineStatus(int id, [FromQuery] bool onlineStatus)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _doctorService.UpdateOnlineStatus(id, onlineStatus);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Online status updated successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPut("expertise/{id}")]
        [Authorize(Policy = PermissionConstants.DoctorUpdate)]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateExpertise(int id, [FromQuery] string expertise)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _doctorService.UpdateExpertise(id, expertise);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Expertise updated successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPut("profile-step/{profileId}")]
        [Authorize(Policy = PermissionConstants.DoctorUpdate)]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateProfileStep(int profileId, [FromQuery] int step)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _doctorService.UpdateProfileStep(profileId, step);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Profile step updated successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        // Alternative routes for backward compatibility with doctor-profile endpoints
        [HttpPost("doctor-profile")]
        [Authorize(Policy = PermissionConstants.DoctorCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorProfile([FromBody] DoctorInsertRequestDto request)
        {
            // Redirect to CreateDoctor
            return await CreateDoctor(request);
        }

        [HttpGet("doctor-profile/{id}")]
        [Authorize(Policy = PermissionConstants.DoctorGetId)]
        public async Task<ActionResult<ApiResponse<DoctorApiResponseDto>>> GetDoctorProfileById(int id)
        {
            // Redirect to GetDoctorById
            return await GetDoctorById(id);
        }

        [HttpGet("doctor-profile/by-user-id/{userId}")]
        [Authorize(Policy = PermissionConstants.DoctorGetId)]
        public async Task<ActionResult<ApiResponse<DoctorApiResponseDto>>> GetDoctorByUserIdRoute(int userId)
        {
            // Redirect to GetByReferenceId
            return await GetByReferenceId(userId);
        }

        [HttpPut("doctor-profile")]
        [Authorize(Policy = PermissionConstants.DoctorUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorProfile([FromBody] DoctorUpdateRequestDto request)
        {
            // Redirect to UpdateDoctor
            return await UpdateDoctor(request);
        }

        [HttpGet("doctor-profile/{id}/doctor-details-by-admin")]
        [Authorize(Policy = PermissionConstants.DoctorGetId)]
        public async Task<ActionResult<ApiResponse<DoctorApiResponseDto>>> GetDoctorDetailsByAdmin(int id)
        {
            var apiResponse = new ApiResponse<DoctorApiResponseDto>();
            try
            {
                // Call stored procedure to get comprehensive doctor details
                var doctorDetails = await _doctorService.GetDetailsByAdmin(id);
                if (doctorDetails.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Doctor not found");
                    return Ok(apiResponse);
                }

                var mappedDoctor = await _mapperService.MapSingle<Entities.EntityClass.Doctor, DoctorApiResponseDto>(doctorDetails.Result);
                apiResponse.Results = mappedDoctor;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Doctor details retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

    }
}
