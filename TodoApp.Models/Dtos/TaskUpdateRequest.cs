using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TodoApp.Models.SD;

namespace TodoApp.Models.Dtos
{
    public class TaskUpdateRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Priority Priority { get; set; }
    }
}
