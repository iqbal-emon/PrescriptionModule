using System.ComponentModel.DataAnnotations;

namespace AuthenticationSystem.Dtos.RequestDto.RolePermissionDto
{
    public class RolePermissionUpdateRequestDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Role ID is required.")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Permission ID is required.")]
        public int PermissionId { get; set; }
    }
}

