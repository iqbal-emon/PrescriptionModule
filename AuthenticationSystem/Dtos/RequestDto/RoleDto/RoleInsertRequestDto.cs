using System.ComponentModel.DataAnnotations;

namespace AuthenticationSystem.Dtos.RequestDto.RoleDto
{
    public class RoleInsertRequestDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        public bool IsDefault { get; set; } = false;

        public bool IsActive { get; set; } = true;
    }
}

