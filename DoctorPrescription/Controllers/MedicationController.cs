using Medication.Application.Services;
using Medication.Dtos.ResponseDto.MedicationDto;
using Medication.Dtos.RquestDto.MedicatonDto;
using Medication.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;

namespace Medication.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class MedicationController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly MedicationService _medicationService;
        public MedicationController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            MedicationService medicationService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _medicationService = medicationService;
        }
        [Authorize(Policy = PermissionConstants.MedicationGetAll)]
        [HttpGet("gets-all-medication")]
        public async Task<ActionResult<ApiResponse<List<MedicationApiResponseDto>>>> GetAllMedication()
        {
            var apiResponse = new ApiResponse<List<MedicationApiResponseDto>>();
            try
            {
                var medications = await _medicationService.GetAll();

                var mappedMedications = await _mapperService.MapList<Entities.EntityClass.MedicineEntity.Medication, MedicationApiResponseDto>(medications.Result);

                if (medications.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationApiConstantsResponseMessage.medication_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedMedications;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, MedicationApiConstantsResponseMessage.medication_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationApiConstantsResponseMessage.medication_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.MedicationGetAll)]
        [HttpGet("gets-most-used-medication")]
        public async Task<ActionResult<ApiResponse<List<MedicationMostUsedDto>>>> GetAllMedicationMostUsedn([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var apiResponse = new ApiResponse<List<MedicationMostUsedDto>>();

            try
            {
                var medications = await _medicationService.GetAllMedicineMostUsed(pageNumber, pageSize);

                if (medications.Result == null || medications.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationApiConstantsResponseMessage.medication_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = medications.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, MedicationApiConstantsResponseMessage.medication_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationApiConstantsResponseMessage.medication_see_try_catch);
            }

            return Ok(apiResponse);
        }


        [Authorize(Policy = PermissionConstants.MedicationGetAll)]
        [HttpGet("gets-bookmarks-medication")]
        public async Task<ActionResult<ApiResponse<List<MedicationApiResponseDto>>>> GetHighlyUsedMedication(int doctorId)
        {
            var apiResponse = new ApiResponse<List<MedicationApiResponseDto>>();
            try
            {
                var medications = await _medicationService.GetBookMarks(doctorId);

                var mappedSymptoms = await _mapperService.MapList<Entities.EntityClass.MedicineEntity.Medication, MedicationApiResponseDto>(medications.Result);

                if (medications.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationApiConstantsResponseMessage.medication_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSymptoms;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, MedicationApiConstantsResponseMessage.medication_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationApiConstantsResponseMessage.medication_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.MedicationGetId)]
        [HttpGet("get-medication-by-id")]
        public async Task<ActionResult<ApiResponse<MedicationApiResponseDto>>> GetMedicationById(int medicationId)
        {
            var apiResponse = new ApiResponse<MedicationApiResponseDto>();
            try
            {
                var medications = await _medicationService.GetById(medicationId);
                var mappedMedications = await _mapperService.MapSingle<Entities.EntityClass.MedicineEntity.Medication, MedicationApiResponseDto>(medications.Result);
                if (medications.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationApiConstantsResponseMessage.medication_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedMedications;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, MedicationApiConstantsResponseMessage.medication_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationApiConstantsResponseMessage.medication_see_try_catch);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.MedicationCreate)]
        [HttpPost("create-medication")]
        public async Task<ActionResult<ApiResponse<int>>> CreateMedication(MedicationInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {

                if (ModelState.IsValid)
                {
                    var response = await _medicationService.Insert(request);
                   
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, MedicationApiConstantsResponseMessage.medication_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, MedicationApiConstantsResponseMessage.medication_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, MedicationApiConstantsResponseMessage.medication_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.MedicationUpdate)]
        [HttpPut("update-medication")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateMedication(MedicationUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _medicationService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, MedicationApiConstantsResponseMessage.medication_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, MedicationApiConstantsResponseMessage.medication_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, MedicationApiConstantsResponseMessage.medication_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.MedicationDelete)]
        [HttpDelete("delete-medication")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteMedication(int MedicationId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _medicationService.Delete(MedicationId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, MedicationApiConstantsResponseMessage.medication_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, MedicationApiConstantsResponseMessage.medication_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, MedicationApiConstantsResponseMessage.medication_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.MedicationGetId)]
        [HttpGet("get-medication-by-name")]
        public async Task<ActionResult<ApiResponse<List<MedicationApiResponseDto>>>> GetMedicationByName(string? medicationName)
        {
            var apiResponse = new ApiResponse<List<MedicationApiResponseDto>>();
            try
            {
                string uniCode = string.Empty;

                if (!string.IsNullOrEmpty(medicationName))
                {
                    uniCode = await _sharedCommonService.ContainsBengali(medicationName);
                }


                var medications = await _medicationService.GetMedicationByName(medicationName, uniCode);

                var mappedMedications = await _mapperService.MapList<Entities.EntityClass.MedicineEntity.Medication, MedicationApiResponseDto>(medications.Result);
                if (medications.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationApiConstantsResponseMessage.medication_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedMedications;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, MedicationApiConstantsResponseMessage.medication_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationApiConstantsResponseMessage.medication_see_try_catch);
            }
            return Ok(apiResponse);
        }

    }
}
