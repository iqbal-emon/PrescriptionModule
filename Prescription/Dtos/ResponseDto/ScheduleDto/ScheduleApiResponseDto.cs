using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.ResponseDto.ScheduleDto
{
    public record ScheduleApiResponseDto
    {
        public int ScheduleID { get; set; }
        public int TenantID { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
