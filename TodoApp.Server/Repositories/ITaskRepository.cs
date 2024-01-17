using TodoApp.Models.SeedWork;

namespace TodoApp.Server.Repositories
{
    public interface ITaskRepository
    {
        Task<PagedList<Models.Task>> GetAllTasks(Models.TaskListSearch taskListSearch);
        Task<Models.Task?> GetById(Guid id);
        Task Create(Models.Task task);
        Task Update(Models.Task task);
        Task Delete(Models.Task task);
    }
}
