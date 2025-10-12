using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patients.Dtos.ResponseDto.PatientsDto
{
    public class PatientAgeDistributionResponseDto
    {
        public string AgeGroup { get; set; }
        public int PatientCount { get; set; }
        public double Percentage { get; set; }
    }
}
