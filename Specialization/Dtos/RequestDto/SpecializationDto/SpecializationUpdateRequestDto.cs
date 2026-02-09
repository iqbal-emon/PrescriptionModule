using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specialization.Dtos.RequestDto.SpecializationDto
{
    public class SpecializationUpdateRequestDto
    {
        public int SpecializationID { get; set; }
        public string? SpecializationName { get; set; }
        public string? Description { get; set; }
        public int? SpecialityID { get; set; }
    }
}

