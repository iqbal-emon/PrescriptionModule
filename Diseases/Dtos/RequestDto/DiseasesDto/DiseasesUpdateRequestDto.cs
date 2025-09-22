using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diseases.Dtos.RequestDto.DiseasesDto
{
    public class DiseasesUpdateRequestDto
    {
        public int DiseaseId { get; set; }

        public int? TenantId { get; set; }

        public string? DiseaseName { get; set; }

        public string? Description { get; set; }




      
    }
}
