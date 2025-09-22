using System;
using System.ComponentModel.DataAnnotations;

namespace DoctorSchedule.Dtos.RequestDto.DoctorScheduleDto
{
    public class DoctorScheduleInsertRequestDto
    {
        public int DoctorID { get; set; }

        public int ScheduleID { get; set; }

        public int TenantID { get; set; }
  
     
    }
}
