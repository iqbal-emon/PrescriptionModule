using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prescription.Dtos.RequestDto.PrescriptionExaminationDto;
using Prescription.Dtos.ResponseDto.PrescriptionExaminationDto;
using Prescription.Utility;
using PrescriptionExamination.Application.Services;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;

namespace Prescription.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PrescriptionExaminationController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly PrescriptionExaminationService _prescriptionExaminationService;

        public PrescriptionExaminationController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            PrescriptionExaminationService prescriptionExaminationService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _prescriptionExaminationService = prescriptionExaminationService;
        }

        [Authorize(Policy = PermissionConstants.PrescriptionExaminationGetAll)]
        [HttpGet("gets-all-prescription-examinations")]
        public async Task<ActionResult<ApiResponse<List<PrescriptionExaminationApiResponseDto>>>> GetAllPrescriptionExaminations()
        {
            var apiResponse = new ApiResponse<List<PrescriptionExaminationApiResponseDto>>();
            try
            {
                var prescriptionExaminations = await _prescriptionExaminationService.GetAll();

                var mappedPrescriptionExaminations = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination, PrescriptionExaminationApiResponseDto>(prescriptionExaminations.Result);

                if (prescriptionExaminations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescriptionExaminations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionExaminationGetId)]
        [HttpGet("get-prescription-examination-by-id")]
        public async Task<ActionResult<ApiResponse<PrescriptionExaminationApiResponseDto>>> GetPrescriptionExaminationById(int prescriptionExaminationId)
        {
            var apiResponse = new ApiResponse<PrescriptionExaminationApiResponseDto>();
            try
            {
                var prescriptionExamination = await _prescriptionExaminationService.GetById(prescriptionExaminationId);
                var mappedPrescriptionExamination = await _mapperService.MapSingle<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination, PrescriptionExaminationApiResponseDto>(prescriptionExamination.Result);

                if (prescriptionExamination.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescriptionExamination;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionExaminationCreate)]
        [HttpPost("create-prescription-examination")]
        public async Task<ActionResult<ApiResponse<int>>> CreatePrescriptionExamination(PrescriptionExaminationInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionExaminationService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionExaminationUpdate)]
        [HttpPut("update-prescription-examination")]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePrescriptionExamination(PrescriptionExaminationUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionExaminationService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionExaminationDelete)]
        [HttpDelete("delete-prescription-examination")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePrescriptionExamination(int prescriptionExaminationId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionExaminationService.Delete(prescriptionExaminationId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionExaminationApiConstantsResponseMessage.prescription_examination_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
