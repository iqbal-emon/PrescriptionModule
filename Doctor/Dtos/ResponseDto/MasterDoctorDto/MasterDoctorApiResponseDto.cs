using System;

namespace Doctor.Dtos.ResponseDto.MasterDoctorDto
{
    public class MasterDoctorApiResponseDto
    {
        public int MasterDoctorID { get; set; }
        public int DoctorID { get; set; }
        public int? AgentMasterID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

