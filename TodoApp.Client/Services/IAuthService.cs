namespace TodoApp.Client.Services
{
    public interface IAuthService
    {
        Task<Models.LoginResponse> Login(Models.LoginRequest loginRequest);
        Task Logout();
    }
}
