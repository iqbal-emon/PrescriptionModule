using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionCreateDto
{
    public class RegistrationInsertRequestDto
    {
        public int doctorId { get; set; }
        public DoctorDto? Doctor { get; set; }
    }
}
