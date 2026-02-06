using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.CampaignDoctorDto
{
    public class CampaignDoctorInsertRequestDto
    {
        [Required(ErrorMessage = "Doctor ID is required.")]
        public int DoctorID { get; set; }

        public int? CampaignID { get; set; }

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int TenantID { get; set; }
    }
}

