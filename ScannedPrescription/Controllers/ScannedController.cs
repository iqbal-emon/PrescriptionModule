using DoctorPrescription.Application.Services;
using ScannedPrescription.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using SharedService.Model;
using System.Data;
using System.Security;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;
using ScannedPrescription.Dtos.RequestDto.ScannedPrescription;
using ScannedPrescription.Dtos.ResponseDto.ScannedPrescriptionDto;

namespace DoctorPrescription.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class ScannedPrescriptionController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly ScannedPrescriptionService _scannedPrescriptionService;
        public ScannedPrescriptionController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            ScannedPrescriptionService scannedPrescriptionService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _scannedPrescriptionService = scannedPrescriptionService;
        }
        [Authorize(Policy = PermissionConstants.ScannedPrescriptionGetAll)]
        [HttpGet("gets-all-scanned-prescriptions")]
        public async Task<ActionResult<ApiResponse<List<ScannedPrescriptionApiResponseDto>>>> GetAllScannedPrescriptions()
        {
            var apiResponse = new ApiResponse<List<ScannedPrescriptionApiResponseDto>>();
            try
            {
                var prescriptions = await _scannedPrescriptionService.GetAll();
                var mappedPrescriptions = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.ScannedPrescription, ScannedPrescriptionApiResponseDto>(prescriptions.Result);

                if (prescriptions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescriptions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_see_try_catch);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.ScannedPrescriptionGetId)]
        [HttpGet("get-scanned-prescription-by-id")]
        public async Task<ActionResult<ApiResponse<ScannedPrescriptionApiResponseDto>>> GetScannedPrescriptionById(int scannedPrescriptionId)
        {
            var apiResponse = new ApiResponse<ScannedPrescriptionApiResponseDto>();
            try
            {
                var prescriptions = await _scannedPrescriptionService.GetById(scannedPrescriptionId);
                var mappedPrescriptions = await _mapperService.MapSingle<Entities.EntityClass.PrescriptionEntity.ScannedPrescription, ScannedPrescriptionApiResponseDto>(prescriptions.Result);
                if (prescriptions.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedPrescriptions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_see_try_catch);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.ScannedPrescriptionCreate)]
        [HttpPost("create-scanned-prescription")]
        public async Task<ActionResult<ApiResponse<int>>> CreateScannedPrescription(ScannedPrescriptionInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _scannedPrescriptionService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.ScannedPrescriptionUpdate)]
        [HttpPut("update-scanned-prescription")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateScannedPrescription(ScannedPrescriptionUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _scannedPrescriptionService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.ScannedPrescriptionDelete)]
        [HttpDelete("delete-scanned-prescription")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteScannedPrescription(int prescriptionAdviceId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _scannedPrescriptionService.Delete(prescriptionAdviceId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, ScannedPrescriptionApiConstantsResponseMessage.scanned_prescription_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
