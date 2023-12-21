using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TodoApp.Models.SD;

namespace TodoApp.Models.Dtos
{
    public class TaskCreateRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Priority Priority { get; set; }
    }
}
