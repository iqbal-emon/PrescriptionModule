using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrescriptionPdf.Application.Services;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;
using PrescriptionPdf.Dtos.ResponseDto.PrescriptionPdfDto;
using PrescriptionPdf.Dtos.RequestDto.PrescriptionPdfDto;
using prescriptionPdf.Utility;
using System.Runtime.InteropServices;
using Utility.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
namespace PrescriptionPdfControllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PrescriptionPdfController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly PrescriptionPdfService _prescriptionPdfService;
        private readonly IConfiguration _configuration;

        public PrescriptionPdfController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            PrescriptionPdfService prescriptionPdfService,
            IConfiguration configuration)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _prescriptionPdfService = prescriptionPdfService;
            _configuration = configuration;
        }

        [Authorize(Policy = PermissionConstants.PrescriptionPdfsGetAll)]
        [HttpGet("get-pdf-prescriptions-by-patient-doctor-id")]
        public async Task<ActionResult<ApiResponse<List<PrescriptionPdfPatientResponseDto>>>> GetAllPrescriptionsByPatientDoctorIdPdf(int patientId,int doctorId)
        {
            var apiResponse = new ApiResponse<List<PrescriptionPdfPatientResponseDto>>();
            try
            {
                var prescriptions = await _prescriptionPdfService.GetByPatientDoctorId(patientId, doctorId);

                if (prescriptions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = prescriptions.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionPdfsGetAll)]
        [HttpGet("get-pdf-prescriptions-by-doctor-prehand-id")]
        public async Task<ActionResult<ApiResponse<List<PrescriptionPdfPatientResponseDto>>>> GetAllPrescriptionsByDoctorIdPrehandPdf(int doctorId)
        {
            var apiResponse = new ApiResponse<List<PrescriptionPdfPatientResponseDto>>();
            try
            {
                var prescriptions = await _prescriptionPdfService.GetPrehandByDoctorId(doctorId);

                if (prescriptions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = prescriptions.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_see_try_catch);
            }
            return Ok(apiResponse);
        }




        [Authorize(Policy = PermissionConstants.PrescriptionPdfsGetById)]
        [HttpGet("get-prescription-pdf-by-appointment-id")]
        public async Task<ActionResult<ApiResponse<PrescriptionPdfApiResponseDto>>> GetPrescriptionPdfByAppointmentId(int appointmentId)
        {
            var apiResponse = new ApiResponse<PrescriptionPdfApiResponseDto>();
            try
            {
                var appointments = await _prescriptionPdfService.GetById(appointmentId);
                var mappedAppointments = await _mapperService.MapSingle<Entities.EntityClass.PrescriptionEntity.PrescriptionPdf, PrescriptionPdfApiResponseDto>(appointments.Result);

                if (appointments.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedAppointments;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionPdfsCreate)]
        [HttpPost("create-prescription-pdf")]
        public async Task<ActionResult<ApiResponse<int>>> CreatePrescriptionPdf(PrescriptionPdfInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionPdfService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_inserted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionPdfsUpdate)]
        [HttpPut("update-prescription-pdf")]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePrescriptionPdf(PrescriptionPdfUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionPdfService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionPdfsDelete)]
        [HttpDelete("delete-prescription-pdf")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePrescriptionPdf(int prescriptionId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _prescriptionPdfService.Delete(prescriptionId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_delete_success_message);
                    return Ok(apiResponse);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionPdfConstantsResponseMessage.prescriptionPdf_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionPdfsCreate)]
        [HttpPost("prescription-upload")]
        public async Task<ActionResult<ApiResponse<PrescriptionUploadResponseDto>>> UploadPrescriptionImage([FromForm] FileUploadRequestViewModel model)
        {
            var apiResponse = new ApiResponse<PrescriptionUploadResponseDto>();
            try
            {
                var prescriptionUploadResponse = await PrescriptionSaveInTempStorage(model);

                if (!prescriptionUploadResponse.IsSuccess)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Upload Failed");
                    return apiResponse;
                }
                apiResponse.Results = new PrescriptionUploadResponseDto
                {
                    uploadedPath = prescriptionUploadResponse.Result,
                    
                };
                apiResponse.StatusCode = 200;
                apiResponse.Message = "Image Uploaded Successfully";
                apiResponse.IsSuccess = true;
                apiResponse.Status = "success";
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, ApiResponseMessage.common_see_try_catch);
            }
            return Ok(apiResponse);
        }
        private async Task<Response<string>> PrescriptionSaveInTempStorage(FileUploadRequestViewModel model)
        {
            var response = new Response<string>();
            string saveFilePath = "";
            if (model.File != null && model.File.Length > 0)
            {
                var fileName = model.File.FileName;
                var uniqueFileName = $"{Guid.NewGuid()}{fileName}";

                var basePath = _configuration.GetSection("AppSettings").GetSection("PDFCREATEDPATH").Value;

                var contentRootPath = $"{basePath}/TempPrescription";

                if (!Directory.Exists(contentRootPath))
                {
                    Directory.CreateDirectory(contentRootPath);
                }
                var filePath = Path.Combine(contentRootPath, uniqueFileName);
                saveFilePath = $"TempPrescription/{uniqueFileName}";

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }
                response.Result = saveFilePath;
                response.IsSuccess = true;
            }
           
            return response;
        }
    }
}
