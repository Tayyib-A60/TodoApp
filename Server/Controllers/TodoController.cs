using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Data.Entities;
using Server.Data.Interfaces;
using Server.Dtos;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly ITodoRepository _todoRepo;
        private readonly IUserRepository _userRepo;

        public TodoController(ILogger<TodoController> logger, ITodoRepository todoRepo, IUserRepository userRepo)
        {
            _logger = logger;
            _todoRepo = todoRepo;
            _userRepo = userRepo;
        }

        [HttpPost("create")]
        public IActionResult CreateTodo([FromBody] TodoDTO todoDTO) 
        {
            var user = _userRepo.GetUserById(todoDTO.userId);

            if(user is not null) {
                var  newTodoItem = new TodoEntity
                {
                    ID = Guid.NewGuid(),
                    UserID = Guid.Parse(todoDTO.userId),
                    Title = todoDTO.title, 
                    Description = todoDTO.description, 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _todoRepo.CreateTodo(newTodoItem);

                return Ok(newTodoItem);
            }

            return NotFound($"User with id {todoDTO.userId} not found");
        }

        [HttpPut("update")]
        public IActionResult UpdateTodo([FromBody] UpdateTodoDTO updateTodoDTO)
        {

            var updatedTodo = UpdateTodoEntity(updateTodoDTO);

            if(updatedTodo is null) {
                return NotFound($"Todo with id {updateTodoDTO.id} not found");
            }

            return Ok(updatedTodo);
        }

        [HttpGet("list")]
        public IActionResult GetTodos()
        {
            var todos = _todoRepo.ListTodos();
            return Ok(todos);
        }

        [HttpGet("get_by_id/{id}")]
        public IActionResult GetTodo(string id)
        {
            var todo = FindTodo(id);

            if(todo is null) {
                return NotFound($"Todo with id {id} not found");
            }

            return Ok(todo);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteTodo(string id)
        {
            var todoDeleted = DeleteTodoEntity(id);

            if(!todoDeleted) {
                return NotFound($"Todo with id {id} not found");
            }

            return Ok(id);
        }

        private TodoEntity? FindTodo(string id) 
        {
            var todo = _todoRepo.GetTodoById(id);
            return todo;
        }

        private TodoEntity? UpdateTodoEntity(UpdateTodoDTO updateTodoDTO)
        {
            var todo = _todoRepo.GetTodoById(updateTodoDTO.id);

            if(todo is not null) {
                todo.Title = updateTodoDTO.title;
                todo.Description = updateTodoDTO.description;
                todo.UpdatedAt = DateTime.UtcNow;
                
                _todoRepo.UpdateTodo(todo);

                return todo;
            }

            return null;
        }

        private bool DeleteTodoEntity(string id)
        {
            var todoId = _todoRepo.DeleteTodo(id);

            if(!String.IsNullOrEmpty(todoId)) {
                return true;
            }

            return false;
        }
    }
}