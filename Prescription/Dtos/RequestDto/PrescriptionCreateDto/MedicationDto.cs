using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionCreateDto
{
    public class MedicationDto
    {
        public string? Name { get; set; }
        public string? Notes { get; set; }
        public int Id { get; set; }
        public string? Duration { get; set; }
        public string? Timming { get; set; }
        public string? MealTime { get; set; }
    }
}
