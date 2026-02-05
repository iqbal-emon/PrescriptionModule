using System.ComponentModel.DataAnnotations;

namespace AuthenticationSystem.Dtos.RequestDto.CompanyDto
{
    public class CompanyUpdateRequestDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "License number cannot exceed 100 characters.")]
        public string? LicenseNo { get; set; }

        [MaxLength(100, ErrorMessage = "Drug registration certificate cannot exceed 100 characters.")]
        public string? DrugRegCertificate { get; set; }

        public string? Address { get; set; }

        [MaxLength(20, ErrorMessage = "Contact number cannot exceed 20 characters.")]
        public string? ContactNo { get; set; }

        [MaxLength(150, ErrorMessage = "Email cannot exceed 150 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Currency symbol is required.")]
        [MaxLength(10, ErrorMessage = "Currency symbol cannot exceed 10 characters.")]
        public string CurrencySymbol { get; set; } = "?";
    }
}

