using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionFollowUp.Dtos.RequestDto.PrescriptionFollowUpDto
{
    public class PrescriptionFollowUpUpdateRequestDto
    {
        public int PrescriptionFollowUpID { get; set; }
        public int? FollowUpID { get; set; }
        public int? PrescriptionID { get; set; }


    }
}
