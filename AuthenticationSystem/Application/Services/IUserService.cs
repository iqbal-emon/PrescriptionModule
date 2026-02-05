namespace AuthenticationSystem.Application.Services
{
    public interface IUserService
    {
        Task<List<string>> GetUserPermissions(int userId);
    }
}
