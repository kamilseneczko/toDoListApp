using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApi.Entities
{
    public class TaskGroupDataToWeb
    {
        public IEnumerable<TaskGroup> taskGroups { get; set; }
        public int Count { get; set; }
    }
}
