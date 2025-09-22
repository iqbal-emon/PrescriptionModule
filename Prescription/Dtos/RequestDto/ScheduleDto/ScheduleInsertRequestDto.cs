using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.ScheduleDto
{
    public class ScheduleInsertRequestDto
    {

        public int? TenantID { get; set; }

        [MaxLength(20)]
        public string Day { get; set; }

        [MaxLength(5)]
        public string Time { get; set; }

    }

}
