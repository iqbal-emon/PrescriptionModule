using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorChamber.Utility;
using ExpertiseCategory.Application.Services;
using System.Runtime.InteropServices;
using ExpertiseCategory.Dtos.RequestDto.ExpertiseCategoryDto;

namespace ExpertiseCategory.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class ExpertiseCategoryController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly ExpertiseCategoryService _expertiseCategoryService;

        public ExpertiseCategoryController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            ExpertiseCategoryService expertiseCategoryService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _expertiseCategoryService = expertiseCategoryService;
        }

        [Authorize(Policy = PermissionConstants.ExpertiseCategoryGetAll)]
        [HttpGet("gets-all-expertise-categories")]
        public async Task<ActionResult<ApiResponse<List<ExpertiseCategoryApiResponseDto>>>> GetAllExpertiseCategories()
        {
            var apiResponse = new ApiResponse<List<ExpertiseCategoryApiResponseDto>>();
            try
            {
                var categoryList = await _expertiseCategoryService.GetAll();
                var mappedCategories = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.ExpertiseCategory, ExpertiseCategoryApiResponseDto>(categoryList.Result);

                if (categoryList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, ExpertiseCategoryApiConstantsResponseMessage.category_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedCategories;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, ExpertiseCategoryApiConstantsResponseMessage.category_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, ExpertiseCategoryApiConstantsResponseMessage.category_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.ExpertiseCategoryGetId)]
        [HttpGet("get-expertise-category-by-id")]
        public async Task<ActionResult<ApiResponse<ExpertiseCategoryApiResponseDto>>> GetExpertiseCategoryById(int categoryId)
        {
            var apiResponse = new ApiResponse<ExpertiseCategoryApiResponseDto>();
            try
            {
                var category = await _expertiseCategoryService.GetById(categoryId);
                var mappedCategory = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.ExpertiseCategory, ExpertiseCategoryApiResponseDto>(category.Result);

                if (category.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, ExpertiseCategoryApiConstantsResponseMessage.category_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedCategory;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, ExpertiseCategoryApiConstantsResponseMessage.category_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, ExpertiseCategoryApiConstantsResponseMessage.category_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.ExpertiseCategoryCreate)]
        [HttpPost("create-expertise-category")]
        public async Task<ActionResult<ApiResponse<int>>> CreateExpertiseCategory(ExpertiseCategoryInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _expertiseCategoryService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, ExpertiseCategoryApiConstantsResponseMessage.category_insert_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, ExpertiseCategoryApiConstantsResponseMessage.category_inserted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, ExpertiseCategoryApiConstantsResponseMessage.category_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.ExpertiseCategoryUpdate)]
        [HttpPut("update-expertise-category")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateExpertiseCategory(ExpertiseCategoryUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _expertiseCategoryService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, ExpertiseCategoryApiConstantsResponseMessage.category_update_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, ExpertiseCategoryApiConstantsResponseMessage.category_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, ExpertiseCategoryApiConstantsResponseMessage.category_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.ExpertiseCategoryDelete)]
        [HttpDelete("delete-expertise-category-by-id")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteExpertiseCategory(int categoryId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _expertiseCategoryService.Delete(categoryId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, ExpertiseCategoryApiConstantsResponseMessage.category_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, ExpertiseCategoryApiConstantsResponseMessage.category_deleted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, ExpertiseCategoryApiConstantsResponseMessage.category_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}