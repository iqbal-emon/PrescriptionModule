namespace AuthenticationSystem.Services
{
    public interface IUserService
    {
        Task<List<string>> GetUserPermissions(int userId);
    }
}
