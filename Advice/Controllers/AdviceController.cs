using Advice.Application.Services;
using Advice.Dtos.RequestDto.AdviceDto;
using Advice.Dtos.ResponseDto.AdviceDto;
using Advice.Utility;
using Entities.EntityClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Permission;

namespace Advice.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class AdviceController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly AdviceService _adviceService;

        public AdviceController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            AdviceService adviceService) 
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _adviceService = adviceService; 
        }

        // GET all Advice
        [Authorize(Policy = PermissionConstants.AdviceGetAll)]
        [HttpGet("gets-all-advice")]
        public async Task<ActionResult<ApiResponse<List<AdviceApiResponseDto>>>> GetAllAdvice()
        {
            var apiResponse = new ApiResponse<List<AdviceApiResponseDto>>();
            try
            {
                var advice = await _adviceService.GetAll();
                var mappedAdvice = await _mapperService.MapList<Entities.EntityClass.CommonAdvice, AdviceApiResponseDto>(advice.Result);

                if (advice.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AdviceApiConstantsResponseMessage.advice_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedAdvice;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AdviceApiConstantsResponseMessage.advice_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AdviceApiConstantsResponseMessage.advice_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.AdviceGetAll)]
        [HttpGet("gets-advice-by-name")]
        public async Task<ActionResult<ApiResponse<List<AdviceApiResponseDto>>>> GetAdviceByName(string? adviceName = null)
        {
            var apiResponse = new ApiResponse<List<AdviceApiResponseDto>>();
            try
            {
                // Assuming you have a service method that fetches advice based on name
                var adviceList = await _adviceService.GetAllAdviceByName(adviceName);

                // Mapping the result from entities to response DTO
                var mappedAdvice = await _mapperService.MapList<Entities.EntityClass.CommonAdvice, AdviceApiResponseDto>(adviceList.Result);

                // Check if no advice is found
                if (adviceList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AdviceApiConstantsResponseMessage.advice_null_of_get_list);
                    return Ok(apiResponse);
                }

                // Set the response data
                apiResponse.Results = mappedAdvice;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AdviceApiConstantsResponseMessage.advice_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AdviceApiConstantsResponseMessage.advice_see_try_catch);
            }
            return Ok(apiResponse);
        }



        [Authorize(Policy = PermissionConstants.AdviceGetAll)]
        [HttpGet("gets-bookmarks-advice")]
        public async Task<ActionResult<ApiResponse<List<AdviceApiResponseDto>>>> GetHighlyUsedAdvice(int doctorId)
        {
            var apiResponse = new ApiResponse<List<AdviceApiResponseDto>>();
            try
            {
                // Assuming you have a method for getting all the advice or highly used advice in the service
                var advice = await _adviceService.GetBookMarks(doctorId);

                // Map the list of advice to the response DTO
                var mappedAdvice = await _mapperService.MapList<Entities.EntityClass.CommonAdvice, AdviceApiResponseDto>(advice.Result);

                // Check if no advice is found
                if (advice.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AdviceApiConstantsResponseMessage.advice_null_of_get_list);
                    return Ok(apiResponse);
                }

                // Set the response data
                apiResponse.Results = mappedAdvice;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AdviceApiConstantsResponseMessage.advice_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                // In case of an exception
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AdviceApiConstantsResponseMessage.advice_see_try_catch);
            }
            return Ok(apiResponse);
        }

        // GET Advice by ID
        [Authorize(Policy = PermissionConstants.AdviceGetId)]
        [HttpGet("get-advice-by-id")]
        public async Task<ActionResult<ApiResponse<AdviceApiResponseDto>>> GetAdviceById(int adviceId)
        {
            var apiResponse = new ApiResponse<AdviceApiResponseDto>();
            try
            {
                var advice = await _adviceService.GetById(adviceId);
                var mappedAdvice = await _mapperService.MapSingle<Entities.EntityClass.CommonAdvice, AdviceApiResponseDto>(advice.Result);

                if (advice.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AdviceApiConstantsResponseMessage.advice_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedAdvice;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AdviceApiConstantsResponseMessage.advice_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AdviceApiConstantsResponseMessage.advice_see_try_catch);
            }
            return Ok(apiResponse);
        }

        // POST create Advice
        [Authorize(Policy = PermissionConstants.AdviceCreate)]
        [HttpPost("create-advice")]
        public async Task<ActionResult<ApiResponse<int>>> CreateAdvice(AdviceInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _adviceService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AdviceApiConstantsResponseMessage.advice_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AdviceApiConstantsResponseMessage.advice_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AdviceApiConstantsResponseMessage.advice_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        // PUT update Advice
        [Authorize(Policy = PermissionConstants.AdviceUpdate)]
        [HttpPut("update-advice")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateAdvice(AdviceUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _adviceService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AdviceApiConstantsResponseMessage.advice_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AdviceApiConstantsResponseMessage.advice_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AdviceApiConstantsResponseMessage.advice_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        // DELETE Advice
        [Authorize(Policy = PermissionConstants.AdviceDelete)]
        [HttpDelete("delete-advice")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAdvice(int adviceId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _adviceService.Delete(adviceId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AdviceApiConstantsResponseMessage.advice_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, AdviceApiConstantsResponseMessage.advice_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, AdviceApiConstantsResponseMessage.advice_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
