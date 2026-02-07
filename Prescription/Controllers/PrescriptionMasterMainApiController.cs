using Prescription.Application.Services;
using Prescription.Dtos.RequestDto.PrescriptionDto;
using Prescription.Dtos.ResponseDto.PrescriptionDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace Prescription.Controllers
{
    [ApiController]
    [Route("api/app/prescription-master")]
    public class PrescriptionMasterMainApiController : ControllerBase
    {
        private readonly PrescriptionService _prescriptionService;
        private readonly MapperService _mapperService;

        public PrescriptionMasterMainApiController(PrescriptionService prescriptionService, MapperService mapperService)
        {
            _prescriptionService = prescriptionService;
            _mapperService = mapperService;
        }

        [HttpPost]
        [Authorize(Policy = PermissionConstants.PrescriptionCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreatePrescriptionMaster([FromBody] PrescriptionInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _prescriptionService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Prescription created successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetId)]
        public async Task<ActionResult<ApiResponse<PrescriptionApiResponseDto>>> GetPrescriptionById(int id)
        {
            var apiResponse = new ApiResponse<PrescriptionApiResponseDto>();
            try
            {
                var prescription = await _prescriptionService.GetById(id);
                if (prescription.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Prescription not found");
                    return Ok(apiResponse);
                }

                var mappedPrescription = await _mapperService.MapSingle<Entities.EntityClass.PrescriptionEntity.Prescription, PrescriptionApiResponseDto>(prescription.Result);
                apiResponse.Results = mappedPrescription;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Prescription retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<List<PrescriptionApiResponseDto>>>> GetAllPrescriptions()
        {
            var apiResponse = new ApiResponse<List<PrescriptionApiResponseDto>>();
            try
            {
                var prescriptions = await _prescriptionService.GetAll();
                if (prescriptions.Result == null || prescriptions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PrescriptionApiResponseDto>(), "No prescriptions found");
                    return Ok(apiResponse);
                }

                var mappedPrescriptions = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.Prescription, PrescriptionApiResponseDto>(prescriptions.Result);
                apiResponse.Results = mappedPrescriptions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Prescriptions retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PrescriptionApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("patient-disease-list/{patientId}")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<List<object>>>> GetPatientDiseaseList(int patientId)
        {
            var apiResponse = new ApiResponse<List<object>>();
            try
            {
                var diseaseList = await _prescriptionService.GetPatientDiseaseList(patientId);
                if (diseaseList.Result == null || diseaseList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<object>(), "No disease list found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = diseaseList.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Patient disease list retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<object>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("prescription-count")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<int>>> GetPrescriptionCount()
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var prescriptions = await _prescriptionService.GetAll();
                var count = prescriptions.Result?.Count ?? 0;
                ApiResponseHelper.SetSuccessResponse(apiResponse, count, "Prescription count retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("prescription-list-by-appointment-creator-id/{patientId}")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<List<PrescriptionApiResponseDto>>>> GetPrescriptionsByAppointmentCreatorId(int patientId)
        {
            var apiResponse = new ApiResponse<List<PrescriptionApiResponseDto>>();
            try
            {
                var prescriptions = await _prescriptionService.GetByAppointmentCreatorId(patientId);
                if (prescriptions.Result == null || prescriptions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PrescriptionApiResponseDto>(), "No prescriptions found");
                    return Ok(apiResponse);
                }

                var mappedPrescriptions = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.Prescription, PrescriptionApiResponseDto>(prescriptions.Result);
                apiResponse.Results = mappedPrescriptions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Prescriptions retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PrescriptionApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("prescription-master-list-by-doctor-id/{doctorId}")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<List<PrescriptionApiResponseDto>>>> GetPrescriptionsByDoctorId(int doctorId)
        {
            var apiResponse = new ApiResponse<List<PrescriptionApiResponseDto>>();
            try
            {
                var prescriptions = await _prescriptionService.GetByDoctorId(doctorId);
                if (prescriptions.Result == null || prescriptions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PrescriptionApiResponseDto>(), "No prescriptions found");
                    return Ok(apiResponse);
                }

                var mappedPrescriptions = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.Prescription, PrescriptionApiResponseDto>(prescriptions.Result);
                apiResponse.Results = mappedPrescriptions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Prescriptions retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PrescriptionApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("prescription-master-list-by-doctor-id-patient-id")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<List<PrescriptionApiResponseDto>>>> GetPrescriptionsByDoctorIdAndPatientId([FromQuery] int doctorId, [FromQuery] int patientId)
        {
            var apiResponse = new ApiResponse<List<PrescriptionApiResponseDto>>();
            try
            {
                var prescriptions = await _prescriptionService.GetByDoctorIdAndPatientId(doctorId, patientId);
                if (prescriptions.Result == null || prescriptions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PrescriptionApiResponseDto>(), "No prescriptions found");
                    return Ok(apiResponse);
                }

                var mappedPrescriptions = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.Prescription, PrescriptionApiResponseDto>(prescriptions.Result);
                apiResponse.Results = mappedPrescriptions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Prescriptions retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PrescriptionApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("prescription-master-list-by-patient-id/{patientId}")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<List<PrescriptionApiResponseDto>>>> GetPrescriptionsByPatientId(int patientId)
        {
            var apiResponse = new ApiResponse<List<PrescriptionApiResponseDto>>();
            try
            {
                var prescriptions = await _prescriptionService.GetByPatientId(patientId);
                if (prescriptions.Result == null || prescriptions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PrescriptionApiResponseDto>(), "No prescriptions found");
                    return Ok(apiResponse);
                }

                var mappedPrescriptions = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.Prescription, PrescriptionApiResponseDto>(prescriptions.Result);
                apiResponse.Results = mappedPrescriptions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Prescriptions retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PrescriptionApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPut]
        [Authorize(Policy = PermissionConstants.PrescriptionUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePrescriptionMaster([FromBody] PrescriptionUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _prescriptionService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Prescription updated successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }
    }
}

