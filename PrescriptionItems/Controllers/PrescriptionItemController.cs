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
using PrescriptionItems.Dtos.RequestDto.PrescriptionItem;
using PrescriptionItems.Dtos.ResponseDto.PrescriptionItemDto;
using PrescriptionItems.Utility;
using System.Runtime.InteropServices;

namespace DoctorPrescription.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PrescriptionItemController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly PrescriptionItemService _prescriptionItemService;
        public PrescriptionItemController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            PrescriptionItemService prescriptionItemService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _prescriptionItemService = prescriptionItemService;
        }
        [Authorize(Policy = PermissionConstants.PrescriptionItemGetAll)]
        [HttpGet("gets-all-prescription-items")]
        public async Task<ActionResult<ApiResponse<List<PrescriptionItemApiResponseDto>>>> GetAllPrescriptionItems()
        {
            var apiResponse = new ApiResponse<List<PrescriptionItemApiResponseDto>>();
            try
            {
                var prescriptionItems = await _prescriptionItemService.GetAll();

                var mappedPrescriptionItems = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.PrescriptionItem, PrescriptionItemApiResponseDto>(prescriptionItems.Result);

                if (prescriptionItems.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionItemApiConstantsResponseMessage.prescription_item_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescriptionItems;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionItemApiConstantsResponseMessage.prescription_item_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionItemApiConstantsResponseMessage.prescription_item_see_try_catch);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.PrescriptionItemGetId)]
        [HttpGet("get-prescription-item-by-id")]
        public async Task<ActionResult<ApiResponse<PrescriptionItemApiResponseDto>>> GetPrescriptionItemById(int prescriptionItemId)
        {
            var apiResponse = new ApiResponse<PrescriptionItemApiResponseDto>();
            try
            {
                var prescriptionItem = await _prescriptionItemService.GetById(prescriptionItemId);
                var mappedPrescriptionItem = await _mapperService.MapSingle<Entities.EntityClass.PrescriptionEntity.PrescriptionItem, PrescriptionItemApiResponseDto>(prescriptionItem.Result);
                if (prescriptionItem.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionItemApiConstantsResponseMessage.prescription_item_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescriptionItem;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionItemApiConstantsResponseMessage.prescription_item_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionItemApiConstantsResponseMessage.prescription_item_see_try_catch);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.PrescriptionItemCreate)]
        [HttpPost("create-prescription-item")]
        public async Task<ActionResult<ApiResponse<int>>> CreatePrescriptionItem(PrescriptionItemInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionItemService.Insert(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionItemApiConstantsResponseMessage.prescription_item_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionItemApiConstantsResponseMessage.prescription_item_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionItemApiConstantsResponseMessage.prescription_item_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.PrescriptionItemUpdate)]
        [HttpPut("update-prescription-item")]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePrescriptionItem(PrescriptionItemUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionItemService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionItemApiConstantsResponseMessage.prescription_item_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionItemApiConstantsResponseMessage.prescription_item_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionItemApiConstantsResponseMessage.prescription_item_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.PrescriptionItemDelete)]
        [HttpDelete("delete-prescription-item")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePrescriptionItem(int prescriptionItemId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionItemService.Delete(prescriptionItemId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionItemApiConstantsResponseMessage.prescription_item_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionItemApiConstantsResponseMessage.prescription_item_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionItemApiConstantsResponseMessage.prescription_item_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
