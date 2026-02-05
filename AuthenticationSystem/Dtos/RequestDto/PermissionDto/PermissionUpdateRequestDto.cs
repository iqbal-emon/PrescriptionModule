using System.ComponentModel.DataAnnotations;

namespace AuthenticationSystem.Dtos.RequestDto.PermissionDto
{
    public class PermissionUpdateRequestDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Display name is required.")]
        [MaxLength(150, ErrorMessage = "Display name cannot exceed 150 characters.")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Permission value is required.")]
        [MaxLength(500, ErrorMessage = "Permission value cannot exceed 500 characters.")]
        public string PermissionValue { get; set; }
    }
}

