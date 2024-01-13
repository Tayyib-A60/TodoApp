using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Dtos;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private static IList<TodoEntity> _todos = new List<TodoEntity>();

        public TodoController(ILogger<TodoController> logger)
        {
            _logger = logger;
            // _todos = new List<TodoEntity>();
        }

        [HttpPost]
        public IActionResult CreateTodo([FromBody] TodoDTO todoDTO) 
        {
            var  newTodoItem = new TodoEntity
            {
                ID = Guid.NewGuid(),
                Title = todoDTO.title, 
                Description = todoDTO.description, 
                CreatedAt = DateTime.UtcNow
            };

            _todos.Add(newTodoItem);

            return Ok(newTodoItem);
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            return Ok(_todos);
        }

        [HttpGet("{id}")]
        public IActionResult GetTodo(string id)
        {
            var todo = FindTodo(id);

            if(todo is null) {
                return NotFound($"Todo with id {id} not found");
            }

            return Ok(todo);
        }

        private TodoEntity FindTodo(string id) 
        {
            var todo = _todos.FirstOrDefault(t => t.ID.ToString() == id);
            return todo;
        }
    }
}