using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionCreateDto
{
    public class ScheduleDto
    {

        public string? Start { get; set; }       // e.g. "Saturday"
        public string? End { get; set; }         // e.g. "Wednesday"
        public string? StartTime { get; set; }   // e.g. "20:00"
        public string? EndTime { get; set; }     // e.g. "22:00"

    }
}
