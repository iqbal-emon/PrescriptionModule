using System;
using System.ComponentModel.DataAnnotations;

namespace ExpertiseCategory.Dtos.RequestDto.ExpertiseCategoryDto
{
    public class ExpertiseCategoryUpdateRequestDto
    {
        public int ExpertiseID { get; set; }

        public string? ExpertiseName { get; set; }



    }
}
