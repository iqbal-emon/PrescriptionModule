using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symptoms.Dtos.RequestDto.SymptomDto
{
    public class SymptomsUpdateRequestDto
    {
        public int SymptomId { get; set; }
        public int? TenantId { get; set; }
        public string? SymptomName { get; set; }
        public string? Description { get; set; }
 
    }
}
