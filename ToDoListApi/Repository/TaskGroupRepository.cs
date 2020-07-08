using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApi.Context;
using ToDoListApi.Entities;

namespace ToDoListApi.Repository
{

    public class TaskGroupRepository : ITaskGroupRepository
    {
        private readonly ToDoContext context;

        public TaskGroupRepository(ToDoContext context)
        {
            this.context = context;
        }

        public TaskGroupDataToWeb GetAllTaskGroup(int? currentPage = null, int? pageSize = null)
        {
            var allTaskCount = context.TaskGroups.Count();

            var query = context.TaskGroups
                .Include(t => t.UserTasks).AsQueryable();

            if (currentPage != null && pageSize != null)
                query = query.Skip((currentPage.Value - 1) * pageSize.Value);

            if (pageSize != null)
                query = query.Take(pageSize.Value);
                

            var taskGroupToWeb = new TaskGroupDataToWeb() { 
                taskGroups = query.ToList(),
                Count = allTaskCount
            };

            return taskGroupToWeb;
        }

        public TaskGroup GetTaskGroup(int id)
        {
            return context.TaskGroups
                .Include(t => t.UserTasks)
                .FirstOrDefault(t => t.Id == id);
        }

        public UserTask GetTask(int id)
        {
            return context.UserTasks
                .Include(t => t.User)
                .FirstOrDefault(t => t.Id == id);
        }

        public void AddEntity(object model)
        {
            context.Add(model);
        }

        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }

        public bool CheckIfUserExist(string name)
        {
            var user = context.TaskGroups.FirstOrDefault(t => t.Name.ToLower() == name.ToLower());

            return user != null;
        }

        public bool DeleteTaskGroup(int id)
        {
            var taskGroup = context.TaskGroups
                .Include(t => t.UserTasks)
                .FirstOrDefault(t => t.Id == id);

            #region usuwanie po kolei elementów z listy zakomentowane (drugi sposób)
            //var taskGroup = context.TaskGroups
            //    .Include(t => t.UserTasks)
            //    .FirstOrDefault(t => t.Id == id);
            //if (taskGroup.UserTasks.Count > 0)
            //{
            //    for (var i = 0; i < taskGroup.UserTasks.Count; i++)
            //    {
            //        var result = DeleteTask(taskGroup.UserTasks.ElementAt(i).Id);
            //        if (!result)
            //            return false;
            //    }
            //}
            #endregion

            context.TaskGroups.Remove(taskGroup);
            return SaveAll();
        }

        public bool DeleteTask(int id)
        {
            var task = context.UserTasks.Find(id);
            context.UserTasks.Remove(task);
            return SaveAll();
        }

       
    }
}


