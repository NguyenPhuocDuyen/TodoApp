using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TodoApp.Models.SD;

namespace TodoApp.Models.Dtos
{
    public class TaskDto
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? AssigneeId { get; set; }

        public string? AssigneeName { get; set; }

        public DateTime CreatedAt { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }
    }
}
