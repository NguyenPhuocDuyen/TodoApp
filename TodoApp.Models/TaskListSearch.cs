using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Models.SeedWork;

namespace TodoApp.Models
{
    public class TaskListSearch : PagingParameters
    {
        public string? Name { get; set; }
        public Guid? AssigneeId { get; set; }
        public SD.Priority? Priority { get; set; }
    }
}
