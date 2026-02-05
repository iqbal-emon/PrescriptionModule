using AuthenticationSystem.Application.Services;
using AuthenticationSystem.Dtos.RequestDto.CompanyBranchDto;
using AuthenticationSystem.Dtos.ResponseDto.CompanyBranchDto;
using AuthenticationSystem.Utility;
using Entities.EntityClass.CompanyEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using Utility.ApiResponse;

namespace AuthenticationSystem.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class CompanyBranchController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly CompanyBranchService _companyBranchService;

        public CompanyBranchController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            CompanyBranchService companyBranchService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _companyBranchService = companyBranchService;
        }

        [HttpGet("gets-all-company-branches")]
        public async Task<ActionResult<ApiResponse<List<CompanyBranchApiResponseDto>>>> GetAllCompanyBranches()
        {
            var apiResponse = new ApiResponse<List<CompanyBranchApiResponseDto>>();
            try
            {
                var branches = await _companyBranchService.GetAll();
                var mappedBranches = await _mapperService.MapList<CompanyBranch, CompanyBranchApiResponseDto>(branches.Result);
                if (branches.Result == null || branches.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.companyBranch_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedBranches;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.companyBranch_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.companyBranch_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("get-company-branch-by-id")]
        public async Task<ActionResult<ApiResponse<CompanyBranchApiResponseDto>>> GetCompanyBranchById(int branchId)
        {
            var apiResponse = new ApiResponse<CompanyBranchApiResponseDto>();
            try
            {
                var branch = await _companyBranchService.GetById(branchId);
                var mappedBranch = await _mapperService.MapSingle<CompanyBranch, CompanyBranchApiResponseDto>(branch.Result);
                if (branch.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.companyBranch_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedBranch;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.companyBranch_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.companyBranch_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("get-company-branches-by-company-id")]
        public async Task<ActionResult<ApiResponse<List<CompanyBranchApiResponseDto>>>> GetCompanyBranchesByCompanyId(int companyId)
        {
            var apiResponse = new ApiResponse<List<CompanyBranchApiResponseDto>>();
            try
            {
                var branches = await _companyBranchService.GetByCompanyId(companyId);
                var mappedBranches = await _mapperService.MapList<CompanyBranch, CompanyBranchApiResponseDto>(branches.Result);
                if (branches.Result == null || branches.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.companyBranch_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedBranches;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.companyBranch_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.companyBranch_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpPost("create-company-branch")]
        public async Task<ActionResult<ApiResponse<int>>> CreateCompanyBranch(CompanyBranchInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _companyBranchService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.companyBranch_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.companyBranch_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.companyBranch_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [HttpPut("update-company-branch")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateCompanyBranch(CompanyBranchUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _companyBranchService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.companyBranch_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.companyBranch_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.companyBranch_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [HttpDelete("delete-company-branch")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteCompanyBranch(int branchId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _companyBranchService.Delete(branchId);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.companyBranch_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, AuthApiConstantsResponseMessage.companyBranch_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, AuthApiConstantsResponseMessage.companyBranch_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }
    }
}

