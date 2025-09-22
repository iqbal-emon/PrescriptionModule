using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.ResponseDto.PrescriptionDto
{
    public class PrescriptionAnalyticsDto
    {
        public int Male { get; set; }
        public int Female { get; set; }
        public int TotalPrescription { get; set; }
    }
}
