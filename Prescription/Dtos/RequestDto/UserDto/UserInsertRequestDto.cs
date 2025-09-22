using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.UserDto
{
    public class UserInsertRequestDto
    {
        [Required(ErrorMessage = "TenantId is required.")]
        public int TenantId { get; set; }

        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string? FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string? LastName { get; set; }

        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [MaxLength(256, ErrorMessage = "Password cannot exceed 256 characters.")]
        public string? PasswordHash { get; set; }
        [MaxLength(20, ErrorMessage = "User type cannot exceed 20 characters.")]
        public string? UserType { get; set; }

        [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public int? ReferenceUserId { get; set; }

    }

}
