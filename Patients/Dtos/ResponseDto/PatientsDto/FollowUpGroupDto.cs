using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patients.Dtos.ResponseDto.PatientsDto
{
    public class FollowUpGroupDto
    {
        public string Date { get; set; }
        public List<FollowUpPatientDto> Patients { get; set; }
    }
}
