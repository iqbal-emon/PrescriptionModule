using DoctorPrescription.Application.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using Symptoms.Dtos.RequestDto.SymptomDto;
using Symptoms.Dtos.RequestDto.SymtomDto;
using Symptoms.Dtos.ResponseDto.SymtomDto;
using symtom.Utility;
using System.Data;
using System.Security;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace Symptoms.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class SymptomsController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly SymptomService _symptomService;

        public SymptomsController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            SymptomService symptomService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _symptomService = symptomService;
        }

        [Authorize(Policy = PermissionConstants.SymptomGetAll)]
        [HttpGet("gets-all-chief-complaint")]
        public async Task<ActionResult<ApiResponse<List<SymptomsApiResponseDto>>>> GetAllSymptom()
        {
            var apiResponse = new ApiResponse<List<SymptomsApiResponseDto>>();
            try
            {
                var symptoms = await _symptomService.GetAll();

                var mappedSymptoms = await _mapperService.MapList<Entities.EntityClass.Symptom, SymptomsApiResponseDto>(symptoms.Result);

                if (symptoms.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, SymptomApiConstantsResponseMessage.symptom_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSymptoms;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, SymptomApiConstantsResponseMessage.symptom_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, SymptomApiConstantsResponseMessage.symptom_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.SymptomGetAll)]
        [HttpGet("gets-bookmarks-chief-complaints")]
        public async Task<ActionResult<ApiResponse<List<SymptomsApiResponseDto>>>> GetHighlyUsedSymtops(int doctorId)
        {
            var apiResponse = new ApiResponse<List<SymptomsApiResponseDto>>();
            try
            {
                var symptoms = await _symptomService.GetBookMarks(doctorId);

                var mappedSymptoms = await _mapperService.MapList<Entities.EntityClass.Symptom, SymptomsApiResponseDto>(symptoms.Result);

                if (symptoms.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, SymptomApiConstantsResponseMessage.symptom_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSymptoms;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, SymptomApiConstantsResponseMessage.symptom_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, SymptomApiConstantsResponseMessage.symptom_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.SymptomGetAll)]
        [HttpGet("gets-chief-complaint-by-name")]
        public async Task<ActionResult<ApiResponse<List<SymptomsApiResponseDto>>>> GetAllSymptomByName(string? SymtomName = null)
        {
            var apiResponse = new ApiResponse<List<SymptomsApiResponseDto>>();
            try
            {
                var symptoms = await _symptomService.GetAllSymptomByName(SymtomName);

                var mappedSymptoms = await _mapperService.MapList<Entities.EntityClass.Symptom, SymptomsApiResponseDto>(symptoms.Result);

                if (symptoms.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, SymptomApiConstantsResponseMessage.symptom_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSymptoms;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, SymptomApiConstantsResponseMessage.symptom_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, SymptomApiConstantsResponseMessage.symptom_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.SymptomGetId)]
        [HttpGet("get-chief-complaint-by-id")]
        public async Task<ActionResult<ApiResponse<SymptomsApiResponseDto>>> GetSymptomById(int symptomId)
        {
            var apiResponse = new ApiResponse<SymptomsApiResponseDto>();
            try
            {
                var symptom = await _symptomService.GetById(symptomId);
                var mappedSymptom = await _mapperService.MapSingle<Entities.EntityClass.Symptom, SymptomsApiResponseDto>(symptom.Result);
                if (symptom.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, SymptomApiConstantsResponseMessage.symptom_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSymptom;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, SymptomApiConstantsResponseMessage.symptom_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, SymptomApiConstantsResponseMessage.symptom_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.SymptomCreate)]
        [HttpPost("create-chief-complaint")]
        public async Task<ActionResult<ApiResponse<int>>> CreateSymptom(SymptomsInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _symptomService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, SymptomApiConstantsResponseMessage.symptom_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, SymptomApiConstantsResponseMessage.symptom_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, SymptomApiConstantsResponseMessage.symptom_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.SymptomUpdate)]
        [HttpPut("update-chief-complaint")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateSymptom(SymptomsUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _symptomService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, SymptomApiConstantsResponseMessage.symptom_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, SymptomApiConstantsResponseMessage.symptom_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, SymptomApiConstantsResponseMessage.symptom_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.SymptomDelete)]
        [HttpDelete("delete-chief-complaint-by-id")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteSymptom(int symptomId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _symptomService.Delete(symptomId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, SymptomApiConstantsResponseMessage.symptom_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, SymptomApiConstantsResponseMessage.symptom_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, SymptomApiConstantsResponseMessage.symptom_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
