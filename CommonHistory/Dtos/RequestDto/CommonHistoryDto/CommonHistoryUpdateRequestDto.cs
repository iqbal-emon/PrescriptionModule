using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHistory.Dtos.RequestDto.CommonHistoryDto
{
    public class CommonHistoryUpdateRequestDto
    {
        public int CommonHistoryId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
