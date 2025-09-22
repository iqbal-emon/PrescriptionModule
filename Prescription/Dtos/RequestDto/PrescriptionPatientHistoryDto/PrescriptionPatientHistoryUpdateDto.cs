using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionPatientHistoryDto
{
    public class PrescriptionPatientHistoryUpdateDto
    {
        public int PrescriptionPatientHistoryID { get; set; }
        public int? CommonHistoryID { get; set; }
        public int? PrescriptionID { get; set; }
        public string? Description { get; set; }
    }
}
