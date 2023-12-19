using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models.Dtos;
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
        public async Task<IActionResult> Get()
            => Ok(_mapper.Map<List<TaskDto>>(await _taskRepository.GetAllTasks()));

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
            return CreatedAtAction(nameof(GetById), new { id = task.Id}, task);
        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Models.Task newTask)
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
            await _taskRepository.Update(newTask);
            return NoContent();
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
