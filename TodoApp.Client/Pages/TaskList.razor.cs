using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TodoApp.Client.Services;
using TodoApp.Models.Dtos;

namespace TodoApp.Client.Pages
{
    public partial class TaskList
    {
        [Inject] private ITaskApiClient TaskApiClient { get; set; }
        [Inject] private IUserApiClient UserApiClient { get; set; }

        private List<TaskDto> Tasks;

        private List<UserDto> Assignees;

        private Models.TaskListSearch TaskListSearch = new Models.TaskListSearch();

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            Tasks = await TaskApiClient.GetAllTasks(TaskListSearch);
            Assignees = await UserApiClient.GetAllUsers();
        }

        private async Task SearchForm(EditContext editContext)
        {
            Tasks = await TaskApiClient.GetAllTasks(TaskListSearch);
        }
    }
}
