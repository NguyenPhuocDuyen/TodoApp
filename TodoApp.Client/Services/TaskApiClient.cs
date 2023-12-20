﻿using System.Net.Http.Json;
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

        public Task Create(TaskCreateRequest task)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string task)
        {
            throw new NotImplementedException();
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

        public Task Update(TaskUpdateRequest task)
        {
            throw new NotImplementedException();
        }
    }
}
