using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examinations.Dtos.ResponseDto.ExaminationsDto
{
    public record ExaminationsApiResponseDto
    {
        public int ExaminationID { get; set; }

        public int TenantID { get; set; }

        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public DateTime ExaminationDate { get; set; } 

        public string Findings { get; set; }

        public string Notes { get; set; }
        public string BloodPressure { get; set; }
        public string Pulse { get; set; }
        public string Temperature { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
