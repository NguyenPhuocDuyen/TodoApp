using TodoApp.Models.Dtos;

namespace TodoApp.Client.Services
{
    public interface ITaskApiClient
    {
        Task<List<TaskDto>?> GetAllTasks();
        Task<TaskDto?> GetById(string id);
        Task Create(TaskCreateRequest task);
        Task Update(TaskUpdateRequest task);
        Task Delete(string task);

    }
}
