using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doctor.Application.Services;
using Doctor.Utility;
using Doctor.Dtos.ResponseDto.DoctorChamberDto;
using Doctor.Dtos.RequestDto.DoctorChamberDto;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class DoctorChamberController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly DoctorChamberService _chamberService;

        public DoctorChamberController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            DoctorChamberService chamberService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _chamberService = chamberService;
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("gets-all-doctor-chambers")]
        public async Task<ActionResult<ApiResponse<List<DoctorChamberApiResponseDto>>>> GetAllDoctorChambers()
        {
            var apiResponse = new ApiResponse<List<DoctorChamberApiResponseDto>>();
            try
            {
                var chamberList = await _chamberService.GetAll();
                var mappedChambers = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorChamber, DoctorChamberApiResponseDto>(chamberList.Result);

                if (chamberList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorChamberApiConstantsResponseMessage.chamber_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedChambers;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorChamberApiConstantsResponseMessage.chamber_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorChamberApiConstantsResponseMessage.chamber_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        [HttpGet("get-doctor-chamber-by-id")]
        public async Task<ActionResult<ApiResponse<DoctorChamberApiResponseDto>>> GetDoctorChamberById(int chamberId)
        {
            var apiResponse = new ApiResponse<DoctorChamberApiResponseDto>();
            try
            {
                var chamber = await _chamberService.GetById(chamberId);
                var mappedChamber = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorChamber, DoctorChamberApiResponseDto>(chamber.Result);

                if (chamber.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorChamberApiConstantsResponseMessage.chamber_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedChamber;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorChamberApiConstantsResponseMessage.chamber_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorChamberApiConstantsResponseMessage.chamber_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        [HttpPost("create-doctor-chamber")]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorChamber(DoctorChamberInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _chamberService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorChamberApiConstantsResponseMessage.chamber_insert_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorChamberApiConstantsResponseMessage.chamber_inserted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorChamberApiConstantsResponseMessage.chamber_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        [HttpPut("update-doctor-chamber")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorChamber(DoctorChamberUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _chamberService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorChamberApiConstantsResponseMessage.chamber_update_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorChamberApiConstantsResponseMessage.chamber_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorChamberApiConstantsResponseMessage.chamber_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        [HttpDelete("delete-doctor-chamber-by-id")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorChamber(int chamberId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _chamberService.Delete(chamberId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorChamberApiConstantsResponseMessage.chamber_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorChamberApiConstantsResponseMessage.chamber_deleted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorChamberApiConstantsResponseMessage.chamber_see_try_catch);
            }
            return Ok(apiResponse);
        }


        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("gets_district_by_division_id")]
        public async Task<ActionResult<ApiResponse<List<DistrictApiResponseDto>>>> GetAllDistrictList(int divisonId)
        {
            var apiResponse = new ApiResponse<List<DistrictApiResponseDto>>();

            try
            {
                var districtList = await _chamberService.GetAllDistrict(divisonId);
                var mappedDistricts = await _mapperService.MapList<
                    Entities.CountryEntity.District,
                    DistrictApiResponseDto
                >(districtList.Result);

                if (districtList.Result == null || districtList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(
                        apiResponse,
                        null,
                        "No district found"
                    );
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDistricts;

                ApiResponseHelper.SetSuccessResponse(
                    apiResponse,
                    apiResponse.Results,
                    "District list loaded successfully",
                    StatusResponseMessage.success,
                    StatusCodes.Status200OK
                );
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(
                    apiResponse,
                    null,
                    "An unexpected error occurred while getting district list"
                );
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("gets-all-division_list")]
        public async Task<ActionResult<ApiResponse<List<DivisionApiResponseDto>>>> GetAllDivisionList()
        {
            var apiResponse = new ApiResponse<List<DivisionApiResponseDto>>();

            try
            {
                // Call the service to get all divisions
                var divisionList = await _chamberService.GetAllDivision();

                // Map entities to DTOs
                var mappedDivisions = await _mapperService.MapList<
                    Entities.CountryEntity.Division,
                    DivisionApiResponseDto
                >(divisionList.Result);

                // Handle empty results
                if (divisionList.Result == null || divisionList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(
                        apiResponse,
                        null,
                        "No division found"
                    );
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDivisions;

                // Set success response
                ApiResponseHelper.SetSuccessResponse(
                    apiResponse,
                    apiResponse.Results,
                    "Division list loaded successfully",
                    StatusResponseMessage.success,
                    StatusCodes.Status200OK
                );
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(
                    apiResponse,
                    null,
                    "An unexpected error occurred while getting division list"
                );
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("get-doctor-chamber-list-by-doctor-id")]
        public async Task<ActionResult<ApiResponse<List<DoctorChamberApiResponseDto>>>> GetDoctorChamberListByDoctorId(int doctorId)
        {
            var apiResponse = new ApiResponse<List<DoctorChamberApiResponseDto>>();
            try
            {
                var chamberList = await _chamberService.GetByDoctorId(doctorId);
                var mappedChambers = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorChamber, DoctorChamberApiResponseDto>(chamberList.Result);

                if (chamberList.Result == null || chamberList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorChamberApiConstantsResponseMessage.chamber_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedChambers;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorChamberApiConstantsResponseMessage.chamber_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorChamberApiConstantsResponseMessage.chamber_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}

