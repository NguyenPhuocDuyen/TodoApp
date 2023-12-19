using Microsoft.AspNetCore.Components;
using TodoApp.Client.Services;
using TodoApp.Models.Dtos;

namespace TodoApp.Client.Pages
{
    public partial class TodoTask
    {
        [Inject] private ITaskApiClient TaskApiClient { get; set; }

        private List<TaskDto> Tasks;

        protected override async Task OnInitializedAsync()
        {
            Tasks = await TaskApiClient.GetAllTasks();
        }
    }
}
