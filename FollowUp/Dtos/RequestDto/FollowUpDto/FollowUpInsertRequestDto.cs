using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Dtos.RequestDto.FollowUpDto
{
    public class FollowUpInsertRequestDto
    {

        [Required(ErrorMessage = "TenantId is required.")]
        public int TenantId { get; set; }

        [Required(ErrorMessage = "FollowUp is required.")]
        [MaxLength(100, ErrorMessage = "FollowUp cannot exceed 100 characters.")]
        public string Name { get; set; }

        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string Description { get; set; }



    }

}
