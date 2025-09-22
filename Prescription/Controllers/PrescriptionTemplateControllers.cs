using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prescription.Application.services;
using Prescription.Application.Services;
using Prescription.Dtos.RequestDto.PrescriptionCreateDto;
using Prescription.Dtos.RequestDto.PrescriptionDto;
using Prescription.Dtos.RequestDto.PrescriptionTemplateDto;
using Prescription.Dtos.RequestDto.ScannedPrescription;
using Prescription.Dtos.ResponseDto.PrescriptionTemplateDto;
using Prescription.Dtos.ResponseDto.ScannedPrescriptionDto;
using Prescription.Utility;
using PrescriptionExamination.Application.Services;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;

namespace Prescription.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PrescriptionTemplateControllers : ControllerBase
    {
        private readonly PrescriptionTemplateService _prescriptionTemplateService;
        private readonly MapperService _mapperService;
        private readonly PrescriptionSymptomService _prescriptionSymptomService;
        private readonly PrescriptionItemService _prescriptionItemService;
        private readonly PrescriptionDiagonsisService _prescriptionDiagonsisService;
        private readonly PrescriptionPatientHistoryService _prescriptionPatientHistoryService;
        private readonly PrescriptionInvestigationService _prescriptionInvestigationService;
        private readonly PrescriptionExaminationService _prescriptionExaminationService;
        private readonly PrescriptionAdviceService _prescriptionAdviceService;
            public PrescriptionTemplateControllers(PrescriptionTemplateService prescriptionTemplateService, MapperService mapperService,
                PrescriptionSymptomService prescriptionSymptomService,
                PrescriptionItemService prescriptionItemService,
                PrescriptionDiagonsisService prescriptionDiagonsisService,
                PrescriptionPatientHistoryService prescriptionPatientHistoryService,
                PrescriptionInvestigationService prescriptionInvestigationService,
                PrescriptionExaminationService prescriptionExaminationService,
                PrescriptionAdviceService prescriptionAdviceService

                )
        {
            _prescriptionTemplateService = prescriptionTemplateService;
            _mapperService = mapperService;
            _prescriptionSymptomService = prescriptionSymptomService;
            _prescriptionItemService = prescriptionItemService;
            _prescriptionDiagonsisService = prescriptionDiagonsisService;
            _prescriptionPatientHistoryService = prescriptionPatientHistoryService;
            _prescriptionInvestigationService = prescriptionInvestigationService;
            _prescriptionExaminationService = prescriptionExaminationService;
            _prescriptionAdviceService = prescriptionAdviceService;


        }
        [Authorize(Policy = PermissionConstants.ScannedPrescriptionGetAll)]
        [HttpGet("gets-all-prescription-template-by-doctor-id")]
        public async Task<ActionResult<ApiResponse<List<PrescriptionTemplateApiResponseDto>>>> GetAllPresciptionTemplagteByDoctorId(int DoctorId,string? Name)
        {
            var apiResponse = new ApiResponse<List<PrescriptionTemplateApiResponseDto>>();
            try
            {
                var prescriptions = await _prescriptionTemplateService.GetPrescriptionTemplatesByDoctorId(DoctorId,Name);
                
                var mappedPrescriptions = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.PrescriptionTemplate, PrescriptionTemplateApiResponseDto>(prescriptions.Result);

                if (prescriptions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescriptions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_see_try_catch);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.ScannedPrescriptionGetId)]
        [HttpGet("get-template-prescription-by-id")]
        public async Task<ActionResult<ApiResponse<PrescriptionRequestDto>>> GetTemplatePrescriptionById(int templateId)
        {
            var apiResponse = new ApiResponse<PrescriptionTemplateApiResponseDto>();
            var responseDto = new PrescriptionRequestDto();
            try
            {
                var prescriptionsTemplate = await _prescriptionTemplateService.GetById(templateId);

                var mappedPrescriptions = await _mapperService.MapSingle<Entities.EntityClass.PrescriptionEntity.PrescriptionTemplate, PrescriptionTemplateApiResponseDto>(prescriptionsTemplate.Result);

                if (prescriptionsTemplate.Result != null)
                {
                    var prescriptionSymtomps = await _prescriptionSymptomService.GetByPrescriptionId(prescriptionsTemplate.Result.PrescriptionID);
                    // Map the response to the required SymptomDto type 
                    if(prescriptionSymtomps.Result != null && prescriptionSymtomps.Result.Any())
                    {
                        responseDto.ChiefComplaints = prescriptionSymtomps.Result?.Select(s => new ChiefComplaintDto
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Notes = s.Notes,
                            Duration = s.Duration,
                            Days = s.Days,

                        }).ToList();
                    }
                    
                   

                    
                        var diagnosisResponse = await _prescriptionDiagonsisService.GetByPrescriptionId(prescriptionsTemplate.Result.PrescriptionID);
                    if (diagnosisResponse.Result != null&& diagnosisResponse.Result.Any())
                    {

                        // Map the response to the required DiagnosisDto type
                        responseDto.Diagnosis = diagnosisResponse.Result?.Select(d => new DiagnosisDto
                        {
                            Id = d.Id,
                            Name = d.DiagnosisName,
                            PastDiagnosis = d.PastDiagnosis,
                            PresentDiagnosis = d.PresentDiagnosis
                        }).ToList();
                    }

                    

                    var prescriptionItem = await _prescriptionItemService.GetByPrescriptionId(prescriptionsTemplate.Result.PrescriptionID);
                    if(prescriptionItem.Result != null && prescriptionItem.Result.Any())
                    {
                        responseDto.Medications = prescriptionItem.Result?.Select(i => new MedicationDto
                        {
                            Id = i.Id,
                            Name = i.Name,
                            Duration = i.Duration,
                            Notes = i.Notes,
                            MealTime = i.MealTime,
                            Timming = i.Timming

                        }).ToList();
                    }

                    var prescriptionPatientHistory = await _prescriptionPatientHistoryService.GetByPrescriptionId(prescriptionsTemplate.Result.PrescriptionID);
                   
                    if(prescriptionPatientHistory.Result != null && prescriptionPatientHistory.Result.Any())
                    {
                        responseDto.History = prescriptionPatientHistory.Result?.Select(h => new HistoryDto
                        {
                            Id = h.Id,
                            Name = h.Name,
                            PastHistory = h.PastHistory,
                            PresentHistory = h.PresentHistory
                        }).ToList();
                    }


                    var prescriptionAdvice = await _prescriptionAdviceService.GetByPrescriptionId(prescriptionsTemplate.Result.PrescriptionID);

                    if (prescriptionAdvice.Result != null && prescriptionAdvice.Result.Any())
                    {
                        responseDto.Advice = prescriptionAdvice.Result?.Select(a => new AdviceDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Notes = a.Notes
                        }).ToList();
                    }


                    var prescriptionInvestigation = await _prescriptionInvestigationService.GetByPrescriptionId(prescriptionsTemplate.Result.PrescriptionID);
                    if (prescriptionInvestigation != null&&prescriptionInvestigation.Result.Any())
                    {
                        responseDto.Test = prescriptionInvestigation.Result?.Select(i => new TestDto
                        {
                            Id = i.Id,
                            Name = i.Name,
                            Notes = i.Notes
                        }).ToList();
                    }
                   
                    var prescriptionExamination = await _prescriptionExaminationService.GetByPrescriptionId(prescriptionsTemplate.Result.PrescriptionID);
                    if(prescriptionExamination.Result != null && prescriptionExamination.Result.Any())
                    {
                        responseDto.Examination = prescriptionExamination.Result?.Select(e => new ExaminationDto
                        {
                            Systolic = e.Systolic,
                            Diastolic = e.Diastolic,
                            Pulse = e.Pulse,
                            Temperature = e.Temperature,
                            Weight = e.Weight,
                            HeightFeet = e.HeightFeet,
                            HeightInches = e.HeightInches,

                        }).ToList();
                    }
                  
                }

                if (prescriptionsTemplate.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_null_of_get_list);
                    return Ok(apiResponse);
                }
                apiResponse.Results = mappedPrescriptions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_see_try_catch);
            }
            return Ok(responseDto);
        }
        [Authorize(Policy = PermissionConstants.ScannedPrescriptionCreate)]
        [HttpPost("create-template-prescription")]
        public async Task<ActionResult<ApiResponse<int>>> CreateTemplatePrescription(PrescriptionTemplateInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionTemplateService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_insert_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.ScannedPrescriptionUpdate)]
        [HttpPut("update-template-prescription")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateTemplatePrescription(PrescriptionTemplateUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionTemplateService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_see_try_catch);
                return Ok(apiResponse);
            }
            return Ok(apiResponse);
        }
        [Authorize(Policy = PermissionConstants.ScannedPrescriptionDelete)]
        [HttpDelete("delete-template-prescription")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteTemplatePrescription(int prescriptionAdviceId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionTemplateService.Delete(prescriptionAdviceId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionTemplateApiConstantsResponseMessage.prescription_template_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
