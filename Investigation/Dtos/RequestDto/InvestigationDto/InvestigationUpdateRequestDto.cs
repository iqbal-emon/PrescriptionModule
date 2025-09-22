using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investigation.Dtos.RequestDto.InvestigationDto
{
    public class InvestigationUpdateRequestDto
    {
        public int InvestigationId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
    }
}
