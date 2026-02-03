using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patients.Dtos.ResponseDto.PatientsDto
{
    public class FollowUpResponseDto
    {
        public List<FollowUpGroupDto> Followups { get; set; }
    }
}
