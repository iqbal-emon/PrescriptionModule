using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionPatientHistory.Dtos.RequestDto.PrescriptionPatientHistoryDto
{
    public class PrescriptionPatientHistoryRequestDto
    {
        [Required(ErrorMessage = "Common History ID is required.")]
        public int CommonHistoryID { get; set; }

        [Required(ErrorMessage = "Prescription ID is required.")]
        public int PrescriptionID { get; set; }

    }
}
