using System.Collections.Generic;
using ToDoListApi.Entities;

namespace ToDoListApi.Repository
{
    public interface ITaskGroupRepository
    {
        void AddEntity(object model);
        TaskGroupDataToWeb GetAllTaskGroup(int? currentPage = null, int? pageSize = null);
        TaskGroup GetTaskGroup(int id);
        UserTask GetTask(int id);
        bool SaveAll();
        bool CheckIfUserExist(string name);
        bool DeleteTaskGroup(int id);
        bool DeleteTask(int id);
    }
}