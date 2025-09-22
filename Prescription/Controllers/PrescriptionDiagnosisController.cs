using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prescription.Application.Services;
using Prescription.Dtos.RequestDto.PrescriptionDiagonosisDto;
using Prescription.Dtos.ResponseDto.PrescriptionDiagonosisDto;
using Prescription.Utility;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;

namespace Prescription.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PrescriptionDiagnosisController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly PrescriptionDiagonsisService _prescriptionDiagonosisService;
        public PrescriptionDiagnosisController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            PrescriptionDiagonsisService prescriptionDiagnosisService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _prescriptionDiagonosisService = prescriptionDiagnosisService;
        }

        [Authorize(Policy = PermissionConstants.DiagonosisGetAll)]
        [HttpGet("gets-all-prescription-diagonosis")]
        public async Task<ActionResult<ApiResponse<List<PrescriptionDiagonosisApiResponseDto>>>> GetAllPrescriptionDiagonosis()
        {
            var apiResponse = new ApiResponse<List<PrescriptionDiagonosisApiResponseDto>>();
            try
            {
                var diagnoses = await _prescriptionDiagonosisService.GetAll();

                var mappedDiagnoses = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis, PrescriptionDiagonosisApiResponseDto>(diagnoses.Result);

                if (diagnoses.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDiagnoses;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_see_try_catch);
            }
            return Ok(apiResponse);
        }

        //[Authorize(Policy = PermissionConstants.DiagonosisGetId)]
        //[HttpGet("get-prescription-diagonosis-by-id")]
        //public async Task<ActionResult<ApiResponse<PrescriptionDiagonosisApiResponseDto>>> GetPrescriptionDiagonosisById(int PrescriptionDiagonosisId)
        //{
        //    var apiResponse = new ApiResponse<PrescriptionDiagonosisApiResponseDto>();
        //    try
        //    {
        //        var PrescriptionDiagonosis = await _prescriptionDiagonosisService.GetById(PrescriptionDiagonosisId);
        //        var mappedPrescriptionDiagonosis = await _mapperService.MapSingle<Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis, PrescriptionDiagonosisApiResponseDto>(PrescriptionDiagonosis.Result);
        //        if (PrescriptionDiagonosis.Result == null)
        //        {
        //            ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_null_of_get_list);
        //            return Ok(apiResponse);
        //        }

        //        apiResponse.Results = mappedPrescriptionDiagonosis;
        //        ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_see_try_catch);
        //    }
        //    return Ok(apiResponse);
        //}

        [Authorize(Policy = PermissionConstants.DiagonosisCreate)]
        [HttpPost("create-prescription-diagonosis")]
        public async Task<ActionResult<ApiResponse<int>>> CreatePrescriptionDiagonosis(PrescriptionDiagonosisInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionDiagonosisService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DiagonosisUpdate)]
        [HttpPut("update-prescription-Diagonosis")]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePrescriptionDiagonosis(PrescriptionDiagonosisUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionDiagonosisService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DiagonosisDelete)]
        [HttpDelete("delete-prescription-diagonosis")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePrescriptionDiagonosis(int PrescriptionDiagonosisId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionDiagonosisService.Delete(PrescriptionDiagonosisId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionDiagonosisApiConstantsResponseMessage.diagonosis_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
