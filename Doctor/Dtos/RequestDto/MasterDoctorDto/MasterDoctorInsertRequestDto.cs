using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.MasterDoctorDto
{
    public class MasterDoctorInsertRequestDto
    {
        [Required(ErrorMessage = "Doctor ID is required.")]
        public int DoctorID { get; set; }

        public int? AgentMasterID { get; set; }

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int TenantID { get; set; }
    }
}

