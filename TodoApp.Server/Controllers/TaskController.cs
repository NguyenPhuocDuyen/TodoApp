using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoApp.Models;
using TodoApp.Models.Dtos;
using TodoApp.Models.SeedWork;
using TodoApp.Server.Repositories;

namespace TodoApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskController(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        // GET: api/<TaskController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TaskListSearch taskListSearch)
        {
            var pagedList = await _taskRepository.GetAllTasks(taskListSearch);

            var taskDtos = _mapper.Map<List<TaskDto>>(pagedList.Items);

            return Ok(
                    new PagedList<TaskDto>(taskDtos,
                        pagedList.MetaData.TotalCount,
                        pagedList.MetaData.CurrentPage,
                        pagedList.MetaData.PageSize)
                );
        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null)
            {
                return NotFound($"Not found task id: {id}");
            }
            return Ok(_mapper.Map<TaskDto>(task));
        }

        // POST api/<TaskController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskCreateRequest newTask)
        {
            var task = new Models.Task
            {
                Name = newTask.Name,
                Priority = newTask.Priority,
                Status = Models.SD.Status.Open,
                CreatedAt = DateTime.UtcNow,
            };
            await _taskRepository.Create(task);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TaskUpdateRequest newTask)
        {
            if (id != newTask.Id)
            {
                return BadRequest("Id not match");
            }

            var task = await _taskRepository.GetById(id);
            if (task == null)
            {
                return NotFound($"Not found task id: {id}");
            }
            task.Name = newTask.Name;
            task.Priority = newTask.Priority;
            await _taskRepository.Update(task);
            return NoContent();
        }

        [HttpPut]
        [Route("{id}/assign")]
        public async Task<IActionResult> AssignTask(Guid id, [FromBody] AssignTaskRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskFromDb = await _taskRepository.GetById(id);

            if (taskFromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            taskFromDb.AssigneeId = request.UserId.Value == Guid.Empty ? null : request.UserId.Value;

            await _taskRepository.Update(taskFromDb);

            return Ok(_mapper.Map<TaskDto>(taskFromDb));
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null)
            {
                return NotFound($"Not found task id: {id}");
            }
            await _taskRepository.Delete(task);
            return NoContent();
        }
    }
}
