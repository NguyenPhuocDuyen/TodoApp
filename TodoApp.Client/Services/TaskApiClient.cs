using System.Net.Http.Json;
using TodoApp.Models.Dtos;

namespace TodoApp.Client.Services
{
    public class TaskApiClient : ITaskApiClient
    {
        private readonly HttpClient _httpClient;

        public TaskApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<TaskDto>> GetAllTasks()
        {
            var result = _httpClient.GetFromJsonAsync<List<TaskDto>>("/api/Task");
            return result;
        }
    }
}
