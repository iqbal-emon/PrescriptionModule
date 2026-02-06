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
using Doctor.Dtos.ResponseDto.MasterDoctorDto;
using Doctor.Dtos.RequestDto.MasterDoctorDto;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class MasterDoctorController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly MasterDoctorService _masterDoctorService;

        public MasterDoctorController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            MasterDoctorService masterDoctorService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _masterDoctorService = masterDoctorService;
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("gets-all-master-doctors")]
        public async Task<ActionResult<ApiResponse<List<MasterDoctorApiResponseDto>>>> GetAllMasterDoctors()
        {
            var apiResponse = new ApiResponse<List<MasterDoctorApiResponseDto>>();
            try
            {
                var masterDoctorList = await _masterDoctorService.GetAll();
                var mappedMasterDoctors = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.MasterDoctor, MasterDoctorApiResponseDto>(masterDoctorList.Result);

                if (masterDoctorList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, MasterDoctorApiConstantsResponseMessage.masterdoctor_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedMasterDoctors;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, MasterDoctorApiConstantsResponseMessage.masterdoctor_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, MasterDoctorApiConstantsResponseMessage.masterdoctor_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        [HttpGet("get-master-doctor-by-id")]
        public async Task<ActionResult<ApiResponse<MasterDoctorApiResponseDto>>> GetMasterDoctorById(int masterDoctorId)
        {
            var apiResponse = new ApiResponse<MasterDoctorApiResponseDto>();
            try
            {
                var masterDoctor = await _masterDoctorService.GetById(masterDoctorId);
                var mappedMasterDoctor = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.MasterDoctor, MasterDoctorApiResponseDto>(masterDoctor.Result);

                if (masterDoctor.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, MasterDoctorApiConstantsResponseMessage.masterdoctor_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedMasterDoctor;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, MasterDoctorApiConstantsResponseMessage.masterdoctor_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, MasterDoctorApiConstantsResponseMessage.masterdoctor_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        [HttpPost("create-master-doctor")]
        public async Task<ActionResult<ApiResponse<int>>> CreateMasterDoctor(MasterDoctorInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _masterDoctorService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, MasterDoctorApiConstantsResponseMessage.masterdoctor_insert_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, MasterDoctorApiConstantsResponseMessage.masterdoctor_inserted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, MasterDoctorApiConstantsResponseMessage.masterdoctor_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        [HttpPut("update-master-doctor")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateMasterDoctor(MasterDoctorUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _masterDoctorService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, MasterDoctorApiConstantsResponseMessage.masterdoctor_update_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, MasterDoctorApiConstantsResponseMessage.masterdoctor_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, MasterDoctorApiConstantsResponseMessage.masterdoctor_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        [HttpDelete("delete-master-doctor-by-id")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteMasterDoctor(int masterDoctorId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _masterDoctorService.Delete(masterDoctorId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, MasterDoctorApiConstantsResponseMessage.masterdoctor_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, MasterDoctorApiConstantsResponseMessage.masterdoctor_deleted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, MasterDoctorApiConstantsResponseMessage.masterdoctor_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("get-master-doctor-list-by-doctor-id")]
        public async Task<ActionResult<ApiResponse<List<MasterDoctorApiResponseDto>>>> GetMasterDoctorListByDoctorId(int doctorId)
        {
            var apiResponse = new ApiResponse<List<MasterDoctorApiResponseDto>>();
            try
            {
                var masterDoctorList = await _masterDoctorService.GetByDoctorId(doctorId);
                var mappedMasterDoctors = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.MasterDoctor, MasterDoctorApiResponseDto>(masterDoctorList.Result);

                if (masterDoctorList.Result == null || masterDoctorList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, MasterDoctorApiConstantsResponseMessage.masterdoctor_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedMasterDoctors;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, MasterDoctorApiConstantsResponseMessage.masterdoctor_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, MasterDoctorApiConstantsResponseMessage.masterdoctor_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}

