using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speciality.Dtos.RequestDto.SpecialityDto
{
    public class SpecialityUpdateRequestDto
    {
        public int SpecialityID { get; set; }
        public string? SpecialityName { get; set; }
        public string? Description { get; set; }
    }
}

