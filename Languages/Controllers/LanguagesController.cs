
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using SharedService.Model;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;
using Languages.Dtos.RequestDto.LanguagesDto;
using Languages.Dtos.ResponseDto.LanguagesDto;
using Languages.Application.Services;
using Languages.Utility;

namespace LanguageService.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class LanguagesController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly LanguagesService _languageService;

        public LanguagesController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            LanguagesService languagesService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _languageService = languagesService;
        }

        [Authorize(Policy = PermissionConstants.LanguagesGetAll)]
        [HttpGet("gets-all-languages")]
        public async Task<ActionResult<ApiResponse<List<LanguagesApiResponseDto>>>> GetAllLanguages()
        {
            var apiResponse = new ApiResponse<List<LanguagesApiResponseDto>>();
            try
            {
                var languages = await _languageService.GetAll();

                var mappedLanguages = await _mapperService.MapList<Entities.EntityClass.Language, LanguagesApiResponseDto>(languages.Result);

                if (languages.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, LanguagesApiConstantsResponseMessage.languages_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedLanguages;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, LanguagesApiConstantsResponseMessage.languages_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, LanguagesApiConstantsResponseMessage.languages_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.LanguagesGetId)]
        [HttpGet("get-language-by-id")]
        public async Task<ActionResult<ApiResponse<LanguagesApiResponseDto>>> GetLanguageById(int languageId)
        {
            var apiResponse = new ApiResponse<LanguagesApiResponseDto>();
            try
            {
                var language = await _languageService.GetById(languageId);
                var mappedLanguage = await _mapperService.MapSingle<Entities.EntityClass.Language, LanguagesApiResponseDto>(language.Result);

                if (language.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, LanguagesApiConstantsResponseMessage.languages_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedLanguage;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, LanguagesApiConstantsResponseMessage.languages_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, LanguagesApiConstantsResponseMessage.languages_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.LanguagesCreate)]
        [HttpPost("create-language")]
        public async Task<ActionResult<ApiResponse<int>>> CreateLanguage(LanguagesInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _languageService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, LanguagesApiConstantsResponseMessage.languages_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, LanguagesApiConstantsResponseMessage.languages_insert_success_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, LanguagesApiConstantsResponseMessage.languages_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.LanguagesUpdate)]
        [HttpPut("update-language")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateLanguage(LanguagesUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _languageService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, LanguagesApiConstantsResponseMessage.languages_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, LanguagesApiConstantsResponseMessage.languages_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, LanguagesApiConstantsResponseMessage.languages_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.LanguagesDelete)]
        [HttpDelete("delete-language")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteLanguage(int languageId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _languageService.Delete(languageId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, LanguagesApiConstantsResponseMessage.languages_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, LanguagesApiConstantsResponseMessage.languages_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, LanguagesApiConstantsResponseMessage.languages_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
