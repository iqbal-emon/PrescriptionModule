using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speciality.Dtos.RequestDto.SpecialityDto
{
    public class SpecialityInsertRequestDto
    {
        [Required(ErrorMessage = "Speciality name is required.")]
        [MaxLength(200)]
        public string SpecialityName { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int TenantID { get; set; }
    }
}

