using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApi.Entities
{
    public class TaskGroup
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual ICollection<UserTask> UserTasks { get; set; }
    }
}
