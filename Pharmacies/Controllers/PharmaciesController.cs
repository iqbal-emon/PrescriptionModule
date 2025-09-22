
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pharmacies.Application.Services;
using Pharmacies.Dtos.ResponseDto.PharmaciesDto;
using Pharmacies.Utility;
using Pharmacies.Dtos.RequestDto.PharmaciesDto;

namespace Pharmacies.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PharmaciesController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly PharmacyService _pharmacyService;

        public PharmaciesController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            PharmacyService pharmacyService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _pharmacyService = pharmacyService;
        }

        [Authorize(Policy = PermissionConstants.PharmacyGetAll)]
        [HttpGet("gets-all-pharmacies")]
        public async Task<ActionResult<ApiResponse<List<PharmaciesApiResponseDto>>>> GetAllPharmacy()
        {
            var apiResponse = new ApiResponse<List<PharmaciesApiResponseDto>>();
            try
            {
                var pharmacies = await _pharmacyService.GetAll();
                var mappedPharmacies = await _mapperService.MapList<Entities.EntityClass.Pharmacy, PharmaciesApiResponseDto>(pharmacies.Result);

                if (pharmacies.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PharmaciesApiConstantsResponseMessage.pharmacies_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPharmacies;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PharmaciesApiConstantsResponseMessage.pharmacies_get_all_success);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PharmaciesApiConstantsResponseMessage.pharmacies_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PharmacyGetId)]
        [HttpGet("get-pharmacies-by-id")]
        public async Task<ActionResult<ApiResponse<PharmaciesApiResponseDto>>> GetPharmacyById(int pharmacyId)
        {
            var apiResponse = new ApiResponse<PharmaciesApiResponseDto>();
            try
            {
                var pharmacy = await _pharmacyService.GetById(pharmacyId);
                var mappedPharmacy = await _mapperService.MapSingle<Entities.EntityClass.Pharmacy, PharmaciesApiResponseDto>(pharmacy.Result);
                if (pharmacy.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PharmaciesApiConstantsResponseMessage.pharmacies_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPharmacy;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PharmaciesApiConstantsResponseMessage.pharmacies_get_all_success);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PharmaciesApiConstantsResponseMessage.pharmacies_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PharmacyCreate)]
        [HttpPost("create-pharmacies")]
        public async Task<ActionResult<ApiResponse<int>>> CreatePharmacy(PharmaciesInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _pharmacyService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PharmaciesApiConstantsResponseMessage.pharmacies_insert_success_message);
                        return Ok(apiResponse);
                    }
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PharmaciesApiConstantsResponseMessage.pharmacies_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PharmaciesApiConstantsResponseMessage.pharmacies_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PharmacyUpdate)]
        [HttpPut("update-pharmacies")]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePharmacy(PharmaciesUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _pharmacyService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PharmaciesApiConstantsResponseMessage.pharmacies_update_success_message);
                        return Ok(apiResponse);
                    }
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PharmaciesApiConstantsResponseMessage.pharmacies_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PharmaciesApiConstantsResponseMessage.pharmacies_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PharmacyDelete)]
        [HttpDelete("delete-pharmacies")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePharmacy(int pharmacyId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _pharmacyService.Delete(pharmacyId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PharmaciesApiConstantsResponseMessage.pharmacies_delete_success_message);
                    return Ok(apiResponse);
                }
                ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, PharmaciesApiConstantsResponseMessage.pharmacies_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}
