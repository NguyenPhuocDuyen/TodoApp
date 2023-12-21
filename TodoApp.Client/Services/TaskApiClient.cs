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

        public async Task<bool> Create(TaskCreateRequest task)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Task", task);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/Task/{id}");
            return result.IsSuccessStatusCode;
        }

        public Task<List<TaskDto>?> GetAllTasks(Models.TaskListSearch taskListSearch)
        {
            var url = $"/api/Task?name={taskListSearch.Name}&assigneeId={taskListSearch.AssigneeId}&priority={taskListSearch.Priority}";
            var result = _httpClient.GetFromJsonAsync<List<TaskDto>>(url);
            return result;
        }

        public Task<TaskDto?> GetById(string id)
        {
            var result = _httpClient.GetFromJsonAsync<TaskDto>($"/api/Task/{id}");
            return result;
        }

        public async Task<bool> Update(Guid id, TaskUpdateRequest task)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/Task/{id}", task);
            return result.IsSuccessStatusCode;
        }
    }
}
