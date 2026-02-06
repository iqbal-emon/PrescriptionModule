using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.CampaignDoctorDto
{
    public class CampaignDoctorUpdateRequestDto
    {
        public int? CampaignDoctorID { get; set; }

        public int? DoctorID { get; set; }

        public int? CampaignID { get; set; }
    }
}

