using Microsoft.EntityFrameworkCore;
using TodoApp.Server.Data;

namespace TodoApp.Server.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoAppDbContext _dbContext;

        public TaskRepository(TodoAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Models.Task task)
        {
            await _dbContext.AddAsync(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Models.Task task)
        {
            _dbContext.Remove(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Models.Task task)
        {
            _dbContext.Update(task);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<Models.Task>> GetAllTasks()
            => await _dbContext.Tasks.Include(x => x.Assignee).ToListAsync();

        public async Task<Models.Task?> GetById(Guid id)
            => await _dbContext.Tasks.Include(x => x.Assignee).FirstAsync(x => x.Id == id);
    }
}
