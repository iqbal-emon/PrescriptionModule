using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prescription.Application.Services;
using Prescription.Dtos.RequestDto.PrescriptionAdvice;
using Prescription.Dtos.ResponseDto.PrescriptionAdvice;
using Prescription.Utility;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;

namespace Prescription.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PrescriptionAdviceController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly PrescriptionAdviceService _prescriptionAdviceService;

        public PrescriptionAdviceController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            PrescriptionAdviceService prescriptionAdviceService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _prescriptionAdviceService = prescriptionAdviceService;
        }

        [Authorize(Policy = PermissionConstants.PrescriptionAdviceGetAll)]
        [HttpGet("gets-all-prescription-advice")]
        public async Task<ActionResult<ApiResponse<List<PrescriptionAdviceApiResponseDto>>>> GetAllPrescriptionAdvice()
        {
            var apiResponse = new ApiResponse<List<PrescriptionAdviceApiResponseDto>>();
            try
            {
                var prescriptionAdvices = await _prescriptionAdviceService.GetAll();

                var mappedPrescriptionAdvices = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice, PrescriptionAdviceApiResponseDto>(prescriptionAdvices.Result);

                if (prescriptionAdvices.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescriptionAdvices;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionAdviceGetId)]
        [HttpGet("get-prescription-advice-by-id")]
        public async Task<ActionResult<ApiResponse<PrescriptionAdviceApiResponseDto>>> GetPrescriptionAdviceById(int prescriptionAdviceId)
        {
            var apiResponse = new ApiResponse<PrescriptionAdviceApiResponseDto>();
            try
            {
                var prescriptionAdvice = await _prescriptionAdviceService.GetById(prescriptionAdviceId);
                var mappedPrescriptionAdvice = await _mapperService.MapSingle<Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice, PrescriptionAdviceApiResponseDto>(prescriptionAdvice.Result);

                if (prescriptionAdvice.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescriptionAdvice;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionAdviceCreate)]
        [HttpPost("create-prescription-advice")]
        public async Task<ActionResult<ApiResponse<int>>> CreatePrescriptionAdvice(PrescriptionAdviceInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionAdviceService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionAdviceUpdate)]
        [HttpPut("update-prescription-advice")]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePrescriptionAdvice(PrescriptionAdviceUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionAdviceService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionAdviceDelete)]
        [HttpDelete("delete-prescription-advice")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePrescriptionAdvice(int prescriptionAdviceId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionAdviceService.Delete(prescriptionAdviceId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionAdviceApiConstantsResponseMessage.prescription_advice_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
