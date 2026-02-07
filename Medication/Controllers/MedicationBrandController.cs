using Medication.Application.Services;
using Medication.Dtos.RequestDto.MedicationBrandDto;
using Medication.Dtos.ResponseDto.MedicationBrand;
using Medication.Utiltiy;
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
        public class MedicationBrandController : ControllerBase
        {
            private readonly SharedCommonService _sharedCommonService;
            private readonly MapperService _mapperService;
            private readonly MedicationBrandService _medicationBrandService;
            public MedicationBrandController(
                SharedCommonService sharedCommonService,
                MapperService mapperService,
                MedicationBrandService medicationBrandService)
            {
                _sharedCommonService = sharedCommonService;
                _mapperService = mapperService;
                _medicationBrandService = medicationBrandService;
            }

            [Authorize(Policy = PermissionConstants.MedicationBrandGetAll)]
            [HttpGet("gets-all-medication-brands")]
            public async Task<ActionResult<ApiResponse<List<MedicationBrandApiResponseDto>>>> GetAllMedicationBrands()
            {
                var apiResponse = new ApiResponse<List<MedicationBrandApiResponseDto>>();
                try
                {
                    var brands = await _medicationBrandService.GetAll();

                    var mappedBrands = await _mapperService.MapList<Entities.EntityClass.MedicineEntity.MedicationBrand, MedicationBrandApiResponseDto>(brands.Result);

                    if (brands.Result.Count == 0)
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationBrandApiConstantsResponseMessage.medication_brand_null_of_get_list);
                        return Ok(apiResponse);
                    }

                    apiResponse.Results = mappedBrands;
                    ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, MedicationBrandApiConstantsResponseMessage.medication_brand_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
                }
                catch (Exception ex)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationBrandApiConstantsResponseMessage.medication_brand_see_try_catch);
                }
                return Ok(apiResponse);
            }

            [Authorize(Policy = PermissionConstants.MedicationBrandGetId)]
            [HttpGet("get-medication-brand-by-id")]
            public async Task<ActionResult<ApiResponse<MedicationBrandApiResponseDto>>> GetMedicationBrandById(int brandId)
            {
                var apiResponse = new ApiResponse<MedicationBrandApiResponseDto>();
                try
                {
                    var brand = await _medicationBrandService.GetById(brandId);
                    var mappedBrand = await _mapperService.MapSingle<Entities.EntityClass.MedicineEntity.MedicationBrand, MedicationBrandApiResponseDto>(brand.Result);
                    if (brand.Result == null)
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationBrandApiConstantsResponseMessage.medication_brand_null_of_get_list);
                        return Ok(apiResponse);
                    }

                    apiResponse.Results = mappedBrand;
                    ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, MedicationBrandApiConstantsResponseMessage.medication_brand_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
                }
                catch (Exception ex)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationBrandApiConstantsResponseMessage.medication_brand_see_try_catch);
                }
                return Ok(apiResponse);
            }

            [Authorize(Policy = PermissionConstants.MedicationBrandCreate)]
            [HttpPost("create-medication-brand")]
            public async Task<ActionResult<ApiResponse<int>>> CreateMedicationBrand(MedicationBrandInsertRequestDto request)
            {
                var apiResponse = new ApiResponse<int>();

                try
                {
                    if (ModelState.IsValid)
                    {
                        var response = await _medicationBrandService.Insert(request);

                        if (response.IsSuccess)
                        {
                            ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, MedicationBrandApiConstantsResponseMessage.medication_brand_insert_success_message);
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
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, MedicationBrandApiConstantsResponseMessage.medication_brand_inserted_failed_message);
                    }
                }
                catch (Exception ex)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, MedicationBrandApiConstantsResponseMessage.medication_brand_see_try_catch);
                    return Ok(apiResponse);
                }

                return Ok(apiResponse);
            }

            [Authorize(Policy = PermissionConstants.MedicationBrandUpdate)]
            [HttpPut("update-medication-brand")]
            public async Task<ActionResult<ApiResponse<int>>> UpdateMedicationBrand(MedicationBrandUpdateRequestDto request)
            {
                var apiResponse = new ApiResponse<int>();

                try
                {
                    if (ModelState.IsValid)
                    {
                        var response = await _medicationBrandService.Update(request);

                        if (response.IsSuccess)
                        {
                            ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, MedicationBrandApiConstantsResponseMessage.medication_brand_update_success_message);
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
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, MedicationBrandApiConstantsResponseMessage.medication_brand_update_failed_message);
                    }
                }
                catch (Exception ex)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, MedicationBrandApiConstantsResponseMessage.medication_brand_see_try_catch);
                    return Ok(apiResponse);
                }

                return Ok(apiResponse);
            }

            [Authorize(Policy = PermissionConstants.MedicationBrandDelete)]
            [HttpDelete("delete-medication-brand")]
            public async Task<ActionResult<ApiResponse<bool>>> DeleteMedicationBrand(int brandId)
            {
                var apiResponse = new ApiResponse<bool>();

                try
                {
                    if (ModelState.IsValid)
                    {
                        var response = await _medicationBrandService.Delete(brandId);

                        if (response.IsSuccess)
                        {
                            ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, MedicationBrandApiConstantsResponseMessage.medication_brand_delete_success_message);
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
                        ApiResponseHelper.SetFailedResponse(apiResponse, false, MedicationBrandApiConstantsResponseMessage.medication_brand_deleted_failed_message);
                    }
                }
                catch (Exception ex)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, MedicationBrandApiConstantsResponseMessage.medication_brand_see_try_catch);
                    return Ok(apiResponse);
                }

                return Ok(apiResponse);
            }
        }
    }

