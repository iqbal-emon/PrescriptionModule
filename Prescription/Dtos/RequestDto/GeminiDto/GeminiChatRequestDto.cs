using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.GeminiDto
{
    public class GeminiChatRequestDto
    {
        public List<ContentDto> Contents { get; set; }
        public SystemInstructionDto SystemInstruction { get; set; }
        public GenerationConfigDto GenerationConfig { get; set; }
    }

    public class ContentDto
    {
        public string Role { get; set; }
        public List<PartDto> Parts { get; set; }
    }

    public class PartDto
    {
        public string Text { get; set; }
    }

    public class SystemInstructionDto
    {
        public List<PartDto> Parts { get; set; }
    }

    public class GenerationConfigDto
    {
        public double? Temperature { get; set; }
        public int? MaxOutputTokens { get; set; }
    }

}
