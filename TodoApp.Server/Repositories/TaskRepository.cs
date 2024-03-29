﻿using Microsoft.EntityFrameworkCore;
using TodoApp.Models.SeedWork;
using TodoApp.Server.Data;

namespace TodoApp.Server.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoAppDbContext _dbContext;

        public TaskRepository(TodoAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Models.Task task)
        {
            await _dbContext.AddAsync(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Models.Task task)
        {
            _dbContext.Remove(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Models.Task task)
        {
            _dbContext.Update(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PagedList<Models.Task>> GetAllTasks(Models.TaskListSearch taskListSearch)
        {
            var query = _dbContext.Tasks.Include(x => x.Assignee).AsQueryable();

            if (!string.IsNullOrEmpty(taskListSearch.Name))
                query = query.Where(x => x.Name.Contains(taskListSearch.Name));

            if (taskListSearch.AssigneeId.HasValue)
                query = query.Where(x => x.AssigneeId == taskListSearch.AssigneeId.Value);

            if (taskListSearch.Priority.HasValue)
                query = query.Where(x => x.Priority == taskListSearch.Priority.Value);

            var count = await query.CountAsync();
            var data =  await query.OrderByDescending(x => x.CreatedAt)
                .Skip((taskListSearch.PageNumber - 1) * taskListSearch.PageSize)
                .Take(taskListSearch.PageSize)
                .ToListAsync();

            return new PagedList<Models.Task>(data, count, taskListSearch.PageNumber, taskListSearch.PageSize);
        }

        public async Task<Models.Task?> GetById(Guid id)
            => await _dbContext.Tasks.Include(x => x.Assignee).FirstAsync(x => x.Id == id);
    }
}
