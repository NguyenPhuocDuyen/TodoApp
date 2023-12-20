using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
using TodoApp.Server.Data;

namespace TodoApp.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoAppDbContext _dbContext;

        public UserRepository(TodoAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
            => await _dbContext.Users.ToListAsync();
    }
}
