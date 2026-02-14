using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;
using System;
using System.IO;
using System.Threading.Tasks;
using Doctor.Application.Services;
using Doctor.Utility;
using Doctor.Dtos.ResponseDto.DigitalSignatureDto;
using Doctor.Dtos.RequestDto.DigitalSignatureDto;
using Microsoft.Extensions.Configuration;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/digital-signature")]
    public class DigitalSignatureController : ControllerBase
    {
        private readonly DigitalSignatureService _signatureService;
        private readonly MapperService _mapperService;
        private readonly IConfiguration _configuration;

        public DigitalSignatureController(
            DigitalSignatureService signatureService,
            MapperService mapperService,
            IConfiguration configuration)
        {
            _signatureService = signatureService;
            _mapperService = mapperService;
            _configuration = configuration;
        }

        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<DigitalSignatureApiResponseDto>>> GetSignatureById(int id)
        {
            var apiResponse = new ApiResponse<DigitalSignatureApiResponseDto>();
            try
            {
                var signature = await _signatureService.GetById(id);
                if (signature.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DigitalSignatureApiConstantsResponseMessage.signature_null_of_get_list);
                    return Ok(apiResponse);
                }

                var mappedSignature = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DigitalSignature, DigitalSignatureApiResponseDto>(signature.Result);
                apiResponse.Results = mappedSignature;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DigitalSignatureApiConstantsResponseMessage.signature_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DigitalSignatureApiConstantsResponseMessage.signature_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        [HttpGet("by-doctor-id/{doctorId}")]
        public async Task<ActionResult<ApiResponse<DigitalSignatureApiResponseDto>>> GetSignatureByDoctorId(int doctorId)
        {
            var apiResponse = new ApiResponse<DigitalSignatureApiResponseDto>();
            try
            {
                var signature = await _signatureService.GetByDoctorId(doctorId);
                if (signature.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DigitalSignatureApiConstantsResponseMessage.signature_null_of_get_list);
                    return Ok(apiResponse);
                }

                var mappedSignature = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DigitalSignature, DigitalSignatureApiResponseDto>(signature.Result);
                apiResponse.Results = mappedSignature;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DigitalSignatureApiConstantsResponseMessage.signature_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DigitalSignatureApiConstantsResponseMessage.signature_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ApiResponse<int>>> UploadSignature([FromForm] DigitalSignatureUploadRequestDto model)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (model.File == null || model.File.Length == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, "No file provided.");
                    return Ok(apiResponse);
                }

                // Validate file type (only images)
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".svg" };
                var fileExtension = Path.GetExtension(model.File.FileName).ToLowerInvariant();
                if (!Array.Exists(allowedExtensions, ext => ext == fileExtension))
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, "Invalid file type. Only image files are allowed.");
                    return Ok(apiResponse);
                }

                // Validate file size (max 5MB)
                const long maxFileSize = 5 * 1024 * 1024; // 5MB
                if (model.File.Length > maxFileSize)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, "File size exceeds the maximum limit of 5MB.");
                    return Ok(apiResponse);
                }

                // Get upload directory from configuration
                var uploadBasePath = _configuration.GetSection("GeneralSettings:FileUploadPath").Value ?? "wwwroot/uploads";
                var signatureDirectory = Path.Combine(uploadBasePath, "DigitalSignatures", model.DoctorID.ToString());
                
                // Create directory if it doesn't exist
                if (!Directory.Exists(signatureDirectory))
                {
                    Directory.CreateDirectory(signatureDirectory);
                }

                // Generate unique filename
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(signatureDirectory, fileName);
                var relativePath = Path.Combine("DigitalSignatures", model.DoctorID.ToString(), fileName).Replace("\\", "/");

                // Save file to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                // Create DTO for database insertion
                var insertDto = new DigitalSignatureInsertRequestDto
                {
                    DoctorID = model.DoctorID,
                    FileName = fileName,
                    OriginalFileName = model.File.FileName,
                    FilePath = relativePath,
                    FileSize = model.File.Length,
                    MimeType = model.File.ContentType,
                    TenantID = model.TenantID,
                    CreatedBy = model.CreatedBy
                };

                // Insert into database
                var response = await _signatureService.Insert(insertDto);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DigitalSignatureApiConstantsResponseMessage.signature_insert_success_message);
                }
                else
                {
                    // If database insert fails, delete the uploaded file
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message ?? DigitalSignatureApiConstantsResponseMessage.signature_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DigitalSignatureApiConstantsResponseMessage.signature_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        [HttpPut]
        public async Task<ActionResult<ApiResponse<int>>> UpdateSignature([FromBody] DigitalSignatureUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _signatureService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DigitalSignatureApiConstantsResponseMessage.signature_update_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DigitalSignatureApiConstantsResponseMessage.signature_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DigitalSignatureApiConstantsResponseMessage.signature_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteSignature(int id)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _signatureService.Delete(id);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DigitalSignatureApiConstantsResponseMessage.signature_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, DigitalSignatureApiConstantsResponseMessage.signature_deleted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, DigitalSignatureApiConstantsResponseMessage.signature_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}

