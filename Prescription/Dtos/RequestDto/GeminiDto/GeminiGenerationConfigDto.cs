using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.GeminiDto
{
    public class GeminiGenerationConfigDto
    {
        public double? Temperature { get; set; }
        public int? MaxOutputTokens { get; set; }
    }

}
