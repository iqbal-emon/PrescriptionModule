namespace AuthenticationSystem.Dtos.RequestDto
{
    public class UserLoginRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// User login type: 1=Patient, 2=Doctor, 3=Agent, 4=Admin
        /// Used to validate that user is logging into the correct portal
        /// </summary>
        public int? UserLoginType { get; set; }
    }
}
