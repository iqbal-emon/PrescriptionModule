using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.MapService;
using Speciality.Application.Services;
using Speciality.Dtos.RequestDto.SpecialityDto;
using Speciality.Dtos.ResponseDto.SpecialityDto;
using Speciality.Utility;
using System;
using System.Collections.Generic;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace AuthenticationSystem.Controllers
{
    [ApiController]
    [Route("api/app/speciality")]
    public class SpecialityMainApiController : ControllerBase
    {
        private readonly SpecialityService _specialityService;
        private readonly MapperService _mapperService;

        public SpecialityMainApiController(SpecialityService specialityService, MapperService mapperService)
        {
            _specialityService = specialityService;
            _mapperService = mapperService;
        }

        [HttpPost]
        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreateSpeciality([FromBody] SpecialityInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _specialityService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, SpecialityApiConstantsResponseMessage.speciality_insert_success_message);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, SpecialityApiConstantsResponseMessage.speciality_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, SpecialityApiConstantsResponseMessage.speciality_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        public async Task<ActionResult<ApiResponse<SpecialityApiResponseDto>>> GetSpecialityById(int id)
        {
            var apiResponse = new ApiResponse<SpecialityApiResponseDto>();
            try
            {
                var speciality = await _specialityService.GetById(id);
                if (speciality.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, SpecialityApiConstantsResponseMessage.speciality_null_of_get_list);
                    return Ok(apiResponse);
                }

                var mappedSpeciality = await _mapperService.MapSingle<Entities.EntityClass.Speciality, SpecialityApiResponseDto>(speciality.Result);
                apiResponse.Results = mappedSpeciality;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, SpecialityApiConstantsResponseMessage.speciality_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, SpecialityApiConstantsResponseMessage.speciality_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<SpecialityApiResponseDto>>>> GetAllSpecialities()
        {
            var apiResponse = new ApiResponse<List<SpecialityApiResponseDto>>();
            try
            {
                var specialities = await _specialityService.GetAll();
                if (specialities.Result == null || specialities.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecialityApiResponseDto>(), SpecialityApiConstantsResponseMessage.speciality_null_of_get_list);
                    return Ok(apiResponse);
                }

                var mappedSpecialities = await _mapperService.MapList<Entities.EntityClass.Speciality, SpecialityApiResponseDto>(specialities.Result);
                apiResponse.Results = mappedSpecialities;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, SpecialityApiConstantsResponseMessage.speciality_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<SpecialityApiResponseDto>(), SpecialityApiConstantsResponseMessage.speciality_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpPut]
        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateSpeciality([FromBody] SpecialityUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _specialityService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, SpecialityApiConstantsResponseMessage.speciality_update_success_message);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, SpecialityApiConstantsResponseMessage.speciality_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, SpecialityApiConstantsResponseMessage.speciality_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteSpeciality(int id)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _specialityService.Delete(id);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, SpecialityApiConstantsResponseMessage.speciality_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, SpecialityApiConstantsResponseMessage.speciality_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, SpecialityApiConstantsResponseMessage.speciality_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}

