using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacies.Dtos.RequestDto.PharmaciesDto
{
    public class PharmaciesInsertRequestDto
    {
        [Required(ErrorMessage = "TenantId is required.")]
        public int TenantId { get; set; }

        [Required(ErrorMessage = "Pharmacy name is required.")]
        [MaxLength(100, ErrorMessage = "Pharmacy name cannot exceed 100 characters.")]
        public string PharmacyName { get; set; }

        [MaxLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
        public string Address { get; set; }

        [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string PhoneNumber { get; set; }

        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

    }

}
