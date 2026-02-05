namespace AuthenticationSystem.Dtos.ResponseDto.RoleDto
{
    public class RoleApiResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
    }
}

