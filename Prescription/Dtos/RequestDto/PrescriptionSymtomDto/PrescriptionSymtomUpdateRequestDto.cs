using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionSymtomDto
{
    public class PrescriptionSymtomUpdateRequestDto
    {
        public int PrescriptionSymtomID { get; set; } // Primary Key

        public int? PrescriptionID { get; set; } // Foreign Key to Prescriptions

        public int? SymptomID { get; set; } // Foreign Key to Symptoms

        public string? Description { get; set; }
        public string? days { get; set; }

        public string? duration { get; set; }


    }
}
