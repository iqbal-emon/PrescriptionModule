using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Dtos.RequestDto.AdviceDto
{
    public class AdviceUpdateRequestDto
    {
        public int CommonAdviceID { get; set; }
        public string? Advice { get; set; }
        public string? Type { get; set; }
        public DateTime? CreatedAt { get; set; } 

    }
}
