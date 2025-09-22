using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degree.Dtos.RequestDto.DegreeDto
{
   public class DegreeUpdateRequestDto
    {
        public int DegreeID { get; set; }
        public string? DegreeName { get; set; }
        public int? Duration { get; set; }
        public string? DurationType { get; set; }
    }
}
