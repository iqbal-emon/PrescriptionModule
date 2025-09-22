using Diagnonosis.Dtos.ResponseDto.DiagnonosisDto;
using Diagononosis.Utility;
using Diagonosis.Application.Services;
using Diagonosis.Dtos.RequestDto.DiagonosisDto;
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

namespace Diagonosis.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class DiagonosisController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly DiagonosisService _diagonosisService;

        public DiagonosisController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            DiagonosisService diagonosisService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _diagonosisService = diagonosisService;
        }

        [Authorize(Policy = PermissionConstants.DiagonosisGetAll)]
        [HttpGet("gets-all-diagnosis")]
        public async Task<ActionResult<ApiResponse<List<DiagonosisApiResponseDto>>>> GetAllDiagnosis()
        {
            var apiResponse = new ApiResponse<List<DiagonosisApiResponseDto>>();
            try
            {
                var diagnosisList = await _diagonosisService.GetAll();

                var mappedDiagnoses = await _mapperService.MapList<Entities.EntityClass.Diagonosis, DiagonosisApiResponseDto>(diagnosisList.Result);

                if (diagnosisList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DiagononosisApiConstantsResponseMessage.diagnosis_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDiagnoses;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DiagononosisApiConstantsResponseMessage.diagnosis_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DiagononosisApiConstantsResponseMessage.diagnosis_see_try_catch);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.DiagonosisGetAll)]
        [HttpGet("gets-bookmarks-diagnosis")]
        public async Task<ActionResult<ApiResponse<List<DiagonosisApiResponseDto>>>> GetHighlyUsedDiagnosis(int doctorId)
        {
            var apiResponse = new ApiResponse<List<DiagonosisApiResponseDto>>();
            try
            {
                var symptoms = await _diagonosisService.GetBookMarks(doctorId);

                var mappedSymptoms = await _mapperService.MapList<Entities.EntityClass.Diagonosis, DiagonosisApiResponseDto>(symptoms.Result);

                if (symptoms.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DiagononosisApiConstantsResponseMessage.diagnosis_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSymptoms;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DiagononosisApiConstantsResponseMessage.diagnosis_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DiagononosisApiConstantsResponseMessage.diagnosis_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DiagonosisGetAll)]
        [HttpGet("gets-diagnosis-by-name")]
        public async Task<ActionResult<ApiResponse<List<DiagonosisApiResponseDto>>>> GetDiagnosisByName(string? diagnosisName = null)
        {
            var apiResponse = new ApiResponse<List<DiagonosisApiResponseDto>>();
            try
            {
                var diagnosisList = await _diagonosisService.GetAllByName(diagnosisName);

                var mappedDiagnoses = await _mapperService.MapList<Entities.EntityClass.Diagonosis, DiagonosisApiResponseDto>(diagnosisList.Result);

                if (diagnosisList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DiagononosisApiConstantsResponseMessage.diagnosis_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDiagnoses;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DiagononosisApiConstantsResponseMessage.diagnosis_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DiagononosisApiConstantsResponseMessage.diagnosis_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DiagonosisGetId)]
        [HttpGet("get-diagnosis-by-id")]
        public async Task<ActionResult<ApiResponse<DiagonosisApiResponseDto>>> GetDiagnosisById(int DiagnosisId)
        {
            var apiResponse = new ApiResponse<DiagonosisApiResponseDto>();
            try
            {
                var diagnosis = await _diagonosisService.GetById(DiagnosisId);
                var mappedDiagnosis = await _mapperService.MapSingle<Entities.EntityClass.Diagonosis, DiagonosisApiResponseDto>(diagnosis.Result);
                if (diagnosis.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DiagononosisApiConstantsResponseMessage.diagnosis_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDiagnosis;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DiagononosisApiConstantsResponseMessage.diagnosis_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DiagononosisApiConstantsResponseMessage.diagnosis_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DiagonosisCreate)]
        [HttpPost("create-diagnosis")]
        public async Task<ActionResult<ApiResponse<int>>> CreateDiagnosis(DiagnonosisInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _diagonosisService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DiagononosisApiConstantsResponseMessage.diagnosis_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DiagononosisApiConstantsResponseMessage.diagnosis_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DiagononosisApiConstantsResponseMessage.diagnosis_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DiagonosisUpdate)]
        [HttpPut("update-diagnosis")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDiagnosis(DiagnonosisUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _diagonosisService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DiagononosisApiConstantsResponseMessage.diagnosis_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DiagononosisApiConstantsResponseMessage.diagnosis_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DiagononosisApiConstantsResponseMessage.diagnosis_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DiagonosisDelete)]
        [HttpDelete("delete-diagnosis-by-id")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDiagnosis(int DiagnosisId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _diagonosisService.Delete(DiagnosisId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DiagononosisApiConstantsResponseMessage.diagnosis_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, DiagononosisApiConstantsResponseMessage.diagnosis_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, DiagononosisApiConstantsResponseMessage.diagnosis_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
