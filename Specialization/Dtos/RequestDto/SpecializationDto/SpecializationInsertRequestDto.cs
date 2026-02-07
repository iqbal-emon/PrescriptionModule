using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specialization.Dtos.RequestDto.SpecializationDto
{
    public class SpecializationInsertRequestDto
    {
        public int? SpecialityID { get; set; }

        [Required(ErrorMessage = "Specialization name is required.")]
        [MaxLength(200)]
        public string SpecializationName { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int TenantID { get; set; }
    }
}

