using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionPatientHistory.Dtos.ResponseDto.PrescriptionPatientHistory
{
    public class PrescriptionPatientHistoryApiResponseDto
    {
        public int PrescriptionPatientHistoryID { get; set; }
        public int CommonHistoryID { get; set; }
        public int PrescriptionID { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

    }
}
