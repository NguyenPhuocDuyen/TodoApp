using Microsoft.AspNetCore.Components;
using TodoApp.Client.Components;
using TodoApp.Client.Pages.Components;
using TodoApp.Client.Services;
using TodoApp.Models.Dtos;
using TodoApp.Models.SeedWork;

namespace TodoApp.Client.Pages
{
    public partial class TaskList
    {
        [Inject] private ITaskApiClient TaskApiClient { get; set; }

        protected Confirmation Confirmation { get; set; }
        protected AssignTask AssignTask { get; set; }

        private Guid TaskDeleteId { get; set; }
        private List<TaskDto> Tasks;
        public MetaData MetaData { get; set; }

        private Models.TaskListSearch TaskListSearch = new();

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await GetTasks();
        }

        public async Task TaskSearch(Models.TaskListSearch taskListSearch)
        {
            TaskListSearch = taskListSearch;
            await GetTasks();
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
                await GetTasks();
            }
        }

        private void OpenAssignPopup(Guid taskId)
        {
            AssignTask.Show(taskId);
        }

        public async Task AssignTaskSuccess(bool result)
        {
            if (result)
            {
                await GetTasks();
            }
        }

        private async Task GetTasks()
        {
            var pagingReponse = await TaskApiClient.GetAllTasks(TaskListSearch);
            Tasks = pagingReponse.Items;
            MetaData = pagingReponse.MetaData;
        }

        private async Task SelectedPage(int page)
        {
            TaskListSearch.PageNumber = page;
            await GetTasks();
        }
    }
}
