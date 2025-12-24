using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.GeminiDto
{
    public class GeminiContentDto
    {
        public string Role { get; set; } // "user" | "assistant"
        public List<GeminiPartDto> Parts { get; set; }
    }
}
