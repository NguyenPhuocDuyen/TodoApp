using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Models
{
    public class SD
    {
        public enum Priority
        {
            Low,
            Medium,
            High
        }

        public enum Status
        {
            Open,
            InProgress,
            Resolved,
            Done,
            Canceled,
            Closed
        }
    }
}
