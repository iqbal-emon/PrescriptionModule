using System;

namespace AuthenticationSystem.Dtos.ResponseDto.CompanyDto
{
    public class CompanyApiResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? LicenseNo { get; set; }
        public string? DrugRegCertificate { get; set; }
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public string CurrencySymbol { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

