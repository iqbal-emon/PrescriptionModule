using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prescription.Application.Services;
using Prescription.Dtos.RequestDto.PrescriptionSymtomDto;
using Prescription.Dtos.ResponseDto.PrescriptionSymtomDto;
using Prescription.Utility;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;

namespace Prescription.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PrescriptionSymptomController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly PrescriptionSymptomService _prescriptionSymptomService;

        public PrescriptionSymptomController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            PrescriptionSymptomService prescriptionSymptomService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _prescriptionSymptomService = prescriptionSymptomService;
        }

        [Authorize(Policy = PermissionConstants.SymptomGetAll)]
        [HttpGet("gets-all-prescription-symptoms")]
        public async Task<ActionResult<ApiResponse<List<PrescriptionSymtomApiResponseDto>>>> GetAllPrescriptionSymptoms()
        {
            var apiResponse = new ApiResponse<List<PrescriptionSymtomApiResponseDto>>();
            try
            {
                var symptoms = await _prescriptionSymptomService.GetAll();
                var mappedSymptoms = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom, PrescriptionSymtomApiResponseDto>(symptoms.Result);

                if (symptoms.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionSymtomApiConstantsResponseMessage.prescription_symptom_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSymptoms;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionSymtomApiConstantsResponseMessage.prescription_symptom_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionSymtomApiConstantsResponseMessage.prescription_symptom_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.SymptomGetId)]
        [HttpGet("get-prescription-symptom-by-id")]
        public async Task<ActionResult<ApiResponse<PrescriptionSymtomApiResponseDto>>> GetPrescriptionSymptomById(int PrescriptionSymptomId)
        {
            var apiResponse = new ApiResponse<PrescriptionSymtomApiResponseDto>();
            try
            {
                var symptom = await _prescriptionSymptomService.GetById(PrescriptionSymptomId);
                var mappedSymptom = await _mapperService.MapSingle<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom, PrescriptionSymtomApiResponseDto>(symptom.Result);

                if (symptom.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionSymtomApiConstantsResponseMessage.prescription_symptom_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSymptom;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionSymtomApiConstantsResponseMessage.prescription_symptom_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionSymtomApiConstantsResponseMessage.prescription_symptom_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.SymptomCreate)]
        [HttpPost("create-prescription-symptom")]
        public async Task<ActionResult<ApiResponse<int>>> CreatePrescriptionSymptom(PrescriptionSymtomInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionSymptomService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionSymtomApiConstantsResponseMessage.prescription_symptom_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionSymtomApiConstantsResponseMessage.prescription_symptom_inserted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionSymtomApiConstantsResponseMessage.prescription_symptom_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.SymptomUpdate)]
        [HttpPut("update-prescription-symptom")]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePrescriptionSymptom(PrescriptionSymtomUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionSymptomService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionSymtomApiConstantsResponseMessage.prescription_symptom_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionSymtomApiConstantsResponseMessage.prescription_symptom_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionSymtomApiConstantsResponseMessage.prescription_symptom_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.SymptomDelete)]
        [HttpDelete("delete-prescription-symptom")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePrescriptionSymptom(int PrescriptionSymptomId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                var response = await _prescriptionSymptomService.Delete(PrescriptionSymptomId);

                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionSymtomApiConstantsResponseMessage.prescription_symptom_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionSymtomApiConstantsResponseMessage.prescription_symptom_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}
