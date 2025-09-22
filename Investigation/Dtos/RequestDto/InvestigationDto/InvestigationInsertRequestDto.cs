using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investigation.Dtos.RequestDto.InvestigationDto
{
    public class InvestigationInsertRequestDto
    {

        [Required(ErrorMessage = "Investigation name is required.")]
        [MaxLength(100, ErrorMessage = "Investigation name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        [MaxLength(50, ErrorMessage = "Code cannot exceed 50 characters.")]
        public string Code { get; set; }

    }
}
