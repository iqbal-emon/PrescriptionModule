using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prescription.Application.services;
using Prescription.Dtos.RequestDto.PrescriptionInvestigationDto;
using Prescription.Dtos.ResponseDto.PrescriptionInvestigationDto;
using Prescription.Utility;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;

namespace PrescriptionInvestigation.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PrescriptionInvestigationController: ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly PrescriptionInvestigationService _prescriptionInvestigationService;

        public PrescriptionInvestigationController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            PrescriptionInvestigationService prescriptionInvestigationService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _prescriptionInvestigationService = prescriptionInvestigationService;
        }

        [Authorize(Policy = PermissionConstants.PrescriptionInvestigationGetAll)]
        [HttpGet("gets-all-prescription-investigations")]
        public async Task<ActionResult<ApiResponse<List<PrescriptionInvestigationApiResponseDto>>>> GetAllPrescriptionInvestigations()
        {
            var apiResponse = new ApiResponse<List<PrescriptionInvestigationApiResponseDto>>();
            try
            {
                var investigations = await _prescriptionInvestigationService.GetAll();
                var mappedInvestigations = await _mapperService.MapList<Entities.EntityClass.PatientEntity.PrescriptionInvestigation, PrescriptionInvestigationApiResponseDto>(investigations.Result);

                if (investigations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedInvestigations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionInvestigationGetId)]
        [HttpGet("get-prescription-investigation-by-id")]
        public async Task<ActionResult<ApiResponse<PrescriptionInvestigationApiResponseDto>>> GetPrescriptionInvestigationById(int prescriptionInvestigationId)
        {
            var apiResponse = new ApiResponse<PrescriptionInvestigationApiResponseDto>();
            try
            {
                var investigations = await _prescriptionInvestigationService.GetById(prescriptionInvestigationId);
                var mappedInvestigations = await _mapperService.MapSingle<Entities.EntityClass.PatientEntity.PrescriptionInvestigation, PrescriptionInvestigationApiResponseDto>(investigations.Result);
                if (investigations.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedInvestigations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionInvestigationCreate)]
        [HttpPost("create-prescription-investigation")]
        public async Task<ActionResult<ApiResponse<int>>> CreatePrescriptionInvestigation(PrescriptionInvestigationInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionInvestigationService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionInvestigationUpdate)]
        [HttpPut("update-prescription-investigation")]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePrescriptionInvestigation(PrescriptionInvestigationUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionInvestigationService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionInvestigationDelete)]
        [HttpDelete("delete-prescription-investigation")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePrescriptionInvestigation(int prescriptionInvestigationId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionInvestigationService.Delete(prescriptionInvestigationId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionInvestigationApiConstantsResponseMessage.prescription_investigation_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
