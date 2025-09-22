using MedicationManufacture.Application.Services;
using MedicationManufacturer.Dtos.ReponseDto.MedicationManufacturerDto;
using MedicationManufacturer.Dtos.RequestDto;
using MedicationManufacturer.Utility;
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

namespace MedicationManufacturer.Controllers
{
  
        [ApiController]
        [Route("api/2025-02/")]
        public class MedicationManufacturerController : ControllerBase
        {
            private readonly SharedCommonService _sharedCommonService;
            private readonly MapperService _mapperService;
            private readonly MedicationManufacturerService _medicationManufacturerService;

            public MedicationManufacturerController(
                SharedCommonService sharedCommonService,
                MapperService mapperService,
                MedicationManufacturerService medicationManufacturerService)
            {
                _sharedCommonService = sharedCommonService;
                _mapperService = mapperService;
                _medicationManufacturerService = medicationManufacturerService;
            }

            [Authorize(Policy = PermissionConstants.MedicationManufacturerGetAll)]
            [HttpGet("gets-all-medication-manufacturers")]
            public async Task<ActionResult<ApiResponse<List<MedicationManufacturerApiReponseDto>>>> GetAllMedicationManufacturers()
            {
                var apiResponse = new ApiResponse<List<MedicationManufacturerApiReponseDto>>();
                try
                {
                    var manufacturers = await _medicationManufacturerService.GetAll();

                    var mappedManufacturers = await _mapperService.MapList<Entities.EntityClass.MedicineEntity.MedicationManufacturer, MedicationManufacturerApiReponseDto>(manufacturers.Result);

                    if (manufacturers.Result.Count == 0)
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_null_of_get_list);
                        return Ok(apiResponse);
                    }

                    apiResponse.Results = mappedManufacturers;
                    ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
                }
                catch (Exception ex)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_see_try_catch);
                }
                return Ok(apiResponse);
            }

            [Authorize(Policy = PermissionConstants.MedicationManufacturerGetId)]
            [HttpGet("get-medication-manufacturer-by-id")]
            public async Task<ActionResult<ApiResponse<MedicationManufacturerApiReponseDto>>> GetMedicationManufacturerById(int manufacturerId)
            {
                var apiResponse = new ApiResponse<MedicationManufacturerApiReponseDto>();
                try
                {
                    var manufacturer = await _medicationManufacturerService.GetById(manufacturerId);
                    var mappedManufacturer = await _mapperService.MapSingle<Entities.EntityClass.MedicineEntity.MedicationManufacturer, MedicationManufacturerApiReponseDto>(manufacturer.Result);
                    if (manufacturer.Result == null)
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_null_of_get_list);
                        return Ok(apiResponse);
                    }

                    apiResponse.Results = mappedManufacturer;
                    ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
                }
                catch (Exception ex)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_see_try_catch);
                }
                return Ok(apiResponse);
            }

            [Authorize(Policy = PermissionConstants.MedicationManufacturerCreate)]
            [HttpPost("create-medication-manufacturer")]
            public async Task<ActionResult<ApiResponse<int>>> CreateMedicationManufacturer(MedicationManufacturerInsertRequestDto request)
            {
                var apiResponse = new ApiResponse<int>();

                try
                {
                    if (ModelState.IsValid)
                    {
                        var response = await _medicationManufacturerService.Insert(request);

                        if (response.IsSuccess)
                        {
                            ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_insert_success_message);
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
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_inserted_failed_message);
                    }
                }
                catch (Exception ex)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_see_try_catch);
                    return Ok(apiResponse);
                }

                return Ok(apiResponse);
            }

            [Authorize(Policy = PermissionConstants.MedicationManufacturerUpdate)]
            [HttpPut("update-medication-manufacturer")]
            public async Task<ActionResult<ApiResponse<int>>> UpdateMedicationManufacturer(MedicationManufacturerUpdateRequestDto request)
            {
                var apiResponse = new ApiResponse<int>();

                try
                {
                    if (ModelState.IsValid)
                    {
                        var response = await _medicationManufacturerService.Update(request);

                        if (response.IsSuccess)
                        {
                            ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_update_success_message);
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
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_update_failed_message);
                    }
                }
                catch (Exception ex)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_see_try_catch);
                    return Ok(apiResponse);
                }

                return Ok(apiResponse);
            }

            [Authorize(Policy = PermissionConstants.MedicationManufacturerDelete)]
            [HttpDelete("delete-medication-manufacturer")]
            public async Task<ActionResult<ApiResponse<bool>>> DeleteMedicationManufacturer(int manufacturerId)
            {
                var apiResponse = new ApiResponse<bool>();

                try
                {
                    if (ModelState.IsValid)
                    {
                        var response = await _medicationManufacturerService.Delete(manufacturerId);

                        if (response.IsSuccess)
                        {
                            ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_delete_success_message);
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
                        ApiResponseHelper.SetFailedResponse(apiResponse, false, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_deleted_failed_message);
                    }
                }
                catch (Exception ex)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, MedicationManufacturerConstantsResponseMessage.medication_manufacturer_see_try_catch);
                    return Ok(apiResponse);
                }

                return Ok(apiResponse);
            }
        }
    }

