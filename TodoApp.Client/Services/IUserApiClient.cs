using TodoApp.Models.Dtos;

namespace TodoApp.Client.Services
{
    public interface IUserApiClient
    {
        Task<List<UserDto>?> GetAllUsers();
    }
}
