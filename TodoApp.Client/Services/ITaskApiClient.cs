using TodoApp.Models.Dtos;

namespace TodoApp.Client.Services
{
    public interface ITaskApiClient
    {
        Task<List<TaskDto>?> GetAllTasks(Models.TaskListSearch taskListSearch);
        Task<TaskDto?> GetById(string id);
        Task<bool> Create(TaskCreateRequest task);
        Task<bool> Update(Guid id, TaskUpdateRequest task);
        Task<bool> Delete(string task);

    }
}
