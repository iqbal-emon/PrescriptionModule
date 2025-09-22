using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionCreateDto
{
    public class PrescriptionRequestDto
    {
        public int? appointmentId { get; set; }
        public string? uploadImage { get; set; }
        public string? gender { get; set; }
        public string? appointmentCode { get; set; }
        
        public bool isHeader { get; set; }
        public bool isPreHand { get; set; }
        public bool isTemplate { get; set; }
        public string? TemplateName { get; set; }
        public string? signature { get; set; }
        
        public PatientDto? Patient { get; set; }
        public DoctorDto? Doctor { get; set; }
        public List<ChiefComplaintDto>? ChiefComplaints { get; set; }
        public List<HistoryDto>? History { get; set; }
        public List<DiagnosisDto>? Diagnosis { get; set; }
        public List<MedicationDto>? Medications { get; set; }
        public List<TestDto>? Test { get; set; }
        public List<AdviceDto>? Advice { get; set; }
        public string? FollowUp { get; set; }
        //public List<FollowUpDto>? FollowUp { get; set; }

        public List<ExaminationDto>? Examination { get; set; }
    }
}
