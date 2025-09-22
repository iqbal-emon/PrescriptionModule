using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionSymtomDto
{
    public class PrescriptionSymtomInsertRequestDto
    {

        [Required(ErrorMessage = "SymptomID is required.")]
        public int SymtomID { get; set; }

        [Required(ErrorMessage = "PrescriptionID is required.")]
        public int PrescriptionID { get; set; } // Foreign Key to Prescriptions

        public string? Description { get; set; } // Additional notes about the symptom

        public string days { get; set; }

        public string duration { get; set; }
   



    }
}
