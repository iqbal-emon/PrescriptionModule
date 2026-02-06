using System;

namespace Doctor.Dtos.ResponseDto.CampaignDoctorDto
{
    public class CampaignDoctorApiResponseDto
    {
        public int CampaignDoctorID { get; set; }
        public int DoctorID { get; set; }
        public int? CampaignID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

