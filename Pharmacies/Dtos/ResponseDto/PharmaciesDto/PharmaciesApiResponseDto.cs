using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacies.Dtos.ResponseDto.PharmaciesDto
{
    public record PharmaciesApiResponseDto
    {
        public int PharmacyId { get; set; }
        public int TenantId { get; set; }
        public string PharmacyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
