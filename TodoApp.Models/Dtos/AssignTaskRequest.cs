using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Models.Dtos
{
    public class AssignTaskRequest
    {
        public Guid? UserId { get; set; }
    }
}
