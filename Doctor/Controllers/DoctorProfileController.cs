using Doctor.Application.Services;
using Doctor.Dtos.RequestDto.DoctorDto;
using Doctor.Dtos.ResponseDto.DoctorDto;
using Doctor.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/doctor-profile")]
    public class DoctorProfileController : ControllerBase
    {
        private readonly DoctorService _doctorService;
        private readonly MapperService _mapperService;

        public DoctorProfileController(DoctorService doctorService, MapperService mapperService)
        {
            _doctorService = doctorService;
            _mapperService = mapperService;
        }

        [HttpPost]
        [Authorize(Policy = PermissionConstants.DoctorCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorProfile([FromBody] DoctorInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _doctorService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Doctor profile created successfully");
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
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error creating doctor profile: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionConstants.DoctorGetId)]
        public async Task<ActionResult<ApiResponse<DoctorApiResponseDto>>> GetDoctorProfileById(int id)
        {
            var apiResponse = new ApiResponse<DoctorApiResponseDto>();
            try
            {
                var doctor = await _doctorService.GetById(id);
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
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error retrieving doctor: {ex.Message}");
            }
            return Ok(apiResponse);
        }

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

        [HttpGet("by-user-id/{userId}")]
        [Authorize(Policy = PermissionConstants.DoctorGetId)]
        public async Task<ActionResult<ApiResponse<DoctorApiResponseDto>>> GetDoctorByUserId(int userId)
        {
            var apiResponse = new ApiResponse<DoctorApiResponseDto>();
            try
            {
                var doctor = await _doctorService.GetByReferenceId(userId);
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

        [HttpGet("by-user-name")]
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

        [HttpGet("by-user-email")]
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

        [HttpGet("{id}/doctor-by-profile-id")]
        [Authorize(Policy = PermissionConstants.DoctorGetId)]
        public async Task<ActionResult<ApiResponse<DoctorApiResponseDto>>> GetDoctorByProfileId(int id)
        {
            // Same as GetById
            return await GetDoctorProfileById(id);
        }

        [HttpGet("{id}/doctor-details-by-admin")]
        [Authorize(Policy = PermissionConstants.DoctorGetId)]
        public async Task<ActionResult<ApiResponse<DoctorApiResponseDto>>> GetDoctorDetailsByAdmin(int id)
        {
            // Same as GetById but with admin permissions
            return await GetDoctorProfileById(id);
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

        [HttpGet]
        [Authorize(Policy = PermissionConstants.DoctorGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorApiResponseDto>>>> GetAllDoctors()
        {
            var apiResponse = new ApiResponse<List<DoctorApiResponseDto>>();
            try
            {
                var doctors = await _doctorService.GetAll();
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

        [HttpPut]
        [Authorize(Policy = PermissionConstants.DoctorUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorProfile([FromBody] DoctorUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _doctorService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Doctor profile updated successfully");
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
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error updating doctor profile: {ex.Message}");
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

        [HttpPut("doctor-profile")]
        [Authorize(Policy = PermissionConstants.DoctorUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorProfileAlt([FromBody] DoctorUpdateRequestDto request)
        {
            // Same as UpdateDoctorProfile
            return await UpdateDoctorProfile(request);
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
    }
}

