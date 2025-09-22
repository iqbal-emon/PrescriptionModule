using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHistory.Dtos.RequestDto.CommonHistoryDto
{
    public class CommonHistoryInsertRequestDto
    {
        [Required(ErrorMessage = "Common History name is required.")]
        [MaxLength(100, ErrorMessage = "Common History name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string Description { get; set; }


    }
}
