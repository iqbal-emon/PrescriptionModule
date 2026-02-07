using Examinations.Application.Services;
using Examinations.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using System.Data;
using System.Security;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;
using Examinations.Dtos.RequestDto.ExaminationsDto;
using Examinations.Dtos.ResponseDto.ExaminationsDto;
using System.Runtime.InteropServices;

namespace Examinations.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class ExaminationsController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly ExaminationsService _examinationsService;

        public ExaminationsController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            ExaminationsService examinationsService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _examinationsService = examinationsService;
        }

        [Authorize(Policy = PermissionConstants.ExaminationsGetAll)]
        [HttpGet("gets-all-examinations")]
        public async Task<ActionResult<ApiResponse<List<ExaminationsApiResponseDto>>>> GetAllExamination()
        {
            var apiResponse = new ApiResponse<List<ExaminationsApiResponseDto>>();
            try
            {
                var examinations = await _examinationsService.GetAll();

                var mappedExaminations = await _mapperService.MapList<Entities.EntityClass.Examination, ExaminationsApiResponseDto>(examinations.Result);

                if (examinations.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, ExaminationsApiConstantsResponseMessage.examinations_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedExaminations;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, ExaminationsApiConstantsResponseMessage.examinations_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, ExaminationsApiConstantsResponseMessage.examinations_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.ExaminationsGetId)]
        [HttpGet("get-examinations-by-id")]
        public async Task<ActionResult<ApiResponse<ExaminationsApiResponseDto>>> GetExaminationById(int examinationId)
        {
            var apiResponse = new ApiResponse<ExaminationsApiResponseDto>();
            try
            {
                var examination = await _examinationsService.GetById(examinationId);
                var mappedExamination = await _mapperService.MapSingle<Entities.EntityClass.Examination, ExaminationsApiResponseDto>(examination.Result);

                if (examination.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, ExaminationsApiConstantsResponseMessage.examinations_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedExamination;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, ExaminationsApiConstantsResponseMessage.examinations_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, ExaminationsApiConstantsResponseMessage.examinations_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.ExaminationsCreate)]
        [HttpPost("create-examinations")]
        public async Task<ActionResult<ApiResponse<int>>> CreateExamination(ExaminationsInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _examinationsService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, ExaminationsApiConstantsResponseMessage.examinations_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, ExaminationsApiConstantsResponseMessage.examinations_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, ExaminationsApiConstantsResponseMessage.examinations_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.ExaminationsUpdate)]
        [HttpPut("update-examinations")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateExamination(ExaminationsUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _examinationsService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, ExaminationsApiConstantsResponseMessage.examinations_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, ExaminationsApiConstantsResponseMessage.examinations_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, ExaminationsApiConstantsResponseMessage.examinations_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.ExaminationsDelete)]
        [HttpDelete("delete-examination")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteExamination(int examinationId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _examinationsService.Delete(examinationId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, ExaminationsApiConstantsResponseMessage.examinations_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, ExaminationsApiConstantsResponseMessage.examinations_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, ExaminationsApiConstantsResponseMessage.examinations_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
