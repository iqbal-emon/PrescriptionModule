using Entities.EntityClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrescriptionPatientHistory.Application.Services;
using PrescriptionPatientHistory.Dtos.RequestDto.PrescriptionPatientHistoryDto;
using PrescriptionPatientHistory.Dtos.ResponseDto.PrescriptionPatientHistory;
using PrescriptionPatientHistory.Utility;
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

namespace PrescriptionPatientHistory.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PrescriptionPatientHistoryController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly PrescriptionPatientHistoryService _prescriptionPatientHistoryService;

        public PrescriptionPatientHistoryController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            PrescriptionPatientHistoryService prescriptionPatientHistoryService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _prescriptionPatientHistoryService = prescriptionPatientHistoryService;
        }

        [Authorize(Policy = PermissionConstants.PrescriptionPatientHistoryGetAll)]
        [HttpGet("gets-all-prescription-patient-history")]
        public async Task<ActionResult<ApiResponse<List<PrescriptionPatientHistoryApiResponseDto>>>> GetAllPrescriptionPatientHistory()
        {
            var apiResponse = new ApiResponse<List<PrescriptionPatientHistoryApiResponseDto>>();
            try
            {
                var prescriptionPatientHistories = await _prescriptionPatientHistoryService.GetAll();

                var mappedPrescriptionPatientHistories = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory, PrescriptionPatientHistoryApiResponseDto>(prescriptionPatientHistories.Result);

                if (prescriptionPatientHistories.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescriptionPatientHistories;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionPatientHistoryGetAll)]
        [HttpGet("gets-all-prescription-patient-history-by-name")]
        public async Task<ActionResult<ApiResponse<List<PrescriptionPatientHistoryApiResponseDto>>>> GetAllPrescriptionPatientHistoryByName(string? PrescriptionPatientHistoryName = null)
        {
            var apiResponse = new ApiResponse<List<PrescriptionPatientHistoryApiResponseDto>>();
            try
            {
                var prescriptionPatientHistories = await _prescriptionPatientHistoryService.GetAllPrescriptionPatientHistoryByName(PrescriptionPatientHistoryName);

                var mappedPrescriptionPatientHistories = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory, PrescriptionPatientHistoryApiResponseDto>(prescriptionPatientHistories.Result);

                if (prescriptionPatientHistories.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescriptionPatientHistories;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionPatientHistoryGetId)]
        [HttpGet("get-prescription-patient-history-by-id")]
        public async Task<ActionResult<ApiResponse<PrescriptionPatientHistoryApiResponseDto>>> GetPrescriptionPatientHistoryById(int prescriptionPatientHistoryId)
        {
            var apiResponse = new ApiResponse<PrescriptionPatientHistoryApiResponseDto>();
            try
            {
                var prescriptionPatientHistory = await _prescriptionPatientHistoryService.GetById(prescriptionPatientHistoryId);
                var mappedPrescriptionPatientHistory = await _mapperService.MapSingle<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory, PrescriptionPatientHistoryApiResponseDto>(prescriptionPatientHistory.Result);
                if (prescriptionPatientHistory.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescriptionPatientHistory;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionPatientHistoryGetId)]
        [HttpGet("get-prescription-patient-history-previous")]
        public async Task<ActionResult<ApiResponse<List<PrescriptionCommonHistoryDto>>>> GetPrescriptionPatientHistoryPrevious(int patientId)
        {
            var apiResponse = new ApiResponse<List<PrescriptionCommonHistoryDto>>();
            try
            {
                var prescriptionPatientHistoryResponse = await _prescriptionPatientHistoryService.GetAllPrescriptionPatientHistoryPrevious(patientId);

                // Ensure response is valid and not empty
                if (prescriptionPatientHistoryResponse.Result.Count==0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_null_of_get_list);
                    return Ok(apiResponse);
                }

                // Map list correctly
                var mappedPrescriptionPatientHistories = await _mapperService.MapList<Entities.EntityClass.CommonHistory, PrescriptionCommonHistoryDto>(prescriptionPatientHistoryResponse.Result);

                apiResponse.Results = mappedPrescriptionPatientHistories;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_see_try_catch);
            }
            return Ok(apiResponse);
        }





        [Authorize(Policy = PermissionConstants.PrescriptionPatientHistoryCreate)]
        [HttpPost("create-prescription-patient-history")]
        public async Task<ActionResult<ApiResponse<int>>> CreatePrescriptionPatientHistory(PrescriptionPatientHistoryRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionPatientHistoryService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionPatientHistoryUpdate)]
        [HttpPut("update-prescription-patient-history")]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePrescriptionPatientHistory(PrescriptionPatientHistoryUpdateDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionPatientHistoryService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionPatientHistoryDelete)]
        [HttpDelete("delete-prescription-patient-history")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePrescriptionPatientHistory(int prescriptionPatientHistoryId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionPatientHistoryService.Delete(prescriptionPatientHistoryId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionPatientHistoryApiConstantsResponseMessage.prescription_patient_history_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
