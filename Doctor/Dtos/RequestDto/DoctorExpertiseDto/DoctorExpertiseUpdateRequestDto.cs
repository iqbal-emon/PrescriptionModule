using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.DoctorExpertiseDto
{
    public class DoctorExpertiseUpdateRequestDto
    {
        public int? DoctorExpertiseID { get; set; }

        public int? DoctorID { get; set; }

        public int? ExpertiseID { get; set; }

        public int? ExperienceYears { get; set; }

        public string? Certification { get; set; }
    }
}

