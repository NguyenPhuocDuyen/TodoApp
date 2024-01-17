using TodoApp.Models.Dtos;
using TodoApp.Models.SeedWork;

namespace TodoApp.Client.Services
{
    public interface ITaskApiClient
    {
        Task<PagedList<TaskDto>?> GetAllTasks(Models.TaskListSearch taskListSearch);
        Task<TaskDto?> GetById(string id);
        Task<bool> Create(TaskCreateRequest task);
        Task<bool> Update(Guid id, TaskUpdateRequest task);
        Task<bool> AssignTask(Guid id, AssignTaskRequest task);
        Task<bool> Delete(Guid id);

    }
}
