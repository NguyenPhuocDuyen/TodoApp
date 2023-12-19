using TodoApp.Models.Dtos;

namespace TodoApp.Client.Services
{
    public interface ITaskApiClient
    {
        Task<List<TaskDto>> GetAllTasks();

    }
}
