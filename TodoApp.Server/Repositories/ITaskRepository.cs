namespace TodoApp.Server.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Models.Task>> GetAllTasks();
        Task<Models.Task?> GetById(Guid id);
        Task Create(Models.Task task);
        Task Update(Models.Task task);
        Task Delete(Models.Task task);
    }
}
