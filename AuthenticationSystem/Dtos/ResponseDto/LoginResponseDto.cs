namespace AuthenticationSystem.Dtos.ResponseDto
{
    public class LoginResponseDto
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public List<string> RoleName { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string LoginType { get; set; }
        public string UserEmail { get; set; }
    }
}

