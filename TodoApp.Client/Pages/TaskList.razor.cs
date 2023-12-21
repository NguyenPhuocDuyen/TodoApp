using Microsoft.AspNetCore.Components;
using TodoApp.Client.Components;
using TodoApp.Client.Services;
using TodoApp.Models.Dtos;

namespace TodoApp.Client.Pages
{
    public partial class TaskList
    {
        [Inject] private ITaskApiClient TaskApiClient { get; set; }

        protected Confirmation Confirmation { get; set; }

        private Guid TaskDeleteId { get; set; }
        private List<TaskDto> Tasks;

        private Models.TaskListSearch TaskListSearch = new();

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            Tasks = await TaskApiClient.GetAllTasks(TaskListSearch);
        }

        public async Task TaskSearch(Models.TaskListSearch taskListSearch)
        {
            TaskListSearch = taskListSearch;
            Tasks = await TaskApiClient.GetAllTasks(TaskListSearch);
        }

        private void OnDeleteTask(Guid deleteId)
        {
            TaskDeleteId = deleteId;
            Confirmation.Show();
        }

        public async Task OnConfirmDeleteTask(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await TaskApiClient.Delete(TaskDeleteId);
                Tasks = await TaskApiClient.GetAllTasks(TaskListSearch);
            }
        }
    }
}
