using DoctorPrescription.Application.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;
using Diseases.Dtos.RequestDto.DiseasesDto;
using Diseases.Dtos.ResponseDto.DiseasesDto;
using Diseases.Utility;

namespace DoctorPrescription.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class DiseasesController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly DiseasesService _diseaseService;
        public DiseasesController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            DiseasesService diseaseService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _diseaseService = diseaseService;
        }

        [Authorize(Policy = PermissionConstants.DiseaseGetAll)]
        [HttpGet("gets-all-diseases")]
        public async Task<ActionResult<ApiResponse<List<DiseasesApiResponseDto>>>> GetAllDiseases()
        {
            var apiResponse = new ApiResponse<List<DiseasesApiResponseDto>>();
            try
            {
                var diseases = await _diseaseService.GetAll();

                var mappedDiseases = await _mapperService.MapList<Entities.EntityClass.Disease, DiseasesApiResponseDto>(diseases.Result);

                if (diseases.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DiseasesApiConstantsResponseMessage.disease_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDiseases;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DiseasesApiConstantsResponseMessage.disease_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DiseasesApiConstantsResponseMessage.disease_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DiseaseGetId)]
        [HttpGet("get-diseases-by-id")]
        public async Task<ActionResult<ApiResponse<DiseasesApiResponseDto>>> GetDiseaseById(int diseaseId)
        {
            var apiResponse = new ApiResponse<DiseasesApiResponseDto>();
            try
            {
                var disease = await _diseaseService.GetById(diseaseId);
                var mappedDisease = await _mapperService.MapSingle<Entities.EntityClass.Disease, DiseasesApiResponseDto>(disease.Result);
                if (disease.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DiseasesApiConstantsResponseMessage.disease_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDisease;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DiseasesApiConstantsResponseMessage.disease_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DiseasesApiConstantsResponseMessage.disease_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DiseaseCreate)]
        [HttpPost("create-diseases")]
        public async Task<ActionResult<ApiResponse<int>>> CreateDisease(DiseasesInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _diseaseService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DiseasesApiConstantsResponseMessage.disease_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DiseasesApiConstantsResponseMessage.disease_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DiseasesApiConstantsResponseMessage.disease_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DiseaseUpdate)]
        [HttpPut("update-diseases")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDisease(DiseasesUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _diseaseService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DiseasesApiConstantsResponseMessage.disease_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DiseasesApiConstantsResponseMessage.disease_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DiseasesApiConstantsResponseMessage.disease_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DiseaseDelete)]
        [HttpDelete("delete-diseases")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDisease(int diseaseId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _diseaseService.Delete(diseaseId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DiseasesApiConstantsResponseMessage.disease_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, DiseasesApiConstantsResponseMessage.disease_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, DiseasesApiConstantsResponseMessage.disease_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
