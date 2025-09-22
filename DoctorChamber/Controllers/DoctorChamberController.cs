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
using DoctorChamber.Application.Services;
using DoctorChamber.Utility;
using DoctorChamber.Dtos.ResponseDtoDoctorChamberDto;
using DoctorChamber.Dtos.RequestDto.DoctorChamberDto;

namespace DoctorChamber.Controllers
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
    }
}
