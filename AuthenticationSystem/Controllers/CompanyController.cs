using AuthenticationSystem.Application.Services;
using AuthenticationSystem.Dtos.RequestDto.CompanyDto;
using AuthenticationSystem.Dtos.ResponseDto.CompanyDto;
using AuthenticationSystem.Utility;
using Entities.EntityClass.CompanyEntity;
using Microsoft.AspNetCore.Authorization;
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
    public class CompanyController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly CompanyService _companyService;

        public CompanyController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            CompanyService companyService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _companyService = companyService;
        }

        [HttpGet("gets-all-companies")]
        public async Task<ActionResult<ApiResponse<List<CompanyApiResponseDto>>>> GetAllCompanies()
        {
            var apiResponse = new ApiResponse<List<CompanyApiResponseDto>>();
            try
            {
                var companies = await _companyService.GetAll();
                var mappedCompanies = await _mapperService.MapList<Company, CompanyApiResponseDto>(companies.Result);
                if (companies.Result == null || companies.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.company_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedCompanies;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.company_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.company_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpGet("get-company-by-id")]
        public async Task<ActionResult<ApiResponse<CompanyApiResponseDto>>> GetCompanyById(int companyId)
        {
            var apiResponse = new ApiResponse<CompanyApiResponseDto>();
            try
            {
                var company = await _companyService.GetById(companyId);
                var mappedCompany = await _mapperService.MapSingle<Company, CompanyApiResponseDto>(company.Result);
                if (company.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.company_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedCompany;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, AuthApiConstantsResponseMessage.company_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, AuthApiConstantsResponseMessage.company_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [HttpPost("create-company")]
        public async Task<ActionResult<ApiResponse<int>>> CreateCompany(CompanyInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _companyService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.company_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.company_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.company_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [HttpPut("update-company")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateCompany(CompanyUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _companyService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.company_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.company_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, AuthApiConstantsResponseMessage.company_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }

        [HttpDelete("delete-company")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteCompany(int companyId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _companyService.Delete(companyId);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, AuthApiConstantsResponseMessage.company_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, AuthApiConstantsResponseMessage.company_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, AuthApiConstantsResponseMessage.company_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }
    }
}

