using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.DegreeDto
{
    public class DegreeInsertRequestDto
    {
        public string? DegreeName { get; set; }

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int? TenantID { get; set; }

        public int? Duration { get; set; }


        public string? DurationType { get; set; }
    }
}
