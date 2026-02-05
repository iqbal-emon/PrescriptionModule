using System.ComponentModel.DataAnnotations;

namespace AuthenticationSystem.Dtos.RequestDto.CompanyBranchDto
{
    public class CompanyBranchInsertRequestDto
    {
        [Required(ErrorMessage = "Company ID is required.")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(150, ErrorMessage = "Name cannot exceed 150 characters.")]
        public string Name { get; set; }

        public string? Address { get; set; }

        [MaxLength(20, ErrorMessage = "Contact number cannot exceed 20 characters.")]
        public string? ContactNo { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

