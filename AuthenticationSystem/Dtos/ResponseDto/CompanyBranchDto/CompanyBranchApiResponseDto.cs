using System;

namespace AuthenticationSystem.Dtos.ResponseDto.CompanyBranchDto
{
    public class CompanyBranchApiResponseDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

