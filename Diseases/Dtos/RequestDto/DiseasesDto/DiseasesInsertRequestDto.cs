using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diseases.Dtos.RequestDto.DiseasesDto
{
    public class DiseasesInsertRequestDto
    {
        [Required(ErrorMessage = "TenantId is required.")]
        public int TenantId { get; set; }

        [Required(ErrorMessage = "Disease name is required.")]
        [MaxLength(100, ErrorMessage = "Disease name cannot exceed 100 characters.")]
        public string DiseaseName { get; set; }

        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string Description { get; set; }


  


    }
}
