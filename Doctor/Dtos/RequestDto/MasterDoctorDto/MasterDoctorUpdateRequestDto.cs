using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.MasterDoctorDto
{
    public class MasterDoctorUpdateRequestDto
    {
        public int? MasterDoctorID { get; set; }

        public int? DoctorID { get; set; }

        public int? AgentMasterID { get; set; }
    }
}

