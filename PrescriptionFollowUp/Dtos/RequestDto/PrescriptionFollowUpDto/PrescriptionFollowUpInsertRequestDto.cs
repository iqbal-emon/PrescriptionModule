using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionFollowUp.Dtos.RequestDto.PrescriptionFollowUpDto
{
    public class PrescriptionFollowUpInsertRequestDto
    {
        [Required(ErrorMessage = "FollowUpID is required.")]
        public int FollowUpID { get; set; }

        [Required(ErrorMessage = "PrescriptionID is required.")]
        public int PrescriptionID { get; set; }
    }
}
