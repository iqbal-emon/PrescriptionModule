namespace AuthenticationSystem.Dtos.RequestDto.UserDto
{
    /// <summary>
    /// DTO that matches exactly the User_Insert stored procedure parameters
    /// </summary>
    public class UserInsertStoredProcedureDto
    {
        public int TenantID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
        public string? UserType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ContactNo { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; } = true;
        public int? ReferenceUserId { get; set; }
    }
}

