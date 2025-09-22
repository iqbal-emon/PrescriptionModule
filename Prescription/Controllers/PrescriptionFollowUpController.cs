using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prescription.Application.Services;
using Prescription.Dtos.RequestDto.PrescriptionFollowUpDto;
using Prescription.Dtos.ResponseDto.PrescriptionFollowUpDto;
using Prescription.Utility;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;

namespace Prescription.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PrescriptionFollowUpController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly PrescriptionFollowUpService _prescriptionFollowUpService;
        public PrescriptionFollowUpController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            PrescriptionFollowUpService prescriptionFollowUpService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _prescriptionFollowUpService = prescriptionFollowUpService;
        }
        [Authorize(Policy = PermissionConstants.PrescriptionFollowUpGetAll)]
        [HttpGet("gets-all-prescription-followup")]
        public async Task<ActionResult<ApiResponse<List<PrescriptionFollowApiResponseDto>>>> GetAllPrescriptionFollowUp()
        {
            var apiResponse = new ApiResponse<List<PrescriptionFollowApiResponseDto>>();
            try
            {
                var prescriptionFollowUps = await _prescriptionFollowUpService.GetAll();

                var mappedPrescriptionFollowUps = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp, PrescriptionFollowApiResponseDto>(prescriptionFollowUps.Result);

                if (prescriptionFollowUps.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescriptionFollowUps;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_see_try_catch);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.PrescriptionFollowUpGetId)]
        [HttpGet("get-prescription-followup-by-id")]
        public async Task<ActionResult<ApiResponse<PrescriptionFollowApiResponseDto>>> GetPrescriptionFollowUpById(int prescriptionFollowUpId)
        {
            var apiResponse = new ApiResponse<PrescriptionFollowApiResponseDto>();
            try
            {
                var prescriptionFollowUps = await _prescriptionFollowUpService.GetById(prescriptionFollowUpId);
                var mappedPrescriptionFollowUps = await _mapperService.MapSingle<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp, PrescriptionFollowApiResponseDto>(prescriptionFollowUps.Result);
                if (prescriptionFollowUps.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescriptionFollowUps;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_see_try_catch);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.PrescriptionFollowUpCreate)]
        [HttpPost("create-prescription-followup")]
        public async Task<ActionResult<ApiResponse<int>>> CreatePrescriptionFollowUp(PrescriptionFollowUpInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionFollowUpService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.MedicationUpdate)]
        [HttpPut("update-prescription-followup")]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePrescriptionFollowUp(PrescriptionFollowUpUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionFollowUpService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.MedicationDelete)]
        [HttpDelete("delete-prescription-followup")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePrescriptionFollowUp(int prescriptionFollowUpId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionFollowUpService.Delete(prescriptionFollowUpId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionFollowUpApiConstantsResponseMessage.prescription_followup_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }










    }
}
