using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.ResponseDto.PrescriptionSymtomDto
{
    public class PrescriptionSymtomApiResponseDto
    {
        public int PrescriptionSymtomID { get; set; } // Primary Key
        public int PrescriptionID { get; set; } // Foreign Key to Prescriptions
        public int SymtomID { get; set; } // Foreign Key to Symptoms
        public string Description { get; set; } // Additional notes about the symptom
        public string days { get; set; }

        public string duration { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
