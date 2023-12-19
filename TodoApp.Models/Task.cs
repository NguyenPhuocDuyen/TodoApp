using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TodoApp.Models.SD;

namespace TodoApp.Models
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? AssigneeId { get; set; }

        [ForeignKey(nameof(AssigneeId))]
        public virtual User? Assignee { get; set; }

        public DateTime CreatedAt { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }
    }
}
