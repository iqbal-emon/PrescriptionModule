using System.ComponentModel.DataAnnotations;

namespace AuthenticationSystem.Dtos.RequestDto.UserDto
{
    public class UserInsertRequestDto
    {
        [Required(ErrorMessage = "Full name is required.")]
        [MaxLength(150, ErrorMessage = "Full name cannot exceed 150 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        [MaxLength(100, ErrorMessage = "User name cannot exceed 100 characters.")]
        public string UserName { get; set; }

        [MaxLength(150, ErrorMessage = "Email cannot exceed 150 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password hash is required.")]
        [MaxLength(255, ErrorMessage = "Password hash cannot exceed 255 characters.")]
        public string PasswordHash { get; set; }

        [MaxLength(20, ErrorMessage = "Contact number cannot exceed 20 characters.")]
        public string? ContactNo { get; set; }

        [Required(ErrorMessage = "Role ID is required.")]
        public int RoleId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

