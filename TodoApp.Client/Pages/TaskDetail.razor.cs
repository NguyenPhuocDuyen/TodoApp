using Microsoft.AspNetCore.Components;
using TodoApp.Client.Services;
using TodoApp.Models.Dtos;

namespace TodoApp.Client.Pages
{
    public partial class TaskDetail
    {
        [Parameter]
        public string TaskId { get; set; }

        [Inject] private ITaskApiClient TaskApiClient { get; set; }

        private TaskDto? Task;

        protected override async Task OnInitializedAsync()
        {
            Task = await TaskApiClient.GetById(TaskId);
        }
    }
}
