
using ApiCallService.BaseApiCallService;
using DoctorExpertise.Dtos.RequestDto.DoctorExpertiseDto;
using Entities.EntityClass;
using Entities.EntityClass.DoctorEntity;
using Entities.EntityClass.PatientEntity;
using Entities.EntityClass.PrescriptionEntity;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prescription.Application.services;
using Prescription.Application.Services;
using Prescription.Dtos.CommonDto;
using Prescription.Dtos.RequestDto.DegreeDto;
using Prescription.Dtos.RequestDto.DoctorChamberDto;
using Prescription.Dtos.RequestDto.DoctorDegreeDto;
using Prescription.Dtos.RequestDto.DoctorDto;
using Prescription.Dtos.RequestDto.DoctorScheduleDto;
using Prescription.Dtos.RequestDto.ExaminationsDto;
using Prescription.Dtos.RequestDto.ExpertiseCategoryDto;
using Prescription.Dtos.RequestDto.GeminiDto;
using Prescription.Dtos.RequestDto.NotificationDto;
using Prescription.Dtos.RequestDto.Patients;
using Prescription.Dtos.RequestDto.PatientsDto;
using Prescription.Dtos.RequestDto.PrescriptionAdvice;
using Prescription.Dtos.RequestDto.PrescriptionCreateDto;
using Prescription.Dtos.RequestDto.PrescriptionDiagonosisDto;
using Prescription.Dtos.RequestDto.PrescriptionDto;
using Prescription.Dtos.RequestDto.PrescriptionExaminationDto;
using Prescription.Dtos.RequestDto.PrescriptionFollowUpDto;
using Prescription.Dtos.RequestDto.PrescriptionInvestigationDto;
using Prescription.Dtos.RequestDto.PrescriptionItem;
using Prescription.Dtos.RequestDto.PrescriptionPatientHistoryDto;
using Prescription.Dtos.RequestDto.PrescriptionPdfDto;
using Prescription.Dtos.RequestDto.PrescriptionSymtomDto;
using Prescription.Dtos.RequestDto.PrescriptionTemplateDto;
using Prescription.Dtos.RequestDto.ScannedPrescription;
using Prescription.Dtos.RequestDto.ScheduleDto;
using Prescription.Dtos.RequestDto.UserDto;
using Prescription.Dtos.ResponseDto.PatientResponse;
using Prescription.Dtos.ResponseDto.PrescriptionDto;
using Prescription.Dtos.ResponseDto.UserDto;
using Prescription.Utility;
using PrescriptionExamination.Application.Services;
using PrescriptionPdf.Dtos.RequestDto.PdfDto;
using PrescriptionPdf.Dtos.ResponseDto.PdfDto;
using QRCoder;
using RestSharp;
using SharedService.CommonService;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;
using static System.Net.Mime.MediaTypeNames;

namespace Prescription.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PrescriptionController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly PrescriptionService _prescriptionService;
        private readonly PrescriptionPatientService _prescriptionPatientService;
        private readonly PrescriptionAdviceService _prescriptionAdviceService;
        private readonly PrescriptionInvestigationService _prescriptionInvestigationService;
        private readonly PrescriptionDiagonsisService _prescriptionDiagonsisService;
        private readonly PrescriptionItemService _prescriptionItemService;
        private readonly PrescriptionFollowUpService _prescriptionFollowUpService;
        private readonly PrescriptionPatientHistoryService _prescriptionPatientHistoryService;
        private readonly PrescriptionSymptomService _prescriptionSymptomService;
        private readonly PrescriptionExaminationService _prescriptionExaminationService;
        private readonly PrescriptionPdfCreatorService _pdfService;
        private readonly PrescriptionTemplateService _prescriptionTemplateService;
        private readonly ScannedPrescriptionService _scannedPrescriptionService;
        private readonly IConfiguration _configuration;
        private readonly IBaseRestClientApiService _baseRestClientApiService;

        public PrescriptionController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            PrescriptionService prescriptionService,
            PrescriptionPatientService prescriptionPatientService,
            PrescriptionAdviceService prescriptionAdviceService,
            PrescriptionInvestigationService prescriptionInvestigationService,
            PrescriptionDiagonsisService prescriptionDiagonsisService,
            PrescriptionItemService prescriptionItemService,
            PrescriptionFollowUpService prescriptionFollowUpService,
            PrescriptionPatientHistoryService prescriptionPatientHistoryService,
            PrescriptionSymptomService prescriptionSymptomService,
            PrescriptionExaminationService prescriptionExaminationService,
             PrescriptionPdfCreatorService pdfService,
             PrescriptionTemplateService prescriptionTemplateService,
             ScannedPrescriptionService scannedPrescriptionService,
             IBaseRestClientApiService baseRestClientApiService,
             IConfiguration configuration
            )
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _prescriptionService = prescriptionService;
            _prescriptionPatientService = prescriptionPatientService;
            _prescriptionAdviceService = prescriptionAdviceService;
            _prescriptionInvestigationService = prescriptionInvestigationService;
            _prescriptionDiagonsisService = prescriptionDiagonsisService;
            _prescriptionDiagonsisService = prescriptionDiagonsisService;
            _prescriptionItemService = prescriptionItemService;
            _prescriptionFollowUpService = prescriptionFollowUpService;
            _prescriptionPatientHistoryService = prescriptionPatientHistoryService;
            _prescriptionSymptomService = prescriptionSymptomService;
            _prescriptionExaminationService = prescriptionExaminationService;
            _prescriptionTemplateService = prescriptionTemplateService;
            _pdfService = pdfService;
            _scannedPrescriptionService = scannedPrescriptionService;
            _baseRestClientApiService = baseRestClientApiService;
            _configuration = configuration;
        }

        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        [HttpGet("gets-all-prescription")]
        public async Task<ActionResult<ApiResponse<List<PrescriptionApiResponseDto>>>> GetAllPrescription()
        {
            var apiResponse = new ApiResponse<List<PrescriptionApiResponseDto>>();
            try
            {
                var prescriptions = await _prescriptionService.GetAll();

                var mappedPrescriptions = await _mapperService.MapList<Entities.EntityClass.PrescriptionEntity.Prescription, PrescriptionApiResponseDto>(prescriptions.Result);

                if (prescriptions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionApiConstantsResponseMessage.prescription_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescriptions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionApiConstantsResponseMessage.prescription_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionApiConstantsResponseMessage.prescription_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionGetId)]
        [HttpGet("get-prescription-by-id")]
        public async Task<ActionResult<ApiResponse<PrescriptionApiResponseDto>>> GetPrescriptionById(int prescriptionId)
        {
            var apiResponse = new ApiResponse<PrescriptionApiResponseDto>();
            try
            {
                var prescription = await _prescriptionService.GetById(prescriptionId);
                var mappedPrescription = await _mapperService.MapSingle<Entities.EntityClass.PrescriptionEntity.Prescription, PrescriptionApiResponseDto>(prescription.Result);
                if (prescription.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionApiConstantsResponseMessage.prescription_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedPrescription;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionApiConstantsResponseMessage.prescription_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionApiConstantsResponseMessage.prescription_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.PrescriptionGetId)]
        [HttpGet("get-prescription-analytics")]
        public async Task<ActionResult<ApiResponse<PrescriptionAnalyticsDto>>> GetPrescriptionAnalytic()
        {
            var apiResponse = new ApiResponse<PrescriptionAnalyticsDto>();
            try
            {
                var prescription = await _prescriptionService.GetAnalytics();
                if (prescription.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionApiConstantsResponseMessage.prescription_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = prescription.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, PrescriptionApiConstantsResponseMessage.prescription_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, PrescriptionApiConstantsResponseMessage.prescription_see_try_catch);
            }
            return Ok(apiResponse);
        }



        [Authorize(Policy = PermissionConstants.PrescriptionCreate)]
        [HttpPost("create-prescription")]
        public async Task<ActionResult<ApiResponse<int>>> CreatePrescription(PrescriptionRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            var pdfResult = new PdfApiResponseDto();
            var pdfApiResponse = new ApiResponse<PdfApiResponseDto>();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionApiConstantsResponseMessage.prescription_inserted_failed_message);
                        return Ok(apiResponse);
                    }

                    var commonDto = new Common { TenantId = 1 };
                    var userResult2 = 0;
                    var getRuleResult2 = new Response<UserApiResponseDto>();

                    if (request.Doctor != null)
                    {
                        if (request.Doctor.DoctorProfileId != 0)
                        {
                            var getRoleResult = await _prescriptionPatientService.UserGetByReferenceIdAndRole(
                                request.Doctor.DoctorProfileId, "Doctor", commonDto.TenantId);

                            getRuleResult2 = getRoleResult;
                            commonDto.userId = getRoleResult?.Result?.UserId;
                        }

                        if (getRuleResult2.Result == null)
                        {
                            var prescriptionUserNew = new UserInsertRequestDto
                            {
                                TenantId = 1,
                                FirstName = request.Doctor.DoctorName,
                                LastName = request.Doctor.DoctorName,
                                PasswordHash = "stdfadsaring",
                                Email = "ihemon2@gmail.com",
                                UserType = "Doctor",
                                IsActive = true,
                                ReferenceUserId = request.Doctor.DoctorProfileId
                            };

                            var userResult = await _prescriptionPatientService.UserInsert(prescriptionUserNew);
                            if (request.Doctor.DoctorProfileId == 0)
                            {
                                var userUpdatedNew = new UserUpdateRequestDto
                                {
                                    UserId = userResult.Result.UserId,
                                    ReferenceUserId = userResult.Result.UserId
                                };

                                var userUpdate = await _prescriptionPatientService.UserUpdate(userUpdatedNew);
                            }
                            userResult2 = userResult.Result.UserId;
                            commonDto.userId = userResult.Result.UserId;

                            var prescriptionDoctorNew = new DoctorInsertRequestDto
                            {
                                UserID = userResult2,
                                Specialization = request.Doctor.specializationName,
                                LicenseNumber = request.Doctor.bmdc,
                                DoctorReferenceID = request.Doctor.DoctorProfileId,
                                HospitalAffiliation = "dfasfasdfafa"
                            };

                            var doctorResult = await _prescriptionPatientService.DoctorInsert(prescriptionDoctorNew);
                            foreach (var chamber in request.Doctor.Chamber)
                            {
                                var entity = new DoctorChamberInsertRequestDto
                                {
                                    ChamberName = chamber.ChamberName,
                                    Address = chamber.Address,
                                    City = chamber.City,
                                    ZipCode = chamber.ZipCode,
                                    Country = chamber.Country,

                                    DistrictId = chamber.districtId,
                                    DivisionId = chamber.divisionId,
                                    DoctorID = doctorResult.Result.DoctorID,
                                    TenantID = 1,
                                    ChamberReferenceId = chamber.chamberId,
                                    IsVisibleOnPrescription = true,
                                    IsDeleted = false
                                };

                                await _prescriptionPatientService.DoctorChamberInsert(entity);
                            }
                            if (request.Doctor.DoctorProfileId == 0)
                            {
                                var doctorNewUpdated = new DoctorUpdateRequestDto
                                {
                                    DoctorID = doctorResult.Result.DoctorID,
                                    UserID = userResult2,
                                    DoctorReferenceID = userResult2
                                };
                                var updatedDoctorResult = await _prescriptionPatientService.DoctorUpdate(doctorNewUpdated);

                            }



                            commonDto.DoctorId = doctorResult.Result?.DoctorID;

                            //await InsertDoctorDetails(request, commonDto);
                        }
                        else
                        {
                            var getDoctorDetails = await _prescriptionPatientService.GetByDoctorid(commonDto.userId);

                            if (getDoctorDetails.Result == null)
                            {
                                var prescriptionDoctorNew = new DoctorInsertRequestDto
                                {
                                    UserID = commonDto.userId,
                                    Specialization = request.Doctor.specializationName,
                                    LicenseNumber = request.Doctor.bmdc,
                                    DoctorReferenceID = request.Doctor.DoctorProfileId,
                                    HospitalAffiliation = "dfasfasdfafa"
                                };

                                if (request.Doctor.DoctorProfileId != null)
                                {
                                    var doctorResult = await _prescriptionPatientService.DoctorInsert(prescriptionDoctorNew);
                                    commonDto.DoctorId = doctorResult.Result?.DoctorID;
                                }




                            }
                            else
                            {
                                commonDto.DoctorId = getDoctorDetails.Result.DoctorID;
                            }
                        }
                    }
                    if (request.Patient.PatientProfileId == 0)
                    {
                        var existingPatient= await _prescriptionPatientService.GetByPhoneNo(request.Patient.patientPhoneNo);
                        if(existingPatient.IsSuccess && existingPatient.Result != null)
                        {
                            commonDto.PatientId = existingPatient.Result.PatientID;
                        }
                        else
                        {
                            userResult2 = await InsertPatient(request, commonDto, userResult2);

                        }

                    }
                    else
                    {
                        commonDto.PatientId = request.Patient.PatientProfileId;
                    }


                  var prescriptionCode = "PS" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

                    var prescription = new PrescriptionInsertRequestDto
                    {
                        TenantId = commonDto.TenantId,
                        PatientFollowUpId = 1,
                        PatientId = commonDto?.PatientId,
                        PharmacyId = 1,
                        DoctorId = commonDto?.DoctorId,
                        isHeader = request.isHeader,
                        isPreHand = request.isPreHand,
                        AppointmentRefId = request.appointmentId,
                        FollowUpDate = !string.IsNullOrEmpty(request.FollowUp) ? DateTime.Parse(request.FollowUp) : DateTime.Today,
                        PrescriptionCode = prescriptionCode

                    };

                    var prescriptionResult = await _prescriptionService.Insert(prescription);

                    if (string.IsNullOrEmpty(request.uploadImage)) {
                        await InsertAdvice(request, prescriptionResult);
                        await InsertInvestigation(request, prescriptionResult);
                        await InsertDiagonosis(request, prescriptionResult);
                        await InsertHistory(request, prescriptionResult);
                        await InsertChiefComplain(request, prescriptionResult);
                        await InsertMedication(request, prescriptionResult);
                        await InsertExamination(request, commonDto, prescriptionResult);
                        await InsertTemplate(request, commonDto, prescriptionResult);
                    }



                    if (prescriptionResult.IsSuccess)
                    {
                        scope.Complete();
                        var pdfInsertResult = "";

                        // Here Create PDF Prescription and send the patient.
                        // Get Email Template or Message template
                        // var pdfInsertResult 
                        if (request.isTemplate == true)
                        {
                            ApiResponseHelper.SetSuccessResponse(pdfApiResponse, null, "Prescription Save As Template");



                        }
                        else
                        {
                            pdfInsertResult = await GeneratePdfPrescription(request, commonDto, prescriptionResult.Result, prescriptionCode);
                            pdfResult.FilePath = pdfInsertResult;


                            ApiResponseHelper.SetSuccessResponse(pdfApiResponse, pdfResult, PrescriptionApiConstantsResponseMessage.prescription_insert_success_message);
                        }

                    }
                    else
                    {
                        scope.Dispose();
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, prescriptionResult.Message);
                    }
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"{PrescriptionApiConstantsResponseMessage.prescription_see_try_catch}-{ex.Message}");
                }

                return Ok(pdfApiResponse);
            }
        }






        private async Task<string> GeneratePdfPrescription(PrescriptionRequestDto request, Common commonDto, int PrescriptionId, string prescriptionCode)
        {
            try
            {

                var doctorDegree = request.Doctor.Degree;

                string doctorDetailsHtml = "";
                var chambers = request.Doctor.Chamber;
                string chamberHtml = "";
                var complaints = request.ChiefComplaints;
                string complaintsHtml = "";
                var histories = request.History;
                string historyHtml = "";

                var examinations = request.Examination;
                string exminationHtml = "";
                var investigations = request.Test;
                string investigationHtml = "";
                var advices = request.Advice;
                string adviceHtml = "";
                var followUp = request.FollowUp;
                string followHtml = "";
                var medications = request.Medications;
                string medicationHtml = "";
                var schedules = request.Doctor.Schedule;
                string scheduleHtml = "";

                var diagnosis = request.Diagnosis;
                string diagnosisHtml = "";



                if (doctorDegree != null)
                {
                    // Take up to 4 degrees
                    var degreesToShow = doctorDegree.Take(4);

                    foreach (var degree in degreesToShow)
                    {
                        doctorDetailsHtml += $"<p style=\"margin: 5px 0; font-size: 18px;\">{degree.DegreeName}-{degree.InstituteName}-{degree.InstituteCity}</p>";
                    }
                }


                if (chambers != null && chambers.Any())
                {
                    foreach (var chamber in chambers)
                    {
                        chamberHtml += $"<h3 style=\"margin: 5px 0; font-size: 18px;color: #00aaff;\">{chamber.ChamberName}</h3>";
                        chamberHtml += $"<p style=\"margin: 5px 0; font-size: 18px;\">{chamber.Address}</p>";

                    }
                }
                else
                {
                    if (request.isPreHand || chambers?.Count == 0)
                    {
                        chamberHtml += @"

";

                    }
                }


                if (schedules != null && schedules.Any())
                {
                    scheduleHtml = string.Join(", ", schedules.Select(s =>
                    {


                        // Handle Start-End display
                        var startEndText = s.Start == s.End ? s.Start : $"{s.Start}-{s.End}";

                        // Format in 12-hour style (e.g., "09:00 AM")
                        var timeRange = $"{s.StartTime:hh:mm tt}-{s.EndTime:hh:mm tt}";

                        return $"<span style=\"font-size: 14px; font-weight: bold;\"> {startEndText}  ({timeRange})</span>";
                    }));
                }








                if (string.IsNullOrEmpty(request.uploadImage))
                {
                    if (complaints != null && complaints.Any())
                    {
                        complaintsHtml = "<ul style=\"padding-left: 20px;margin: 0;color:#7F7F7F;\">";
                        foreach (var complaint in complaints)
                        {
                            complaintsHtml += $"<li style=\"margin: 5px 0; font-size: 18px;\">{complaint.Name} &nbsp {complaint.Days} &nbsp {complaint.Notes} </li>";
                        }
                        complaintsHtml += "</ul>";

                    }

                    

                    if (diagnosis != null && diagnosis.Any())
                    {
                        diagnosisHtml = "<ul style=\"padding-left: 20px;margin: 0;color:#7F7F7F;\">";
                        foreach (var diago in diagnosis)
                        {
                            diagnosisHtml += $"<li style=\"margin: 5px 0; font-size: 18px;\">{diago.Name} &nbsp {diago.PastDiagnosis} &nbsp {diago.PresentDiagnosis} </li>";
                        }
                        diagnosisHtml += "</ul>";
                    }

                    if (histories != null && histories.Any())
                    {
                        historyHtml = "<ul style=\"padding-left: 20px;margin: 0;color:#7F7F7F;\">";
                        foreach (var history in histories)
                        {
                            historyHtml += $"<li style=\"margin: 5px 0; font-size: 18px;\">{history.Name} &nbsp {history.PastHistory} &nbsp {history.PresentHistory} </li>";
                        }
                        historyHtml += "</ul>";
                    }


                    if (examinations != null)
                    {
                        exminationHtml = "<ul style=\"padding-left: 20px;margin: 0;color:#7F7F7F;\">";

                        foreach (var examination in examinations)
                        {
                            exminationHtml += $"<li style=\"margin: 2px 0; font-size: 18px;\">BP:{examination.Systolic}/{examination.Diastolic} </li>";
                            exminationHtml += $"<li style=\"margin: 2px 0; font-size: 18px;\">Pulse:{examination.Pulse} </li>";
                            exminationHtml += $"<li style=\"margin: 2px 0; font-size: 18px;\">Weight:{examination.Weight} </li>";
                            exminationHtml += $"<li style=\"margin: 2px 0; font-size: 18px;\">Height:{examination.HeightFeet}-{examination.HeightInches} </li>";

                        }
                        exminationHtml += "</ul>";

                    }


                    if (investigations != null && investigations.Any())
                    {
                        investigationHtml = "<ul style=\"padding-left: 20px;margin: 0;color:#7F7F7F;\">";
                        foreach (var investigation in investigations)
                        {
                            investigationHtml += $"<li style=\"margin: 1px 0; font-size: 18px;\">{investigation.Name} &nbsp {investigation.Notes} </li>";

                        }
                        investigationHtml += "</ul>";

                    }




                    if (advices != null && advices.Any())
                    {
                        adviceHtml = "<ul style=\"padding-left: 20px;margin: 0;color:#7F7F7F;\">";

                        foreach (var advice in advices)
                        {
                            adviceHtml += $"<li style=\"margin: 1px 0; font-size: 18px;\">{advice.Name} &nbsp {advice.Notes} </li>";

                        }
                        adviceHtml += "</ul>";
                    }

                    if (!string.IsNullOrWhiteSpace(followUp) && DateTime.TryParse(followUp, out var parsedDate))
                    {
                        followHtml = $"<ul style=\"padding-left: 20px;color:#7F7F7F;margin: 0;\"><li style=\"margin: 0; font-size: 18px;\">{parsedDate:MM/dd/yyyy}</li></ul>";
                    }


                    if (medications != null && medications.Any())
                    {
                        medicationHtml = "<ol style=\"padding-left: 20px;color:#7F7F7F;margin: 0;\">";

                        foreach (var medication in medications)
                        {
                            medicationHtml += "<div style=\"padding: 10px 0;\">"; // Padding between medicines
                            medicationHtml += $"<li style=\"margin: 5px 0; font-size: 20px;font-weight: bold; \">{medication.Name}</li>";
                            medicationHtml += $"<p style=\"margin: 5px 2px; font-size: 18px;padding-left:20px;\">{medication.Timming} &nbsp {medication.MealTime} &nbsp {medication.Duration} &nbsp {medication.Notes}</p>";
                            medicationHtml += "</div>";
                        }

                        medicationHtml += "</ol>";
                    }





                }


                var imageHtmlCode = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Medical Prescription</title>
                </head>
                <body style=""margin: 0; display: flex; justify-content: center; align-items: center;"">
                    <div style=""width: 900px; height: 1300px;"">
                        <img src=""{request.uploadImage}"" 
                             alt=""Uploaded Prescription"" 
                             style="" width: 100%;height:1300px;"" />
                    </div>
                </body>
                </html>";







                // Admin Send Email
                string whk = _configuration.GetSection("AppSettings").GetSection("PDFSOFTWAREPATH").Value;
                // C:\Users\KOW\AppData\Roaming
                //string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string appDataFolder = _configuration.GetSection("AppSettings").GetSection("PDFCREATEDPATH").Value;
                string subdirectory = "KOW";
                string fileName = $"prescription.html";
                string htmlFilePath = Path.Combine(appDataFolder, fileName);
                string pdfFileName = $"{PrescriptionId}_{commonDto.PatientId}_{commonDto.DoctorId}.pdf";
                string pdfFilePath = Path.Combine(appDataFolder, pdfFileName);
                //string imagePath = _configuration.GetSection("AppSettings").GetSection("STATICIMAGEPATH").Value;
                //byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                //string base64Image = Convert.ToBase64String(imageBytes);
                var pdfInsertModel = new PdfInsertRequestDto
                {
                    PrescriptionID = PrescriptionId,
                    DoctorID = commonDto.DoctorId,
                    PatientID = commonDto.PatientId,
                    FilePath = Path.Combine("Prescriptions", pdfFileName).Replace("\\", "/"),
                    AppointmentRefId = request.appointmentId

                };

                string prescriptionBaseUrl = _configuration["GeneralSettings:PrescriptionBaseURL"];

                //var QR = GenerateQrCodeBase64("https://soowgood.com");

                var htmlCode = $@"
                        <meta charset=""UTF-8"">
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <title>Medical Prescription Form</title>
                        </head>
                        <body style=""font-family: 'Roboto', sans-serif;"">
                            <div style=""width: 980px;  display: flex; flex-direction: column;height: 1220px;"">
        
                                <!-- Header Section -->
                                <div style=""background-color: #f0f0f0; padding: 10px; border-bottom: 1px solid #ccc;height:15%"">

                                {(request.isHeader ? $@"
                                    <div style=""float: left; width: 70%;"">
                                        <h3 style=""margin: 5px 0; font-size: 22px;color: #00A87E;"">{request.Doctor.DoctorName}</h3>
                                        {doctorDetailsHtml}
                                       <p style=""margin: 5px 0; font-size: 14px;"">{request.Doctor.AreaOfExperties}</p>

                   <p style=""margin: 5px 0; font-size: 18px;\"">BMDC:{request.Doctor.bmdc}</p>

                                    </div>
                                    <div style=""float: right; width: 30%; text-align: right;"">
                                        {chamberHtml}
                                        {scheduleHtml}
                                    </div>
                                    <div style=""clear: both;""></div>
                                " : "")}
                        </div>

                                <!-- Patient Info Section -->
                                <table style=""width: 100%;p border-collapse: collapse; border-bottom: 1px solid #ccc;"">
                                    <tr>
                                      <td style=""padding: 8px 25px; font-size: 14px; font-weight: bold; color:#24141;"">
                                Name: <font color:#1F1F1F;>{request.Patient.PatientName}</font>
                            </td>

                            <td style=""padding: 8px 25px; font-size: 14px; font-weight: bold; color:#24141;"">
                                Age: <font color:#1F1F1F;>{request.Patient.PatientAge}</font>
                            </td>

                            <td style=""padding: 8px 25px; font-size: 14px; font-weight: bold; color:#24141;"">
                                Blood Group: <font color:#1F1F1F;>{request.Patient.patientBloodGroup}</font>
                            </td>

                            <td style=""padding: 8px 25px; font-size: 14px; font-weight: bold; color:#24141;"">
                                Gender: <font color:#1F1F1F;>{request.Patient.patientGender}</font>
                            </td>



                                    </tr>
                                </table>


                                <!-- Main Content Section -->
                                <table style=""width: 100%; border-collapse: collapse;height: 93%;"">
                                    <!-- Left Section - Patient History -->
                                    <tr>
                                        <td style=""width: 30%; padding: 10px; border-right: 1px solid #ccc; vertical-align: top;padding-left:20px;"">
                                            <h4 style=""margin: 5px 0;padding-bottom: 2px;color: #00A87E;"">Chief Complaint</h4>
                                            {complaintsHtml}
                                            <h4 style=""margin: 5px 0; padding-bottom: 2px;color: #00A87E;"">History</h4>
                                            {historyHtml}
                                            <h4 style=""margin: 5px 0; padding-bottom: 2px;color: #00A87E;"">On Examinations</h4>
                                            {exminationHtml}
                                            <h4 style=""margin: 5px 0 5px 0; padding-bottom: 2px;color: #00A87E;"">Diagnosis</h4>
                                              {diagnosisHtml}                    
                                            <h4 style=""margin: 5px 0 5px 0; padding-bottom: 2px;color: #00A87E;"">Investigation</h4>
                                                                                        {investigationHtml}



                                                <h4 style=""margin: 5px 0 5px 0;  padding-bottom: 2px;color: #00A87E;"">Follow Up</h4>
                                                                    {followHtml}
                                                                </td>

                                        <!-- Right Section - Prescription -->
                                        <td style=""width: 70%; padding: 10px; vertical-align: top;padding-left:20px;"">
                                            <h3 style=""margin: 1px 0;  padding-bottom: 2px;color:#00A87E;"">Rx.</h3>
                                            <div style=""margin: 10px 0;"">
                                                {medicationHtml}
                                            </div>

                                            <h3 style=""margin: 1px 0 5px 0;  padding-bottom: 5px;color:#00A87E;"">Advices</h3>
                                            {adviceHtml}

                    
                                        </td>
                                         <td style=""width: 30%; vertical-align: bottom;"">
                                            <div style=""text-align: center;"">
                                                                <img src=""{request.Doctor.signature}"" alt=""Doctor's Signature"" height=""100"" width=""150"">

                                                <div style=""border-top: 1px solid #000; width: 150px; margin: 0 auto;""></div>
                                                <p style=""margin: 5px 0 0 0; font-size: 12px; text-align: center;"">
                                                    Doctor's Signature
                                                </p>
                                            </div>
                                        </td>
                                    </tr>
                                </table>



                               <!-- Footer Section -->
                      <div style=""width: 100%; padding: 5px; border-top: 1px solid #ccc; font-size: 12px; color: #666; background-color: #f9f9f9; margin-top: auto;"">

    <table style=""width:100%; margin-top:5px;"">
        <tr>
            <!-- Left -->
            <td style=""text-align:left; vertical-align:top; border:none;"">
  <p style=""margin:5px 0 0 0;color:#7F7F7F;"">
  Date issued: {TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time")).ToString("MM/dd/yyyy hh:mm tt")}

</p>


            </td>

            <!-- Right -->
            <td style=""text-align:right; vertical-align:top; border:none;"">
                
                <table style=""display:inline-block;"">
                    <tr>
                        <td style=""color:#00A87E; font-size:14px; padding:0; border:none;"">
                            Powered By:
                        </td>
                        <td style=""color:#7F7F7F; font-size:14px; padding-left:5px; border:none;"">
                            Prescripto
                        </td>
                    </tr>
                </table>

                <p style=""margin:3px 0 0 0;color:#7F7F7F;"">
                    PrescriptionCode: {prescriptionCode}
                </p>
            </td>
        </tr>
    </table>

</div>


                        </div>

                            </div>
                        </body>
                        </html>";



                HtmlDocument doc = new HtmlDocument();
                if (!string.IsNullOrEmpty(request.uploadImage))
                {
                    doc.LoadHtml(imageHtmlCode);


                }
                else
                {
                    doc.LoadHtml(htmlCode);

                }
                HtmlNode tbody = doc.DocumentNode.SelectSingleNode("//body");

                var sl = 0;
                //foreach (var service in mail.OrderImageServicesList)
                //{
                //    sl++;
                //    var td = $"<td style='padding: 8px;'>{sl}</td>\r\n <td style='padding: 8px;'>{service.name}</td>\r\n <td style='padding: 8px;'>{service.TotalImage}</td>\r\n <td style='padding: 8px;'>{service.point}</td>\r\n <td style='padding: 8px;'>{service.spend_point}</td>";
                //    HtmlNode newRow = HtmlNode.CreateNode($"<tr>{td}</tr>");
                //    tbody.AppendChild(newRow);
                //}

                string mergeModifiedHtml = doc.DocumentNode.OuterHtml;
                //var totalPoint = mail.OrderImageServicesList.Sum(x => x.spend_point);
                var modifiedHtml = mergeModifiedHtml.Replace("{Total Charge : $20.00}", $"Total Charge : {0}");

                //if (mail.orderMasterChargeBreakdown != null)
                //{
                //    modifiedHtml = modifiedHtml
                //        .Replace("{Date}", mail.orderMasterChargeBreakdown.order_time)
                //        .Replace("{OrderNumber}", mail.orderMasterChargeBreakdown.order_no)
                //        .Replace("{SubscriptionPlan}", mail.orderMasterChargeBreakdown.order_subscription_plan_type)
                //        .Replace("{PaymentStatus}", mail.orderMasterChargeBreakdown.order_payment_status)
                //        .Replace("{OrderStatus}", mail.orderMasterChargeBreakdown.order_status)
                //        .Replace("{RawImageCount}", mail.orderMasterChargeBreakdown.order_no_of_images.ToString())
                //        .Replace("{Current Balance : 600 Credit(s)}", $"{{Current Balance : {mail.orderMasterChargeBreakdown.current_balance} Credit(s)}}");
                //}

                var pdfRequestModel = new PrescriptionPdfRequestDto
                {
                    HtmlContent = modifiedHtml,
                    PdfFilePath = pdfFilePath,
                    WkhtmltopdfPath = whk,
                    FileName = fileName
                };

                var result = await _pdfService.PdfInsert(pdfRequestModel);

                //try
                //{
                //    var subject = template.Result.header;
                //    var body = $"{modifiedHtml}\n{template.Result.footer}";
                //    Thread email = new Thread(delegate ()
                //    {
                //        var isEmailSent = new EmailSender(_configuration).SendMailGun(subject, body, emailList, null, pdfFilePath);
                //    });
                //    email.IsBackground = true;
                //    email.Start();
                //    response.IsSuccess = true;
                //}
                //catch (Exception e)
                //{
                //    response.IsSuccess = false;
                //    response.Message = e.Message;
                //}






                var pdfResult = await _pdfService.DbPdfInsert(pdfInsertModel);
                var baseUrl = _configuration.GetSection("GeneralSettings").GetSection("PrescriptionBaseURL").Value;
                var PrescriptionLink = baseUrl + pdfInsertModel.FilePath;

                var notification = new SmsSendRequestDto()
                {
                    MobileNo = request.Patient.patientPhoneNo,
                    Sms = $"Dear {request.Patient.PatientName},\r\n" +
                    $"Your prescription from {request.Doctor.DoctorName} is now available. " +
                    $"Please click the link below to view or download it:\r\n {PrescriptionLink} " +
                    "\r\nIf you have any questions or need further assistance, feel free to contact us.\r\n" +
                    "Best regards,\r\n Prescripto"
                };
                if (!string.IsNullOrWhiteSpace(request.Patient.patientPhoneNo))
                {
                    var notifications = await _pdfService.Notification(notification);
                }







                return pdfInsertModel.FilePath;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task InsertExamination(PrescriptionRequestDto request, Common? commonDto, Response<int> prescriptionResult)
        {
            if ((request.Examination != null) && prescriptionResult.Result != 0)
            {
                foreach (var examination in request.Examination)
                {
                    var examinationInsertNew = new ExaminationsInsertRequestDto
                    {
                        DoctorId = commonDto?.DoctorId,
                        PatientId = commonDto?.PatientId,
                        BloodPressure = examination?.Diastolic + '/' + examination?.Systolic,
                        Pulse = examination?.Pulse,
                        ExaminationDate = DateTime.Today,
                        Findings = "not found",
                        Temperature = examination?.Temperature,
                        Notes = examination?.HeightFeet + '+' + examination?.HeightInches + '+' + examination?.Weight,
                        TenantId = 1

                    };

                    var examinationResult = await _prescriptionPatientService.ExaminationInsert(examinationInsertNew);



                    var prescriptionExmainationNew = new PrescriptionExaminationInsertRequestDto
                    {
                        PrescriptionId = prescriptionResult.Result,
                        ExaminationId = examinationResult.Result.ExaminationID

                    };


                    var result = await _prescriptionExaminationService.Insert(prescriptionExmainationNew);
                }

            }
        }

        private async Task InsertMedication(PrescriptionRequestDto request, Response<int> prescriptionResult)
        {
            if ((request.Medications != null) && prescriptionResult.Result != 0)
            {
                foreach (var medication in request.Medications)
                {
                    var prescriptionMedicationsNew = new PrescriptionItemInsertRequestDto
                    {
                        PrescriptionId = prescriptionResult.Result,
                        MedicationId = medication.Id,
                        Duration = medication.Duration,
                        Instructions = medication.Notes,
                        Dosage = medication.Timming,
                        MealTime = medication.MealTime

                    };


                    var result = await _prescriptionItemService.Insert(prescriptionMedicationsNew);
                }

            }
        }

        private async Task InsertChiefComplain(PrescriptionRequestDto request, Response<int> prescriptionResult)
        {
            if ((request.ChiefComplaints != null) && prescriptionResult.Result != 0)
            {
                foreach (var complain in request.ChiefComplaints)
                {
                    var prescriptionSymtomNew = new PrescriptionSymtomInsertRequestDto
                    {
                        PrescriptionID = prescriptionResult.Result,
                        SymtomID = complain.Id,
                        Description = complain.Notes,
                        duration = complain.Duration,
                        days = complain.Days

                    };


                    var result = await _prescriptionSymptomService.Insert(prescriptionSymtomNew);
                }

            }
        }

        private async Task InsertTemplate(PrescriptionRequestDto request, Common? commonDto, Response<int> prescriptionResult)
        {
            if ((request.isTemplate == true) && prescriptionResult.Result != 0)
            {

                var prescriptionTemplateNew = new PrescriptionTemplateInsertRequestDto
                {
                    PrescriptionID = prescriptionResult.Result,
                    Name = request.TemplateName,
                    DoctorID = (int)commonDto.DoctorId
                };


                var result = await _prescriptionTemplateService.Insert(prescriptionTemplateNew);


            }
        }




        private async Task InsertHistory(PrescriptionRequestDto request, Response<int> prescriptionResult)
        {
            if ((request.History != null) && prescriptionResult.Result != 0)
            {
                foreach (var history in request.History)
                {
                    var prescriptionHistoryNew = new PrescriptionPatientHistoryRequestDto
                    {
                        PrescriptionID = prescriptionResult.Result,
                        CommonHistoryID = history.Id,
                        PastHistory = history.PastHistory,
                        PresentHistory = history.PresentHistory
                    };


                    var result = await _prescriptionPatientHistoryService.Insert(prescriptionHistoryNew);
                }

            }
        }

        private async Task InsertDiagonosis(PrescriptionRequestDto request, Response<int> prescriptionResult)
        {
            if ((request.Diagnosis != null) && prescriptionResult.Result != 0)
            {
                foreach (var diagnosis in request.Diagnosis)
                {
                    var prescriptionDiagnosisNew = new PrescriptionDiagonosisInsertRequestDto
                    {
                        PrescriptionId = prescriptionResult.Result,
                        DiagnosisId = diagnosis.Id,
                        PastDiagnosis = diagnosis.PastDiagnosis,
                        PresentDiagnosis = diagnosis.PresentDiagnosis

                    };


                    var result = await _prescriptionDiagonsisService.Insert(prescriptionDiagnosisNew);
                }

            }
        }

        private async Task InsertInvestigation(PrescriptionRequestDto request, Response<int> prescriptionResult)
        {
            if ((request.Test != null) && prescriptionResult.Result != 0)
            {
                foreach (var test in request.Test)
                {
                    var prescriptionInvestigationNew = new PrescriptionInvestigationInsertRequestDto
                    {
                        PrescriptionId = prescriptionResult.Result,
                        InvestigationId = test.Id,
                        Description = test.Notes
                    };


                    var result = await _prescriptionInvestigationService.Insert(prescriptionInvestigationNew);
                }

            }
        }

        private async Task InsertAdvice(PrescriptionRequestDto request, Response<int> prescriptionResult)
        {
            if ((request.Advice != null) && prescriptionResult.Result != 0)
            {
                foreach (var advice in request.Advice)
                {
                    var prescriptionAdviceNew = new PrescriptionAdviceInsertRequestDto
                    {
                        PrescriptionId = prescriptionResult.Result,
                        CommonAdviceId = advice.Id,
                        Description = advice.Notes
                    };


                    var result = await _prescriptionAdviceService.Insert(prescriptionAdviceNew);
                }

            }
        }
        public static string GenerateQrCodeBase64(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Input text cannot be null or empty.", nameof(text));

            try
            {
                using (var qrGenerator = new QRCodeGenerator())
                using (var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
                using (var qrCode = new PngByteQRCode(qrCodeData))  // Changed to PngByteQRCode
                {
                    byte[] pngBytes = qrCode.GetGraphic(20);
                    return $"data:image/png;base64,{Convert.ToBase64String(pngBytes)}";
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to generate QR code", ex);
            }
        }


        private async Task<int> InsertPatient(PrescriptionRequestDto request, Common? commonDto, int userResult2)
        {
            if (request.Patient != null)
            {
                var getRuleResult3 = new Response<UserApiResponseDto>();

                if (request.Patient.PatientProfileId != 0)
                {
                    var getRoleResult = await _prescriptionPatientService.UserGetByReferenceIdAndRole(request.Patient.PatientProfileId, "Patient", commonDto.TenantId);
                    getRuleResult3 = getRoleResult;
                    commonDto.userId = getRoleResult.Result?.UserId;
                }

                if (getRuleResult3.Result == null)
                {
                    var prescriptionUserNew = new UserInsertRequestDto
                    {
                        TenantId = 1,
                        FirstName = request.Patient.PatientName,
                        LastName = request.Patient.PatientName,
                        PasswordHash = "stdfadsaring",
                        Email = "patient@example.com",
                        UserType = "Patient",
                        IsActive = true,
                        PhoneNumber = request.Patient.patientPhoneNo,
                        ReferenceUserId = request.Patient.PatientProfileId
                    };

                    var userResult = await _prescriptionPatientService.UserInsert(prescriptionUserNew);

                    if (request.Patient.PatientProfileId == 0)
                    {
                        var userUpdatedNew = new UserUpdateRequestDto
                        {
                            UserId = userResult.Result.UserId,
                            ReferenceUserId = userResult.Result.UserId
                        };

                        var userUpdate = await _prescriptionPatientService.UserUpdate(userUpdatedNew);
                    }

                    userResult2 = userResult.Result.UserId;
                    commonDto.userId = userResult.Result.UserId;

                    var prescriptionPatientNew = new PatientsInsertRequestDto
                    {
                        UserID = userResult2,
                        PatientReferenceID = request.Patient.PatientProfileId,
                        BloodGroup = request.Patient.patientBloodGroup,
                        PatientAge = request.Patient.PatientAge,
                        Gender = request.Patient.patientGender

                    };

                    var patientResult = await _prescriptionPatientService.PatientInsert(prescriptionPatientNew);

                    if (request.Patient.PatientProfileId == 0)
                    {
                        var patientUpdatedNew = new PatientsUpdateRequestDto
                        {
                            PatientID = patientResult.Result.PatientID,
                            UserID = userResult2,
                            PatientReferenceID = userResult2,
                        };

                        var updatedPatientResult = await _prescriptionPatientService.PatientUpdate(patientUpdatedNew);



                    }


                    commonDto.PatientId = patientResult.Result?.PatientID;
                }
                else
                {
                    var getPatientDetails = await _prescriptionPatientService.GetByPatientId(commonDto.userId);

                    if (getPatientDetails.Result == null)
                    {
                        var prescriptionPatientNew = new PatientsInsertRequestDto
                        {
                            UserID = commonDto.userId,
                            PatientReferenceID = request.Patient.PatientProfileId,

                        };

                        if (request.Patient.PatientProfileId != null)
                        {
                            var patientResult = await _prescriptionPatientService.PatientInsert(prescriptionPatientNew);
                            commonDto.PatientId = patientResult.Result?.PatientID;
                        }
                    }
                    else
                    {
                        commonDto.PatientId = getPatientDetails.Result.PatientID;
                    }
                }
            }

            return userResult2;
        }

        //private async Task InsertDoctor(PrescriptionRequestDto request, Common? commonDto, ref int userResult2, ref Response<UserApiResponseDto> getRuleResult2)
        //{
        //    if (request.Doctor != null)
        //    {

        //        if (request.Doctor.DoctorProfileId != 0)
        //        {
        //            var getRoleResult = await _prescriptionPatientService.UserGetByReferenceIdAndRole(request.Doctor.DoctorProfileId, "Doctor", commonDto.TenantId);
        //            getRuleResult2 = getRoleResult;
        //            commonDto.userId = getRoleResult?.Result?.UserId;
        //        }

        //        if (getRuleResult2.Result == null)
        //        {
        //            var prescriptionUserNew = new UserInsertRequestDto
        //            {
        //                TenantId = 1,
        //                FirstName = request.Doctor.DoctorName,
        //                LastName = request.Doctor.DoctorName,
        //                PasswordHash = "stdfadsaring",
        //                Email = "ihemon2@gmail.com",
        //                UserType = "Doctor",
        //                IsActive = true,
        //                ReferenceUserId = request.Doctor.DoctorProfileId
        //            };

        //            var userResult = await _prescriptionPatientService.UserInsert(prescriptionUserNew);

        //            if (request.Doctor.DoctorProfileId == 0)
        //            {
        //                var userUpdatedNew = new UserUpdateRequestDto
        //                {
        //                    UserId = userResult.Result.UserId,
        //                    ReferenceUserId = userResult.Result.UserId
        //                };

        //                var userUpdate = await _prescriptionPatientService.UserUpdate(userUpdatedNew);
        //            }

        //            userResult2 = userResult.Result.UserId;
        //            commonDto.userId = userResult.Result.UserId;
        //            var prescriptionDoctorNew = new DoctorInsertRequestDto
        //            {
        //                UserID = userResult2,
        //                Specialization = request.Doctor.specializationName,
        //                LicenseNumber = request.Doctor.bmdc,
        //                DoctorReferenceID = request.Doctor.DoctorProfileId,
        //                HospitalAffiliation = "dfasfasdfafa"
        //            };



        //            var doctorResult = await _prescriptionPatientService.DoctorInsert(prescriptionDoctorNew);

        //            if (request.Doctor.DoctorProfileId == 0)
        //            {
        //                var doctorNewUpdated = new DoctorUpdateRequestDto
        //                {
        //                    DoctorID = doctorResult.Result.DoctorID,
        //                    UserID = userResult2,
        //                    DoctorReferenceID = userResult2
        //                };
        //                var updatedDoctorResult = await _prescriptionPatientService.DoctorUpdate(doctorNewUpdated);

        //            }


        //            commonDto.DoctorId = doctorResult.Result?.DoctorID;

        //            // Call the new method to insert doctor details
        //            await InsertDoctorDetails(request, commonDto);




        //        }

        //        else
        //        {

        //            var getDoctorDetails = await _prescriptionPatientService.GetByDoctorid(commonDto.userId);

        //            if (getDoctorDetails.Result == null)
        //            {
        //                var prescriptionDoctorNew = new DoctorInsertRequestDto
        //                {
        //                    UserID = commonDto.userId,
        //                    Specialization = "string",
        //                    LicenseNumber = request.Doctor.bmdc,
        //                    DoctorReferenceID = request.Doctor.DoctorProfileId,
        //                    HospitalAffiliation = "dfasfasdfafa"
        //                };
        //                if (request.Doctor.DoctorProfileId != null)
        //                {
        //                    var doctorResult = await _prescriptionPatientService.DoctorInsert(prescriptionDoctorNew);
        //                    commonDto.DoctorId = doctorResult.Result?.DoctorID;
        //                }



        //            }
        //            else
        //            {
        //                commonDto.DoctorId = getDoctorDetails.Result.DoctorID;


        //            }




        //        }


        //    }
        //}


        //[HttpPost("new_registration")]
        //public async Task<ActionResult<ApiResponse<int>>> Registration(UserInsertRequestDto request)
        //{
        //    var apiResponse = new ApiResponse<int>();

        //    try
        //    {
        //        // PasswordHasher instance create
        //        var passwordHasher = new PasswordHasher<UserInsertRequestDto>();

        //        // Hash the password
        //        var hashedPassword = passwordHasher.HashPassword(request, request.PasswordHash);

        //        // Create new user DTO with hashed password
        //        var prescriptionUserNew = new UserInsertRequestDto
        //        {
        //            TenantId = 1,
        //            FirstName = request.FirstName,
        //            LastName = request.LastName,
        //            PasswordHash = hashedPassword, // ✅ hashed password stored
        //            Email = string.IsNullOrWhiteSpace(request.Email) ? "patient@example.com" : request.Email,
        //            UserType = "Doctor",
        //            IsActive = true,
        //            PhoneNumber = request.PhoneNumber
        //        };

        //        // Insert user and get new user ID
        //        var user = await _prescriptionPatientService.UserInsert(prescriptionUserNew);

        //        // Use the new user ID for doctor insert
        //        var prescriptionDoctorNew = new DoctorInsertRequestDto
        //        {
        //            UserID = user.Result.UserId,
        //            HospitalAffiliation = "dfasfasdfafa"
        //        };

        //        var doctorResult = await _prescriptionPatientService.DoctorInsert(prescriptionDoctorNew);

        //        // Prepare API response
        //        apiResponse.Results = doctorResult.Result.DoctorID;
        //        apiResponse.IsSuccess = true;
        //        apiResponse.Message = "Doctor registered successfully.";
        //        apiResponse.Status = "Success";
        //        apiResponse.StatusCode = 200;
        //    }
        //    catch (Exception ex)
        //    {
        //        apiResponse.IsSuccess = false;
        //        apiResponse.Message = $"Error: {ex.Message}";
        //        apiResponse.Status = "Failed";
        //        apiResponse.StatusCode = 500;
        //    }

        //    return Ok(apiResponse);
        //}



        [HttpPost("doctor_registration")]
        public async Task<ActionResult<ApiResponse<int>>> DoctorRegistration(RegistrationInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            var pdfResult = new PdfApiResponseDto();
            var pdfApiResponse = new ApiResponse<PdfApiResponseDto>();

            await InsertDoctorDetails(request);

            return null;
        }


        private async Task InsertDoctorDetails(RegistrationInsertRequestDto request)
        {
            // Area of expertise Insert
            if (request.Doctor.AreaOfExperties != null)
            {
                var AreadofExpertiseNew = new ExpertiseCategoryInsertRequestDto
                {
                    TenantID = 1,
                    ExpertiseName = request.Doctor.AreaOfExperties
                };
                var doctorCategoryExpInsertResult = await _prescriptionPatientService.ExpertiseCategoryInsert(AreadofExpertiseNew);

                var prescriptionExpertise = new DoctorExpertiseInsertRequestDto
                {
                    ExpertiseID = doctorCategoryExpInsertResult.Result.ExpertiseID,
                    DoctorID = request.doctorId
                };

                var DoctorExpertise = await _prescriptionPatientService.DoctorExpertiseInsert(prescriptionExpertise);
            }

            // Degree Insert
            if (request.Doctor.Degree != null && request.Doctor.Degree.Count != 0)
            {
                foreach (var degree in request.Doctor.Degree)
                {

                    var doctorDegreeNew = new DoctorDegreeInsertRequestDto
                    {
                        DoctorID = request.doctorId,
                        DegreeID = degree.Id,
                        PassingYear = 0,
                        InstituteName = degree.InstituteName,
                        InstituteID = 0,
                        Country = degree.InstituteCountry,
                        CountryID = 0,
                        City = degree.InstituteCity,
                        CityID = 0,
                        ZipCode = "string",
                        ZipCodeID = 0,
                        TenantID = 0
                    };

                    var doctorDegreeInsertResult = await _prescriptionPatientService.DoctorDegreeInsert(doctorDegreeNew);
                }
            }


            // Schedule Insert
            //if (request.Doctor.Schedule != null && request.Doctor.Schedule.Count != 0)
            //{
            //    foreach (var schedule in request.Doctor.Schedule)
            //    {
            //        var prescriptionDoctorScheduleNew = new ScheduleInsertRequestDto
            //        {
            //            Time = schedule.StartTime+"-"+schedule.EndTime,
            //            Day = schedule.Start+"-"+schedule.End,
            //            TenantID = commonDto?.TenantId
            //        };

            //        var scheduleResult = await _prescriptionPatientService.ScheduleInsert(prescriptionDoctorScheduleNew);

            //        var doctorScheduleNew = new DoctorScheduleInsertRequestDto
            //        {
            //            DoctorID = commonDto?.DoctorId,
            //            TenantID = commonDto?.TenantId,
            //            ScheduleID = scheduleResult.Result.ScheduleID,

            //        };

            //        var doctorDegreeInsertResult = await _prescriptionPatientService.DoctorScheduleInsert(doctorScheduleNew);
            //    }
            //}




            // Chamber Insert
            //if (request.Doctor.Chamber != null && request.Doctor.Chamber.Count != 0)
            //{
            //    foreach (var chamber in request.Doctor.Chamber)
            //    {
            //        var prescriptionDoctorChamberNew = new DoctorChamberInsertRequestDto
            //        {
            //            DoctorID = commonDto?.DoctorId,
            //            ChamberName = chamber.ChamberName,
            //            Address = chamber.Address,
            //            Country = chamber.Country,
            //            CountryID = 0,
            //            City = chamber.City,
            //            CityID = 0,
            //            ZipCode = chamber.ZipCode,
            //            ZipCodeID = 0,
            //            TenantID = 0,
            //            IsDeleted = false
            //        };

            //        var result = await _prescriptionPatientService.InsertDoctorChamber(prescriptionDoctorChamberNew);
            //    }
            //}
        }

        [Authorize(Policy = PermissionConstants.PrescriptionUpdate)]
        [HttpPut("update-prescription")]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePrescription(PrescriptionUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionService.Update(request);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionApiConstantsResponseMessage.prescription_update_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionApiConstantsResponseMessage.prescription_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, PrescriptionApiConstantsResponseMessage.prescription_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] GeminiRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request?.Prompt))
                return BadRequest("Prompt is required.");

            try
            {
                var result = await _prescriptionPatientService.CallGemini2Flash(request.Prompt);
                return Ok(new { response = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        
        }


        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] GeminiChatRequestDto request)
        {
            if (request?.Contents == null || !request.Contents.Any())
                return BadRequest(new { error = "Contents are required" });

            try
            {
                var result = await _prescriptionPatientService.GenerateChatAsync(request);
                return Ok(new { response = result });
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("overloaded"))
            {
                return StatusCode(503, new
                {
                    error = "Service Unavailable",
                    message = "The AI service is currently overloaded. Please retry shortly.",
                    retryAfter = 5
                });
            }
            catch (Exception ex)
            {
                // Log the full exception for debugging
                // _logger.LogError(ex, "Error in Generate endpoint");

                return StatusCode(500, new
                {
                    error = "Internal Server Error",
                    message = ex.Message, // Temporarily expose for debugging
                    details = ex.InnerException?.Message
                });
            }
        }





        [Authorize(Policy = PermissionConstants.PrescriptionDelete)]
        [HttpDelete("delete-prescription")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePrescription(int prescriptionId)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _prescriptionService.Delete(prescriptionId);

                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, PrescriptionApiConstantsResponseMessage.prescription_delete_success_message);
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
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionApiConstantsResponseMessage.prescription_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, PrescriptionApiConstantsResponseMessage.prescription_see_try_catch);
                return Ok(apiResponse);
            }

            return Ok(apiResponse);
        }

        // ========== Merged from PrescriptionMasterMainApiController - Backward Compatibility Routes ==========

        [HttpPost("prescription-master")]
        [Authorize(Policy = PermissionConstants.PrescriptionCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreatePrescriptionMasterMainApi([FromBody] PrescriptionInsertRequestDto request)
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

        [HttpGet("prescription-master/{id}")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetId)]
        public async Task<ActionResult<ApiResponse<PrescriptionApiResponseDto>>> GetPrescriptionByIdMainApi(int id)
        {
            return await GetPrescriptionById(id);
        }

        [HttpGet("prescription-master")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<List<PrescriptionApiResponseDto>>>> GetAllPrescriptionsMainApi()
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

        [HttpGet("prescription-master/patient-disease-list/{patientId}")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<List<object>>>> GetPatientDiseaseListMainApi(int patientId)
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

        [HttpGet("prescription-master/prescription-count")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<int>>> GetPrescriptionCountMainApi()
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

        [HttpGet("prescription-master/prescription-list-by-appointment-creator-id/{patientId}")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<List<PrescriptionApiResponseDto>>>> GetPrescriptionsByAppointmentCreatorIdMainApi(int patientId)
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

        [HttpGet("prescription-master/prescription-master-list-by-doctor-id/{doctorId}")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<List<PrescriptionApiResponseDto>>>> GetPrescriptionsByDoctorIdMainApi(int doctorId)
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

        [HttpGet("prescription-master/prescription-master-list-by-doctor-id-patient-id")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<List<PrescriptionApiResponseDto>>>> GetPrescriptionsByDoctorIdAndPatientIdMainApi([FromQuery] int doctorId, [FromQuery] int patientId)
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

        [HttpGet("prescription-master/prescription-master-list-by-patient-id/{patientId}")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<List<PrescriptionApiResponseDto>>>> GetPrescriptionsByPatientIdMainApi(int patientId)
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

        [HttpPut("prescription-master")]
        [Authorize(Policy = PermissionConstants.PrescriptionUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdatePrescriptionMasterMainApi([FromBody] PrescriptionUpdateRequestDto request)
        {
            return await UpdatePrescription(request);
        }

        [HttpGet("prescription-master/{id}/prescription")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetId)]
        public async Task<ActionResult<ApiResponse<PrescriptionApiResponseDto>>> GetPrescriptionByIdRoute(int id)
        {
            return await GetPrescriptionById(id);
        }

        [HttpGet("prescription-master/prescription-by-appointment-id/{appointmentId}")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetId)]
        public async Task<ActionResult<ApiResponse<PrescriptionApiResponseDto>>> GetPrescriptionByAppointmentId(int appointmentId)
        {
            var apiResponse = new ApiResponse<PrescriptionApiResponseDto>();
            try
            {
                var prescription = await _prescriptionService.GetByAppointmentId(appointmentId);
                if (prescription.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Prescription not found for this appointment");
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

        [HttpGet("get-template-prescription-by-ids")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetId)]
        public async Task<ActionResult<ApiResponse<object>>> GetTemplatePrescriptionById([FromQuery] int templateId)
        {
            var apiResponse = new ApiResponse<object>();
            try
            {
                var template = await _prescriptionTemplateService.GetById(templateId);
                if (template.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Template not found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = template.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Template retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        //[HttpGet("gets-all-prescription-template-by-doctor-id")]
        //[Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        //public async Task<ActionResult<ApiResponse<List<object>>>> GetAllPrescriptionTemplatesByDoctorId([FromQuery] int doctorId)
        //{
        //    var apiResponse = new ApiResponse<List<object>>();
        //    try
        //    {
        //        var templates = await _prescriptionTemplateService.GetAllByDoctorId(doctorId);
        //        if (templates.Result == null || templates.Result.Count == 0)
        //        {
        //            ApiResponseHelper.SetFailedResponse(apiResponse, new List<object>(), "No templates found");
        //            return Ok(apiResponse);
        //        }

        //        apiResponse.Results = templates.Result.Cast<object>().ToList();
        //        ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Templates retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        ApiResponseHelper.SetFailedResponse(apiResponse, new List<object>(), $"Error: {ex.Message}");
        //    }
        //    return Ok(apiResponse);
        //}

        //[HttpGet("get-pdf-prescriptions-by-patient-doctor-id")]
        //[Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        //public async Task<ActionResult<ApiResponse<List<object>>>> GetPdfPrescriptionsByPatientDoctorId([FromQuery] int patientId, [FromQuery] int doctorId)
        //{
        //    var apiResponse = new ApiResponse<List<object>>();
        //    try
        //    {
        //        var baseUrl = _configuration.GetSection("GeneralSettings:PrescriptionBaseURL").Value;
        //        var endPoint = $"api/2025-02/get-pdf-prescriptions-by-patient-doctor-id?patientId={patientId}&doctorId={doctorId}";
        //        string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                
        //        var responseJson = await _baseRestClientApiService.MakeApiCall<JObject>(baseUrl, endPoint, Method.Get, null, token, 3, 1000);
        //        var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
        //        var pdfs = deSerializedJsonResult["results"]?.ToObject<List<object>>();

        //        if (pdfs == null || pdfs.Count == 0)
        //        {
        //            ApiResponseHelper.SetFailedResponse(apiResponse, new List<object>(), "No PDF prescriptions found");
        //            return Ok(apiResponse);
        //        }

        //        apiResponse.Results = pdfs;
        //        ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "PDF prescriptions retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        ApiResponseHelper.SetFailedResponse(apiResponse, new List<object>(), $"Error: {ex.Message}");
        //    }
        //    return Ok(apiResponse);
        //}

        [HttpGet("get-pdf-prescriptions-by-doctor-prehand-id")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<List<object>>>> GetPdfPrescriptionsByDoctorPrehandId([FromQuery] int doctorId)
        {
            var apiResponse = new ApiResponse<List<object>>();
            try
            {
                var baseUrl = _configuration.GetSection("GeneralSettings:PrescriptionBaseURL").Value;
                var endPoint = $"api/2025-02/get-pdf-prescriptions-by-doctor-prehand-id?doctorId={doctorId}";
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                
                var responseJson = await _baseRestClientApiService.MakeApiCall<JObject>(baseUrl, endPoint, Method.Get, null, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                var pdfs = deSerializedJsonResult["results"]?.ToObject<List<object>>();

                if (pdfs == null || pdfs.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<object>(), "No prehand PDF prescriptions found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = pdfs;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Prehand PDF prescriptions retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<object>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        //[HttpGet("get-prescription-pdf-by-appointment-id")]
        //[Authorize(Policy = PermissionConstants.PrescriptionGetId)]
        //public async Task<ActionResult<ApiResponse<object>>> GetPrescriptionPdfByAppointmentId([FromQuery] int appointmentId)
        //{
        //    var apiResponse = new ApiResponse<object>();
        //    try
        //    {
        //        var baseUrl = _configuration.GetSection("GeneralSettings:PrescriptionBaseURL").Value;
        //        var endPoint = $"api/2025-02/get-prescription-pdf-by-appointment-id?appointmentId={appointmentId}";
        //        string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                
        //        var responseJson = await _baseRestClientApiService.MakeApiCall<JObject>(baseUrl, endPoint, Method.Get, null, token, 3, 1000);
        //        var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
        //        var pdf = deSerializedJsonResult["results"]?.ToObject<object>();

        //        if (pdf == null)
        //        {
        //            ApiResponseHelper.SetFailedResponse(apiResponse, null, "PDF prescription not found for this appointment");
        //            return Ok(apiResponse);
        //        }

        //        apiResponse.Results = pdf;
        //        ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "PDF prescription retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}");
        //    }
        //    return Ok(apiResponse);
        //}

        //[HttpPost("prescription-upload")]
        //[Authorize(Policy = PermissionConstants.PrescriptionCreate)]
        //public async Task<ActionResult<ApiResponse<int>>> PrescriptionUpload([FromBody] ScannedPrescriptionInsertRequestDto request)
        //{
        //    var apiResponse = new ApiResponse<int>();
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var response = await _scannedPrescriptionService.Insert(request);
        //            if (response.IsSuccess)
        //            {
        //                ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Prescription uploaded successfully");
        //            }
        //            else
        //            {
        //                ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
        //            }
        //        }
        //        else
        //        {
        //            ApiResponseHelper.SetFailedResponse(apiResponse, 0, "Invalid request data");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error: {ex.Message}");
        //    }
        //    return Ok(apiResponse);
        //}

        [HttpGet("get-medication-division-usage")]
        [Authorize(Policy = PermissionConstants.PrescriptionGetAll)]
        public async Task<ActionResult<ApiResponse<List<object>>>> GetMedicationDivisionUsage([FromQuery] int? tenantId = null, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            var apiResponse = new ApiResponse<List<object>>();
            try
            {
                var usage = await _prescriptionService.GetMedicationDivisionUsage(tenantId, startDate, endDate);
                if (usage.Result == null || usage.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<object>(), "No medication usage data found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = usage.Result;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Medication usage data retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<object>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }


    }
}
