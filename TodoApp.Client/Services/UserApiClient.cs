using System.Net.Http.Json;
using TodoApp.Models.Dtos;

namespace TodoApp.Client.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly HttpClient _httpClient;

        public UserApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<UserDto>?> GetAllUsers()
        {
            var result = _httpClient.GetFromJsonAsync<List<UserDto>>("/api/User");
            return result;
        }
    }
}
