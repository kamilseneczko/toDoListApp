using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApi.Entities;
using ToDoListApi.Repository;

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : Controller
    {
        private readonly ITaskGroupRepository repository;

        public AppController(ITaskGroupRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllTaskGroup(int? currentPage = null, int? pageSize = null)
        {
            return Ok(repository.GetAllTaskGroup(currentPage, pageSize));
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskGroup(int id)
        {
            return Ok(repository.GetTaskGroup(id));
        }

        [HttpGet("task/{id}")]
        public IActionResult GetTask(int id)
        {
            return Ok(repository.GetTask(id));
        }


        [HttpPost]
        public ActionResult<TaskGroup> Post([FromBody] TaskGroup model)
        {
            if (repository.CheckIfUserExist(model.Name)){
                return StatusCode(409, $"User '{model.Name}' already exists");
            };

            if (model.UserTasks == null)
            {
                model.UserTasks = new List<UserTask>();
            }
            repository.AddEntity(model);
            if (repository.SaveAll()) {
                return Created($"api/app/{model.Id}", model);
            };
            return BadRequest();
        }

        [HttpPost("userTask")]
        public IActionResult AddTask([FromBody] UserTaskToAdd model)
        {
            if (repository.CheckIfUserExist(model.UserName))
            {
                var user = repository.GetTaskGroup(model.UserId);
                if (user == null)
                    return NotFound();

                var newUserTask = new UserTask()
                {
                    Deadline = model.Deadline,
                    Name = model.Name,
                    Status = StatusTask.New
                };

                user.UserTasks.Add(newUserTask);
                if (repository.SaveAll())
                {
                    return Created($"api/app/{model.UserId}", newUserTask);
                };

            }

            return BadRequest("User not exist");
        }

        [HttpPut]
        public IActionResult UpdateTask(UserTaskToUpdate task)
        {
            if(repository.CheckIfUserExist(task.UserName))
            {
                var taskFromRepo = repository.GetTask(task.Id);
                if (taskFromRepo == null)
                    return NotFound();

                taskFromRepo.Name = task.Name;
                taskFromRepo.Deadline = task.Deadline;
                taskFromRepo.Status = task.status;

                if (repository.SaveAll())
                    return NoContent();

                throw new Exception($"Updating task {task.Id} failed on save");
            }

            return BadRequest("User not exist");
        }

        [HttpDelete("{id}")]
        public  IActionResult DeleteTaskGroup(int id)
        {
            var result = repository.DeleteTaskGroup(id);
            if (result)
                return Ok();
            
            return BadRequest("Failed to delete task group");
        }

        [HttpDelete("task/{id}")]
        public IActionResult DeleteTask(int id)
        {
            var result = repository.DeleteTask(id);
            if (result)
                return Ok();

            return BadRequest("Failed to delete task");
        }

    }
}
