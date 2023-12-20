using TodoApp.Models;

namespace TodoApp.Server.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
    }
}
