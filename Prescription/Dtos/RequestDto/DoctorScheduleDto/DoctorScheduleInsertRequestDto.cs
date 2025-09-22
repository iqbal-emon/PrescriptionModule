using System;
using System.ComponentModel.DataAnnotations;

namespace Prescription.Dtos.RequestDto.DoctorScheduleDto
{
    public class DoctorScheduleInsertRequestDto
    {
        public int? DoctorID { get; set; }

        public int? ScheduleID { get; set; }

        public int? TenantID { get; set; }
  
     
    }
}
