using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symptoms.Dtos.ResponseDto.SymtomDto
{
    public record SymptomsApiResponseDto
    {
        public int SymptomId { get; set; }
        public int TenantId { get; set; }
        public string SymptomName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
