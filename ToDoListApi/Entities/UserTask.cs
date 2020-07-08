using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApi.Entities
{
    public class UserTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public TaskGroup User { get; set; }
        public StatusTask Status { get; set; }
    }
}
