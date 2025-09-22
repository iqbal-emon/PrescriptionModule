using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Dtos.RequestDto.AdviceDto
{
    public class AdviceInsertRequestDto
    {

        [Required(ErrorMessage = "Advice is required.")]
        [MaxLength(500, ErrorMessage = "Advice cannot exceed 500 characters.")]
        public string Advice { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [MaxLength(50, ErrorMessage = "Type cannot exceed 50 characters.")]
        public string Type { get; set; }

    }
}
