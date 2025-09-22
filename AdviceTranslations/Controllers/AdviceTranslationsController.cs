using DoctorPrescription.Application.Services;
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
using AdviceTranslations.Dtos.RequestDto.AdviceTranslationsDto;
using AdviceTranslations.Dtos.ResponseDto.AdviceTranslationsDto;
using System.Runtime.InteropServices;
using AdviceTranslations.Utility;

namespace DoctorPrescription.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class AdviceTranslationsController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly AdviceTranslationsService _adviceTranslationsService;

        public AdviceTranslationsController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            AdviceTranslationsService adviceTranslationService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _adviceTranslationsService = adviceTranslationService;
        }

        [Authorize(Policy = PermissionConstants.AdviceTranslationsGetAll)]
        [HttpGet("gets-all-advice-translations")]
        public async Task<ActionResult<ApiResponse<List<AdviceTranslationsApiResonseDto>>>> GetAllAdviceTranslations()
        {
            var apiResponse = new ApiResponse<List<AdviceTranslationsApiResonseDto>>();
            try
            {
                var adviceTranslations = await _adviceTranslationsService.GetAll();

                var mappedAdviceTranslations = await _mapperService.MapList<Entities.EntityClass.AdviceTranslation, AdviceTranslationsApiResonseDto>(adviceTranslations.Result);

                if (adviceTranslations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AdviceTranslationsApiConstantsResponseMessage.advice_translations_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedAdviceTranslations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AdviceTranslationsApiConstantsResponseMessage.advice_translations_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AdviceTranslationsApiConstantsResponseMessage.advice_translations_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.AdviceTranslationsGetId)]
        [HttpGet("get-advice-translation-by-id")]
        public async Task<ActionResult<ApiResponse<AdviceTranslationsApiResonseDto>>> GetAdviceTranslationById(int adviceTranslationId)
        {
            var apiResponse = new ApiResponse<AdviceTranslationsApiResonseDto>();
            try
            {
                var adviceTranslation = await _adviceTranslationsService.GetById(adviceTranslationId);

                var mappedAdviceTranslation = await _mapperService.MapSingle<Entities.EntityClass.AdviceTranslation, AdviceTranslationsApiResonseDto>(adviceTranslation.Result);

                if (adviceTranslation.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AdviceTranslationsApiConstantsResponseMessage.advice_translations_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedAdviceTranslation;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AdviceTranslationsApiConstantsResponseMessage.advice_translations_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AdviceTranslationsApiConstantsResponseMessage.advice_translations_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.AdviceTranslationsCreate)]
        [HttpPost("create-advice-translation")]
        public async Task<ActionResult<ApiResponse<int>>> CreateAdviceTranslation(AdviceTranslationsInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _adviceTranslationsService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AdviceTranslationsApiConstantsResponseMessage.advice_translations_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AdviceTranslationsApiConstantsResponseMessage.advice_translations_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AdviceTranslationsApiConstantsResponseMessage.advice_translations_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.AdviceTranslationsUpdate)]
        [HttpPut("update-advice-translation")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateAdviceTranslation(AdviceTranslationsUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _adviceTranslationsService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AdviceTranslationsApiConstantsResponseMessage.advice_translations_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AdviceTranslationsApiConstantsResponseMessage.advice_translations_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AdviceTranslationsApiConstantsResponseMessage.advice_translations_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.AdviceTranslationsDelete)]
        [HttpDelete("delete-advice-translation")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAdviceTranslation(int adviceTranslationId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _adviceTranslationsService.Delete(adviceTranslationId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AdviceTranslationsApiConstantsResponseMessage.advice_translations_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, AdviceTranslationsApiConstantsResponseMessage.advice_translations_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, AdviceTranslationsApiConstantsResponseMessage.advice_translations_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
