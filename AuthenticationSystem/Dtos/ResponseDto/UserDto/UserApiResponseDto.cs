using System;

namespace AuthenticationSystem.Dtos.ResponseDto.UserDto
{
    public class UserApiResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
        public string? ContactNo { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

