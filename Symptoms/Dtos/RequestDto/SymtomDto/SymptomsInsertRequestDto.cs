using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symptoms.Dtos.RequestDto.SymtomDto
{
    public class SymptomsInsertRequestDto
    {
        [Required(ErrorMessage = "TenantId is required.")]
        public int TenantId { get; set; }

        [Required(ErrorMessage = "Symptom name is required.")]
        [MaxLength(100, ErrorMessage = "Symptom name cannot exceed 100 characters.")]
        public string SymptomName { get; set; }

        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string Description { get; set; }

    }

}
