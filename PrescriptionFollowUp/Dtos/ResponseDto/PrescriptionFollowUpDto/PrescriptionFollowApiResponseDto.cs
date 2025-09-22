using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionFollowUp.Dtos.ResponseDto.PrescriptionFollowUpDto
{
    public class PrescriptionFollowApiResponseDto
    {
        public int PrescriptionFollowUpID { get; set; }
        public int FollowUpID { get; set; }
        public int PrescriptionID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
